using FAP_BE.DTOs;
using FAP_BE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Win32;
using OfficeOpenXml;
using System.IO;


namespace FAP_BE.DataAccess
{
    public class StudentManagement
    {
        private static FAP_PRN231Context _context;
        private static StudentManagement instance = null;
        private static readonly object _locker = new object();

        public static StudentManagement Instance
        {
            get
            {
                lock (_locker)
                {
                    if (instance == null)
                    {
                        instance = new StudentManagement();
                        _context = new FAP_PRN231Context();
                    }
                    return instance;
                }
            }
        }

        public List<Student> GetAllStudents()
        {
            try
            {
                var list = _context.Students.
                    Include(md => md.MetaData).
                    ThenInclude(a => a.Account).
                    ThenInclude(r => r.Role).ToList();
                return list;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public Student GetStudentById(int id)
        {
            try
            {
                var student = _context.Students
                    .Include(s => s.MetaData)
                    .ThenInclude(md => md.Account)
                    .ThenInclude(a => a.Role)
                    .FirstOrDefault(s => s.Id == id);

                return student;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Student GetStudentByRoleNumber(string rolenumber)
        {
            try
            {
                var student = _context.Students
                    .Include(s => s.MetaData)
                    .ThenInclude(md => md.Account)
                    .ThenInclude(a => a.Role)
                    .FirstOrDefault(s => s.RoleNumber.Equals(rolenumber));

                return student;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool AddNewStudent(CreateNewStudentDTO newStudentDTO)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Metadata metadata = new Metadata();
                    metadata.Name = newStudentDTO.Name;
                    metadata.Address = newStudentDTO.Address;
                    metadata.Dob = newStudentDTO.Dob;
                    metadata.Email = newStudentDTO.Email;
                    metadata.Image = newStudentDTO.Image;
                    _context.MetaData.Add(metadata);
                    _context.SaveChanges();

                    Account account = new Account();
                    account.Username = newStudentDTO.Username;
                    account.Password = newStudentDTO.Password;
                    account.RoleId = newStudentDTO.RoleId;
                    account.MetaDataId = metadata.MetaDataId;
                    _context.Accounts.Add(account);
                    _context.SaveChanges();

                    Student student = new Student();
                    student.RoleNumber = newStudentDTO.RoleNumber;
                    student.MetaDataId = metadata.MetaDataId;
                    _context.Students.Add(student);
                    _context.SaveChanges();

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool UpdateStudent(int sid, CreateNewStudentDTO newStudentDTO)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existingStudent = _context.Students.FirstOrDefault(x => x.Id == sid);

                    if(existingStudent == null) return false;

                    var existingMetaData = _context.MetaData.FirstOrDefault(x => x.MetaDataId == existingStudent.MetaDataId);
                    var existingAccount = _context.Accounts.FirstOrDefault(x => x.MetaDataId == existingStudent.MetaDataId);


                    existingMetaData.Name = newStudentDTO.Name;
                    existingMetaData.Dob = newStudentDTO.Dob;
                    existingMetaData.Address = newStudentDTO.Address;
                    existingMetaData.Image = newStudentDTO?.Image;

                    existingAccount.Password = newStudentDTO.Password;

                    _context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool DeleteStudent(int sid)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existingStudent = _context.Students.FirstOrDefault(x => x.Id == sid);

                    if (existingStudent == null) return false;

                    int metaid = existingStudent.MetaDataId;

                    var studentCourse = _context.StudentCourse.Where(sc => sc.StudentId == sid);
                    var attendance = _context.Attendances.Where(a => a.StudentId == sid);
                    var existingMetaData = _context.MetaData.FirstOrDefault(x => x.MetaDataId == metaid);
                    var existingAccount = _context.Accounts.FirstOrDefault(x => x.MetaDataId == metaid);

                    _context.StudentCourse.RemoveRange(studentCourse);
                    _context.Attendances.RemoveRange(attendance);
                    _context.SaveChanges();

                    _context.Students.Remove(existingStudent);
                    _context.Accounts.Remove(existingAccount);
                    _context.MetaData.Remove(existingMetaData);

                    _context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
                
        }

        public int StudentAbsentPercentage(int sid)
        {
            int TotalSlots = StudentTotalSlots(sid);
            int TotalAbsentSlots = StudentAbsentSlots(sid);

            if ( (TotalSlots == 0) || (TotalAbsentSlots == 0 && TotalSlots == 0 )) return 0;

            int result = (TotalAbsentSlots/TotalSlots)*100;
            return result;
        }

        public int StudentTotalSlots(int sid)
        {
            var totalSlots = _context.Attendances.Where(a => a.StudentId == sid).Count();
            if (totalSlots == null) return 0;
            return totalSlots;
        }

        public int StudentAbsentSlots(int sid)
        {
            var totalSlots = _context.Attendances.Where(a => a.StudentId == sid && a.Status == 2).Count();
            if (totalSlots == null) return 0;
            return totalSlots;
        }

        public bool ExportStudentToExcel(List<StudentInfoDTO> list, Stream stream)
        {
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("StudentData");

                    worksheet.Cells[1, 1].Value = "StudentId";
                    worksheet.Cells[1, 2].Value = "Rolenumber";
                    worksheet.Cells[1, 3].Value = "Name";
                    worksheet.Cells[1, 4].Value = "Username";
                    worksheet.Cells[1, 5].Value = "Address";
                    worksheet.Cells[1, 6].Value = "Dob";
                    worksheet.Cells[1, 7].Value = "Email";
                    worksheet.Cells[1, 8].Value = "Image";
                    worksheet.Cells[1, 9].Value = "AbsentPercentage%";
                    worksheet.Cells[1, 10].Value = "TotalSlots";

                    int row = 2;
                    foreach (var student in list)
                    {
                        worksheet.Cells[row, 1].Value = student.Id;
                        worksheet.Cells[row, 2].Value = student.RoleNumber;
                        worksheet.Cells[row, 3].Value = student.Name;
                        worksheet.Cells[row, 4].Value = student.Username;
                        worksheet.Cells[row, 5].Value = student.Address;
                        worksheet.Cells[row, 6].Value = student.Dob.ToString("dd/MM/yyyy");
                        worksheet.Cells[row, 7].Value = student.Email;
                        worksheet.Cells[row, 8].Value = student.Image;
                        worksheet.Cells[row, 9].Value = StudentAbsentPercentage(student.Id);
                        worksheet.Cells[row, 10].Value = StudentTotalSlots(student.Id);
                        row++;
                    }

                    package.SaveAs(stream);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;

            }
        }


    }
}

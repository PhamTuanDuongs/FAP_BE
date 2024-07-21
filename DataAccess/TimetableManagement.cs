using AutoMapper;
using FAP_BE.DTOs;
using FAP_BE.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace FAP_BE.DataAccess
{
    public class TimetableManagement
    {
        private static FAP_PRN231Context _context;
        private static TimetableManagement _instance;

        private static readonly object _lock = new object();
        public static TimetableManagement Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TimetableManagement();
                    _context = new FAP_PRN231Context();
                }
                return _instance;
            }
        }


        public List<Attendance>  GetSchedulesByStudentId(int studentId, DateTime from, DateTime to)
        {
            using (var context = new FAP_PRN231Context())
            {
                List<Attendance> listAttendance = _context.Attendances.
                               Include(f => f.Schedule).ThenInclude(i => i.Instructor).
                               Include(c => c.Schedule).ThenInclude(co => co.RoomNavigation).
                               Include(d => d.Schedule).ThenInclude(r => r.Course).ThenInclude(s => s.Subject).
                               Where(sc => sc.Schedule.Date >= from && sc.Schedule.Date <= to && sc.Student.Id == studentId)
                               .ToList();
                listAttendance = listAttendance.DistinctBy(s => s.ScheduleId).ToList();
                return listAttendance;
            }

        }

        public List<Schedule> GetSchedulesByInstructorId(int instructorId, DateTime from, DateTime to)
        {
            using (var context = new FAP_PRN231Context())
            {
                List<Schedule> listSchedules = _context.Schedules
                                   .Include(i => i.Instructor)
                                   .Include(co => co.RoomNavigation).
                                   Include(r => r.Course).ThenInclude(s => s.Subject).
                                   Where(sc => sc.Date >= from && sc.Date <= to && sc.Instructor.Id == instructorId)
                                   .ToList();
                listSchedules = listSchedules.DistinctBy(s => s.Id).ToList();
                return listSchedules;
            }

        }

        public List<Attendance> GetStatisticsAttendance(int id, int courseid)
        {
            using (var context = new FAP_PRN231Context())
            {
                List<Attendance> listAttendance = _context.Attendances.
                                Include(st => st.Student).ThenInclude(stm => stm.MetaData).
                               Include(f => f.Schedule).ThenInclude(i => i.Instructor).
                               Include(c => c.Schedule).ThenInclude(co => co.RoomNavigation).
                               Include(d => d.Schedule).ThenInclude(r => r.Course).ThenInclude(s => s.Subject).
                               Where(sc => sc.Schedule.InstructorId == id && sc.Schedule.CourseId == courseid)
                               .ToList();
                //listAttendance = listAttendance.DistinctBy(s => s.ScheduleId).ToList();
                return listAttendance;
            }
        }


        public int getNumberIsAllowedAbsent(int totalSlot, int numberAbsent)
        {
                int numberofsessionswithoutabsence = totalSlot - numberAbsent;
                double percentslotafterDivideSlotIsAllowedAbsent = (double)numberofsessionswithoutabsence / (double)totalSlot * 100;
                int percentAbsent = (int)(100 - percentslotafterDivideSlotIsAllowedAbsent);
            return percentAbsent;
        }


        public List<Schedule> GetSchedules()
        {
            using (var context = new FAP_PRN231Context())
            {
                List<Schedule> listSchedules = _context.Schedules
                                   .Include(i => i.Instructor)
                                   .Include(co => co.RoomNavigation).
                                   Include(r => r.Course).ThenInclude(s => s.Subject)
                                   .ToList();
                listSchedules = listSchedules.DistinctBy(s => s.Id).ToList();
                return listSchedules;
            }

        }

        public bool ExportStatisticToExcel(List<Statistics_Attendance> list, Stream stream)
        {
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("StudentData");

                    worksheet.Cells[1, 1].Value = "CourseName";
                    worksheet.Cells[1, 2].Value = "Rolenumber";
                    worksheet.Cells[1, 3].Value = "Name";
                    worksheet.Cells[1, 4].Value = "AbsentPercentage%";
                    worksheet.Cells[1, 5].Value = "TotalAbsents";

                    int row = 2;
                    foreach (var student in list)
                    {
                        worksheet.Cells[row, 1].Value = student.CourseName;
                        worksheet.Cells[row, 2].Value = student.RollNumber;
                        worksheet.Cells[row, 3].Value = student.StudentName;
                        worksheet.Cells[row, 4].Value = student.Percentage;
                        worksheet.Cells[row, 5].Value = student.Summary;
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

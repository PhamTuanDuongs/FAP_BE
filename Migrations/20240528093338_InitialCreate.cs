using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAP_BE.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meta_Data",
                columns: table => new
                {
                    MetaDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Dob = table.Column<DateTime>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "varchar(225)", unicode: false, maxLength: 225, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Meta_Dat__429BA08D26E459CC", x => x.MetaDataId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Room__737584F77FEF8B19", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "varchar(225)", unicode: false, maxLength: 225, nullable: false),
                    ManageSlot = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorCode = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    MetaDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Instructo__MetaD__49C3F6B7",
                        column: x => x.MetaDataId,
                        principalTable: "Meta_Data",
                        principalColumn: "MetaDataId");
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleNumber = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    MetaDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Student__MetaDat__44FF419A",
                        column: x => x.MetaDataId,
                        principalTable: "Meta_Data",
                        principalColumn: "MetaDataId");
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    MetaDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK__Account__MetaDat__403A8C7D",
                        column: x => x.MetaDataId,
                        principalTable: "Meta_Data",
                        principalColumn: "MetaDataId");
                    table.ForeignKey(
                        name: "FK__Account__RoleId__3F466844",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    TimeSlot = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Course__TimeSlot__534D60F1",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    Room = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Schedule__Course__5812160E",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Schedule__Instru__5629CD9C",
                        column: x => x.InstructorId,
                        principalTable: "Instructor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Schedule__Room__571DF1D5",
                        column: x => x.Room,
                        principalTable: "Room",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    DateAttended = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Attendan__BB0D8E2DD0F2A81A", x => new { x.StudentId, x.ScheduleId });
                    table.ForeignKey(
                        name: "FK__Attendanc__Sched__5BE2A6F2",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Attendanc__Stude__5AEE82B9",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Account__429BA08C8A1CBF00",
                table: "Account",
                column: "MetaDataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Account__536C85E4BFDD5503",
                table: "Account",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Account__8AFACE1BC8F02419",
                table: "Account",
                column: "RoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_ScheduleId",
                table: "Attendance",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_SubjectId",
                table: "Course",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "UQ__Course__A25C5AA7F742BF69",
                table: "Course",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Instruct__321792F655B5F98E",
                table: "Instructor",
                column: "InstructorCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Instruct__429BA08C5560F710",
                table: "Instructor",
                column: "MetaDataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Meta_Dat__A9D10534CC60EFE4",
                table: "Meta_Data",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Role__737584F67100069D",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_CourseId",
                table: "Schedule",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_InstructorId",
                table: "Schedule",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Room",
                table: "Schedule",
                column: "Room");

            migrationBuilder.CreateIndex(
                name: "UQ__Student__429BA08C677A21C4",
                table: "Student",
                column: "MetaDataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Student__486BE7499774320D",
                table: "Student",
                column: "RoleNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Subject__737584F671C70D24",
                table: "Subject",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Subject__A25C5AA7E5FEA18F",
                table: "Subject",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Instructor");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Meta_Data");
        }
    }
}

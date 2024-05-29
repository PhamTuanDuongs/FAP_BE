﻿// <auto-generated />
using System;
using FAP_BE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FAP_BE.Migrations
{
    [DbContext(typeof(FAP_PRN231Context))]
    [Migration("20240528093338_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FAP_BE.Models.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"), 1L, 1);

                    b.Property<int>("MetaDataId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("AccountId");

                    b.HasIndex(new[] { "MetaDataId" }, "UQ__Account__429BA08C8A1CBF00")
                        .IsUnique();

                    b.HasIndex(new[] { "Username" }, "UQ__Account__536C85E4BFDD5503")
                        .IsUnique();

                    b.HasIndex(new[] { "RoleId" }, "UQ__Account__8AFACE1BC8F02419")
                        .IsUnique();

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("FAP_BE.Models.Attendance", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasMaxLength(225)
                        .HasColumnType("nvarchar(225)");

                    b.Property<DateTime>("DateAttended")
                        .HasColumnType("datetime");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "ScheduleId")
                        .HasName("PK__Attendan__BB0D8E2DD0F2A81A");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Attendance", (string)null);
                });

            modelBuilder.Entity("FAP_BE.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<string>("TimeSlot")
                        .IsRequired()
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("char(5)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex(new[] { "Code" }, "UQ__Course__A25C5AA7F742BF69")
                        .IsUnique();

                    b.ToTable("Course", (string)null);
                });

            modelBuilder.Entity("FAP_BE.Models.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("InstructorCode")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("MetaDataId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "InstructorCode" }, "UQ__Instruct__321792F655B5F98E")
                        .IsUnique();

                    b.HasIndex(new[] { "MetaDataId" }, "UQ__Instruct__429BA08C5560F710")
                        .IsUnique();

                    b.ToTable("Instructor", (string)null);
                });

            modelBuilder.Entity("FAP_BE.Models.Metadata", b =>
                {
                    b.Property<int>("MetaDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MetaDataId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Image")
                        .HasMaxLength(225)
                        .IsUnicode(false)
                        .HasColumnType("varchar(225)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MetaDataId")
                        .HasName("PK__Meta_Dat__429BA08D26E459CC");

                    b.HasIndex(new[] { "Email" }, "UQ__Meta_Dat__A9D10534CC60EFE4")
                        .IsUnique();

                    b.ToTable("Meta_Data", (string)null);
                });

            modelBuilder.Entity("FAP_BE.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("RoleId");

                    b.HasIndex(new[] { "Name" }, "UQ__Role__737584F67100069D")
                        .IsUnique();

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("FAP_BE.Models.Room", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Name")
                        .HasName("PK__Room__737584F77FEF8B19");

                    b.ToTable("Room", (string)null);
                });

            modelBuilder.Entity("FAP_BE.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.Property<string>("Room")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("Slot")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("InstructorId");

                    b.HasIndex("Room");

                    b.ToTable("Schedule", (string)null);
                });

            modelBuilder.Entity("FAP_BE.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MetaDataId")
                        .HasColumnType("int");

                    b.Property<string>("RoleNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "MetaDataId" }, "UQ__Student__429BA08C677A21C4")
                        .IsUnique();

                    b.HasIndex(new[] { "RoleNumber" }, "UQ__Student__486BE7499774320D")
                        .IsUnique();

                    b.ToTable("Student", (string)null);
                });

            modelBuilder.Entity("FAP_BE.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("ManageSlot")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(225)
                        .IsUnicode(false)
                        .HasColumnType("varchar(225)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "UQ__Subject__737584F671C70D24")
                        .IsUnique();

                    b.HasIndex(new[] { "Code" }, "UQ__Subject__A25C5AA7E5FEA18F")
                        .IsUnique();

                    b.ToTable("Subject", (string)null);
                });

            modelBuilder.Entity("FAP_BE.Models.Account", b =>
                {
                    b.HasOne("FAP_BE.Models.Metadata", "MetaData")
                        .WithOne("Account")
                        .HasForeignKey("FAP_BE.Models.Account", "MetaDataId")
                        .IsRequired()
                        .HasConstraintName("FK__Account__MetaDat__403A8C7D");

                    b.HasOne("FAP_BE.Models.Role", "Role")
                        .WithOne("Account")
                        .HasForeignKey("FAP_BE.Models.Account", "RoleId")
                        .IsRequired()
                        .HasConstraintName("FK__Account__RoleId__3F466844");

                    b.Navigation("MetaData");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("FAP_BE.Models.Attendance", b =>
                {
                    b.HasOne("FAP_BE.Models.Schedule", "Schedule")
                        .WithMany("Attendances")
                        .HasForeignKey("ScheduleId")
                        .IsRequired()
                        .HasConstraintName("FK__Attendanc__Sched__5BE2A6F2");

                    b.HasOne("FAP_BE.Models.Student", "Student")
                        .WithMany("Attendances")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("FK__Attendanc__Stude__5AEE82B9");

                    b.Navigation("Schedule");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("FAP_BE.Models.Course", b =>
                {
                    b.HasOne("FAP_BE.Models.Subject", "Subject")
                        .WithMany("Courses")
                        .HasForeignKey("SubjectId")
                        .IsRequired()
                        .HasConstraintName("FK__Course__TimeSlot__534D60F1");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("FAP_BE.Models.Instructor", b =>
                {
                    b.HasOne("FAP_BE.Models.Metadata", "MetaData")
                        .WithOne("Instructor")
                        .HasForeignKey("FAP_BE.Models.Instructor", "MetaDataId")
                        .IsRequired()
                        .HasConstraintName("FK__Instructo__MetaD__49C3F6B7");

                    b.Navigation("MetaData");
                });

            modelBuilder.Entity("FAP_BE.Models.Schedule", b =>
                {
                    b.HasOne("FAP_BE.Models.Course", "Course")
                        .WithMany("Schedules")
                        .HasForeignKey("CourseId")
                        .IsRequired()
                        .HasConstraintName("FK__Schedule__Course__5812160E");

                    b.HasOne("FAP_BE.Models.Instructor", "Instructor")
                        .WithMany("Schedules")
                        .HasForeignKey("InstructorId")
                        .IsRequired()
                        .HasConstraintName("FK__Schedule__Instru__5629CD9C");

                    b.HasOne("FAP_BE.Models.Room", "RoomNavigation")
                        .WithMany("Schedules")
                        .HasForeignKey("Room")
                        .IsRequired()
                        .HasConstraintName("FK__Schedule__Room__571DF1D5");

                    b.Navigation("Course");

                    b.Navigation("Instructor");

                    b.Navigation("RoomNavigation");
                });

            modelBuilder.Entity("FAP_BE.Models.Student", b =>
                {
                    b.HasOne("FAP_BE.Models.Metadata", "MetaData")
                        .WithOne("Student")
                        .HasForeignKey("FAP_BE.Models.Student", "MetaDataId")
                        .IsRequired()
                        .HasConstraintName("FK__Student__MetaDat__44FF419A");

                    b.Navigation("MetaData");
                });

            modelBuilder.Entity("FAP_BE.Models.Course", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("FAP_BE.Models.Instructor", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("FAP_BE.Models.Metadata", b =>
                {
                    b.Navigation("Account")
                        .IsRequired();

                    b.Navigation("Instructor")
                        .IsRequired();

                    b.Navigation("Student")
                        .IsRequired();
                });

            modelBuilder.Entity("FAP_BE.Models.Role", b =>
                {
                    b.Navigation("Account")
                        .IsRequired();
                });

            modelBuilder.Entity("FAP_BE.Models.Room", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("FAP_BE.Models.Schedule", b =>
                {
                    b.Navigation("Attendances");
                });

            modelBuilder.Entity("FAP_BE.Models.Student", b =>
                {
                    b.Navigation("Attendances");
                });

            modelBuilder.Entity("FAP_BE.Models.Subject", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}

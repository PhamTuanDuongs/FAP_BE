using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FAP_BE.Models
{
    public partial class FAP_PRN231Context : DbContext
    {
        public FAP_PRN231Context()
        {
        }

        public FAP_PRN231Context(DbContextOptions<FAP_PRN231Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Instructor> Instructors { get; set; } = null!;
        public virtual DbSet<Metadata> MetaData { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<StudentCourse> StudentCourse { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=DESKTOP-2RUM469;database=FAP_PRN231;uid=sa;pwd=123;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasIndex(e => e.MetaDataId, "UQ__Account__429BA08C8A1CBF00")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ__Account__536C85E4BFDD5503")
                    .IsUnique();
                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.MetaData)
                    .WithOne(p => p.Account)
                    .HasForeignKey<Account>(d => d.MetaDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account__MetaDat__403A8C7D");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account__RoleId__3F466844");
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.ScheduleId })
                    .HasName("PK__Attendan__BB0D8E2DD0F2A81A");

                entity.ToTable("Attendance");

                entity.Property(e => e.Comment).HasMaxLength(225);

                entity.Property(e => e.DateAttended).HasColumnType("datetime");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attendanc__Sched__5BE2A6F2");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attendanc__Stude__5AEE82B9");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.HasIndex(e => e.Code, "UQ__Course__A25C5AA7F742BF69")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");
                entity.Property(e => e.Room)
                .HasMaxLength(30)
                .IsUnicode(false);
                entity.Property(e => e.TimeSlot)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Course__TimeSlot__534D60F1");

                entity.HasOne(d => d.RoomNavigation)
                .WithMany(p => p.Courses)
                .HasForeignKey(d => d.Room)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Room");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.ToTable("Instructor");

                entity.HasIndex(e => e.InstructorCode, "UQ__Instruct__321792F655B5F98E")
                    .IsUnique();

                entity.HasIndex(e => e.MetaDataId, "UQ__Instruct__429BA08C5560F710")
                    .IsUnique();

                entity.Property(e => e.InstructorCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.MetaData)
                    .WithOne(p => p.Instructor)
                    .HasForeignKey<Instructor>(d => d.MetaDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Instructo__MetaD__49C3F6B7");
            });

            modelBuilder.Entity<Metadata>(entity =>
            {
                entity.HasKey(e => e.MetaDataId)
                    .HasName("PK__Meta_Dat__429BA08D26E459CC");

                entity.ToTable("Meta_Data");

                entity.HasIndex(e => e.Email, "UQ__Meta_Dat__A9D10534CC60EFE4")
                    .IsUnique();

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Image)
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.HasIndex(e => e.Name, "UQ__Role__737584F67100069D")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__Room__737584F77FEF8B19");

                entity.ToTable("Room");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Room)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Schedule__Course__5812160E");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Schedule__Instru__5629CD9C");

                entity.HasOne(d => d.RoomNavigation)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.Room)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Schedule__Room__571DF1D5");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.HasIndex(e => e.MetaDataId, "UQ__Student__429BA08C677A21C4")
                    .IsUnique();

                entity.HasIndex(e => e.RoleNumber, "UQ__Student__486BE7499774320D")
                    .IsUnique();

                entity.Property(e => e.RoleNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.MetaData)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.MetaDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Student__MetaDat__44FF419A");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.HasIndex(e => e.Name, "UQ__Subject__737584F671C70D24")
                    .IsUnique();

                entity.HasIndex(e => e.Code, "UQ__Subject__A25C5AA7E5FEA18F")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(225)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StudentCourse>().HasKey(ps => new { ps.StudentId, ps.CourseId });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

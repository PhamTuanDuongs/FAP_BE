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
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Instructor> Instructors { get; set; } = null!;
        public virtual DbSet<MetaData> MetaData { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Session> Sessions { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration config = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", true, true)
                              .Build();
                var strConn = config["ConnectionStrings:DB"];
                optionsBuilder.UseSqlServer(strConn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasIndex(e => e.MetaDataId, "UQ__Account__429BA08C9BA1BCD7")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ__Account__536C85E42BE88E6D")
                    .IsUnique();

                entity.HasIndex(e => e.RoleId, "UQ__Account__8AFACE1B459A1BEC")
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
                    .WithOne(p => p.Account)
                    .HasForeignKey<Account>(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account__RoleId__3F466844");
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => new { e.StudentCode, e.SessionId })
                    .HasName("PK__Attendan__0357CF2C6F301D40");

                entity.ToTable("Attendance");

                entity.Property(e => e.StudentCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DateAttended).HasColumnType("date");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attendanc__Sessi__60A75C0F");

                entity.HasOne(d => d.StudentCodeNavigation)
                    .WithMany(p => p.Attendances)
                    .HasPrincipalKey(p => p.RoleNumber)
                    .HasForeignKey(d => d.StudentCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attendanc__Stude__5FB337D6");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseCode)
                    .HasName("PK__Course__FC00E0017D59C0D9");

                entity.ToTable("Course");

                entity.HasIndex(e => e.Name, "UQ__Course__737584F628659E1F")
                    .IsUnique();

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.HasIndex(e => e.GroupCode, "UQ__Group__3B974380558A7AF0")
                    .IsUnique();

                entity.HasIndex(e => e.GroupName, "UQ__Group__6EFCD4340CECBECB")
                    .IsUnique();

                entity.Property(e => e.GroupCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.GroupName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.ToTable("Instructor");

                entity.HasIndex(e => e.InstructorCode, "UQ__Instruct__321792F6D430519B")
                    .IsUnique();

                entity.HasIndex(e => e.MetaDataId, "UQ__Instruct__429BA08C25A40C19")
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

            modelBuilder.Entity<MetaData>(entity =>
            {
                entity.HasKey(e => e.MetaDataId)
                    .HasName("PK__Meta_Dat__429BA08D047B6133");

                entity.ToTable("Meta_Data");

                entity.HasIndex(e => e.Email, "UQ__Meta_Dat__A9D1053407A8B194")
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

                entity.HasIndex(e => e.Name, "UQ__Role__737584F6C3D96F9C")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__Room__737584F74D076408");

                entity.ToTable("Room");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Session");

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.GroupCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.InstructorCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Room)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.CourseCodeNavigation)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.CourseCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__CourseC__5CD6CB2B");

                entity.HasOne(d => d.GroupCodeNavigation)
                    .WithMany(p => p.Sessions)
                    .HasPrincipalKey(p => p.GroupCode)
                    .HasForeignKey(d => d.GroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__GroupCo__59FA5E80");

                entity.HasOne(d => d.InstructorCodeNavigation)
                    .WithMany(p => p.Sessions)
                    .HasPrincipalKey(p => p.InstructorCode)
                    .HasForeignKey(d => d.InstructorCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__Instruc__5AEE82B9");

                entity.HasOne(d => d.RoomNavigation)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.Room)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__Room__5BE2A6F2");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.HasIndex(e => e.MetaDataId, "UQ__Student__429BA08C22452019")
                    .IsUnique();

                entity.HasIndex(e => e.RoleNumber, "UQ__Student__486BE74958C3D6D0")
                    .IsUnique();

                entity.Property(e => e.RoleNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.MetaData)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.MetaDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Student__MetaDat__44FF419A");

                entity.HasMany(d => d.Groups)
                    .WithMany(p => p.Students)
                    .UsingEntity<Dictionary<string, object>>(
                        "StudentGroup",
                        l => l.HasOne<Group>().WithMany().HasForeignKey("GroupId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Student_G__Group__5165187F"),
                        r => r.HasOne<Student>().WithMany().HasForeignKey("StudentId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Student_G__Stude__5070F446"),
                        j =>
                        {
                            j.HasKey("StudentId", "GroupId").HasName("PK__Student___838C84AFF527CB62");

                            j.ToTable("Student_Group");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

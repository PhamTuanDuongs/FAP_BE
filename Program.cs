using FAP_BE.DataAccess;
using FAP_BE.Mappings;
using FAP_BE.Models;
using FAP_BE.Repository;
using FAP_BE.Service;
using Microsoft.EntityFrameworkCore;

namespace FAP_BE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<FAP_PRN231Context>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();
            builder.Services.AddSingleton<ICourseRepository, CourseRepository>();
            builder.Services.AddSingleton<ITimetableRepository, TimtableRepository>();

            builder.Services.AddSingleton<IAttendancesRepository, AttendancesReponsitory>();
            //  builder.Services.AddScoped<AttendancesManagement>(); 

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000") // URL của frontend
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });


            //builder.Services.AddScoped< builder.Services., AttendancesManagement>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();   
            });
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

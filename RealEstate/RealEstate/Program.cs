
using Microsoft.EntityFrameworkCore;
using RealEstate.Middlewares;
using RealEstate.Models;

namespace RealEstate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<RealEstateDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection")));

            builder.Services.ConfigureJwtAuthenticationServices(builder.Configuration);
            builder.Services.AddRealEstateServices();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthorization();

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options => options
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod());


            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

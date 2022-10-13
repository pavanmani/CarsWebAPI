using System.Reflection;
using CARS.Data;
using CARS.Data.Repo;
using Microsoft.EntityFrameworkCore;

namespace CARS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddTransient<CompanySeeder>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<ICompRepository, CompRepository>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //builder.Services.AddDbContext<CarsAPIDbContext>(options=>options.UseInMemoryDatabase("CarsDb"));
            builder.Services.AddDbContext<CarsAPIDbContext>(cfg => 
            cfg.UseSqlServer(builder.Configuration.GetConnectionString("CarsAPIConnectionString")));

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            CreateDbIfNotExists(app);

            static void CreateDbIfNotExists(IApplicationBuilder app)
            {
                using var scope = app.ApplicationServices.CreateScope();
                var services = scope.ServiceProvider;
                try
                {
                    var seeder = scope.ServiceProvider.GetService<CompanySeeder>();
                    seeder.Seed();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
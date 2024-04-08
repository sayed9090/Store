using Microsoft.EntityFrameworkCore;
using Store.Core.Repository.Contract;
using Store.Repository;
using Store.Repository.Context;
using Store.Repository.Data;

namespace Store.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(
                option => {
                    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection"));
            });
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            var app = builder.Build();
            // Using use To Ask Clr remove Scope after Finised 
            using var Scope = app.Services.CreateScope();

            var Services = Scope.ServiceProvider;
            
            var _dbcontext = Services.GetRequiredService<StoreDbContext>();
            var loggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbcontext.Database.MigrateAsync(); // Update Database
                await StoreContextSeed.SeedAsync(_dbcontext,loggerFactory); // Data Seeding
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error has been occured during apply the migration ");
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

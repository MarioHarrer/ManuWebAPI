
using ManuWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ManuWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString)) { throw new Exception("Connection String wurde nicht gefunden!"); }

            builder.Services.AddControllers();

            // AddSwaggerGen() statt AddOpenApi() nutzen
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Eine Sql Lite datenbank verwenden
            builder.Services.AddDbContext<SpielerContext>(opt =>
            opt.UseSqlite(connectionString));


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SpielerContext>(); context.Database.EnsureCreated(); // Erstellt die Tabelle 'BarcaPlayers' in der .db Datei}


                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(options =>
                    {
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                        options.RoutePrefix = string.Empty;
                    });
                }

                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();
                app.Run();
            }

        }
    }
}

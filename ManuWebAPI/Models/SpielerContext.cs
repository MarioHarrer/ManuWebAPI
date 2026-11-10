using Microsoft.EntityFrameworkCore;

namespace ManuWebAPI.Models
{

    //Verwaltung der Verbindung zu Datenbankdatei //wandelt in sql befehl zum beispiel um also Insert into
    public class SpielerContext : DbContext
    {
        public SpielerContext(DbContextOptions<SpielerContext> options)
            : base(options)
        {
        }

        public DbSet<Spieler> Spielers { get; set; } = null!;
    }
}

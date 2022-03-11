using CleanAcrchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Data
{
    public class StreamerDbContext : DbContext
    {
        protected override async void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //cadena de conexion al servidor
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-2A71OD9\SQLEXPRESS01;Initial Catalog=Streamer;Integrated Security=True").LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information).EnableSensitiveDataLogging();
        }
        //convierto las clases como entidades
        //agrego el ? para que sea  nulo
        public DbSet<Streamer>? Streamers { get; set; }
        public DbSet<Video>? Videos { get; set; }
    }
}
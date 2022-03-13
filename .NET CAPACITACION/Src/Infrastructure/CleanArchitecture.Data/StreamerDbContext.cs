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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //de esta manera creamos la relación independientemente de los nombres asignados
            modelBuilder.Entity<Streamer>()
                        .HasMany(m => m.Videos)
                        .WithOne(m => m.Streamer)
                        .HasForeignKey(m => m.StreamerId)
                        .IsRequired()
                        //eliminación por cascada
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Video>()
                         .HasMany(p => p.Actores)
                         .WithMany(t => t.Videos)
                         .UsingEntity<VideoActor>(
                             pt => pt.HasKey(e => new { e.ActorId, e.VideoId })
                         );
        }
        //convierto las clases como entidades
        //agrego el ? para que sea  nulo
        public DbSet<Streamer>? Streamers { get; set; }
        public DbSet<Video>? Videos { get; set; }
        public DbSet<Actor>? Actores { get; set; }
        public DbSet<Director>? Directores { get; set; }
    }
}
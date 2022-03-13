using CleanAcrchitecture.Domain.Entities;
using CleanArchitecture.Data;
using Microsoft.EntityFrameworkCore;
//creamos una instancia del dbcontext
StreamerDbContext dbContext = new();

await AddNewDirectorWithVideo();
await AddNewActorWithVideo();
await AddNewStreamerWithVideoId();
//await AddNewStreamerWithVideo();
//await TrackingAndNotTraking();
//await QueryLinq();
//await QueryMethods();
//await QueryFilter();
//await QueryStreaming();
//await AddNewRecords();
Console.WriteLine("Presione cualquier tecla para terminar el programa");
Console.ReadKey();

async Task AddNewDirectorWithVideo()
{
    var Director = new Director
    {
        Nombre = "Lorenzo",
        Apellido = "Basteri",
        VideoId = 21
    };
    await dbContext.AddAsync(Director);
    await dbContext.SaveChangesAsync();
}
async Task AddNewActorWithVideo()
{
    var Actor = new Actor
    {
        Nombre = "Brad",
        Apellido = "Pitt"
    };

    await dbContext.AddAsync(Actor);
    await dbContext.SaveChangesAsync();

    var videoActor = new VideoActor
    {
        ActorId = Actor.Id,
        VideoId = 21
    };
    await dbContext.AddAsync(videoActor);
    await dbContext.SaveChangesAsync();
}
async Task AddNewStreamerWithVideoId()
{
    var BatmanForever = new Video
    {
        Nombre = "Batman Forever",
        StreamerId = 11
    };
    await dbContext.AddAsync(BatmanForever);
    await dbContext.SaveChangesAsync();
}
async Task AddNewStreamerWithVideo()
{
    var Pantaya = new Streamer
    {
        Nombre = "Pantalla"
    };
    var HungerGames = new Video
    {
        Nombre = "Hunger Games",
        Streamer = Pantaya
    };
    await dbContext.AddAsync(HungerGames);
    await dbContext.SaveChangesAsync();
}
async Task TrackingAndNotTraking()
{
    //permite hacer un get y actualizarlo
    var streamingWhitTracking = await dbContext!.Streamers!.FirstOrDefaultAsync(streamer => streamer.Id == 3);
    //permite hacer un get per o no actualizarlo, por eso es recomendable usarlo apra solo leer datos
    var streamingWhitNoTracking = await dbContext!.Streamers!.AsNoTracking().FirstOrDefaultAsync(streamer => streamer.Id == 4);

    streamingWhitTracking.Nombre = "Netflix Super";
    streamingWhitNoTracking.Nombre = "Amazon Super Videos";

    await dbContext.SaveChangesAsync();

}
async Task QueryLinq()
{
    Console.WriteLine("Ingrese el Servicio de Streaming:");
    var srtreamerNombre = Console.ReadLine();

    var streamers = await (from i in dbContext.Streamers
                           where EF.Functions.Like(i.Nombre, $"%{srtreamerNombre}%")
                           select i).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}
async Task QueryMethods()
{
    var streamer = dbContext!.Streamers!;
    //se buscara el primer dato que tenga el parametro(asume existencia de informacion y si no hay nada tirara una excepción)
    var firstAsync = await streamer.Where(streamer => streamer.Nombre.Contains("z")).FirstAsync();
    //se buscara el primer dato que tenga el parametro(si no hay nada devolvera null y no lanzara una excepción)
    var firstOrDefaultAsync = await streamer.Where(streamer => streamer.Nombre.Contains("z")).FirstOrDefaultAsync();
    var firstOrDefaultAsync2V = await streamer.FirstOrDefaultAsync(streamer => streamer.Nombre.Contains("z"));
    //si el get que realizo trae mas de un valor dara una excepción
    var singleAsync = await streamer.Where(streamer => streamer.Id == 3).SingleAsync();
    //siempre devolvera un valor y si no hay nda sera null
    var singleOrDefaultAsync = await streamer.Where(streamer => streamer.Id == 3).SingleOrDefaultAsync();
    var resultado = await streamer.FindAsync(3);
}
async Task QueryFilter()
{
    Console.WriteLine("Ingrese una compania de Streaming:");
    var streamingnombre = Console.ReadLine();

    //solo buscare resultados especificos
    var streamers = await dbContext!.Streamers!.Where(streamer => streamer!.Nombre!.Equals(streamingnombre)).ToListAsync();
    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }

    //buscare una parte del nombre si he olvidado algo
    //var streamerPartialResult = await dbContext!.Streamers!.Where(streamer => streamer!.Nombre!.Contains(streamingnombre)).ToListAsync();
    var streamerPartialResult = await dbContext!.Streamers!.Where(streamer => EF.Functions.Like(streamer.Nombre, $"%{ streamingnombre}%")).ToListAsync();

    foreach (var streamer in streamerPartialResult)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}
async Task QueryStreaming()
{
    var streamers = await dbContext!.Streamers!.ToListAsync();
    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}
async Task AddNewRecords()
{
    Streamer stremaer = new()
    {
        Nombre = "Amzon Prime",
        url = "https://www.amazonprime.com"
    };
    //agregamos un nuevo dato en la BD
    dbContext!.Streamers!.Add(stremaer);
    await dbContext.SaveChangesAsync();

    var movies = new List<Video>
    {
        new Video
        {
            Nombre = "Mad Max",
            StreamerId= stremaer.Id
        },
        new Video
        {
            Nombre = "Batman",
            StreamerId = stremaer.Id
        },
        new Video
        {
            Nombre = "Star Wars",
            StreamerId = stremaer.Id
        },
        new Video
        {
            Nombre = "Ella no te quiere XD",
            StreamerId = stremaer.Id
        }
    };
    //Agregamos un Conjunto de Datos en la BD
    await dbContext.AddRangeAsync(movies);
    await dbContext.SaveChangesAsync();
}
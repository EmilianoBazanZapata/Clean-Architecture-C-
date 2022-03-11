using CleanAcrchitecture.Domain.Entities;
using CleanArchitecture.Data;
using Microsoft.EntityFrameworkCore;
//creamos una instancia del dbcontext
StreamerDbContext dbContext = new();
await QueryFilter();
//await QueryStreaming();
//await AddNewRecords();
Console.WriteLine("Presione cualquier tecla para terminar el programa");
Console.ReadKey();

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
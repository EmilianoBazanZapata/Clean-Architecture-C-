using CleanAcrchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IVideoRepository : IAsyncRepository<Video>
    {
        //metodo para obtener UNA sola pelicula por su nombre
        Task<Video> GetVideobyName(string NameVideo);
        //metodo para buscar las peliculas registradas por un usuario en particular
        Task<IEnumerable<Video>> GetVideoByUserName(string UserName);
    }
}
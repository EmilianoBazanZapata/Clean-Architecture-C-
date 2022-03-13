using CleanAcrchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IVideoRepository : IAsyncRepository<Video>
    {
        Task<IEnumerable<Video>> GetVideobyName(string NameVideo);
    }
}
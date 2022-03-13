using AutoMapper;
using CleanAcrchitecture.Domain.Entities;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;

namespace CleanArchitecture.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Video, VideosVm>();
        }
    }
}
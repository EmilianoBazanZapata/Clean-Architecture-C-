using CleanAcrchitecture.Domain.Common;

namespace CleanAcrchitecture.Domain.Entities
{
    public class VideoActor : BaseDomainModel
    {
        public int VideoId { get; set; }
        public int ActorId { get; set; }
    }
}
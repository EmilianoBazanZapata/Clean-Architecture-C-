using CleanAcrchitecture.Domain.Common;

namespace CleanAcrchitecture.Domain.Entities
{
    public class Actor : BaseDomainModel
    {
        public Actor()
        {
            Videos = new HashSet<Video>();
        }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}
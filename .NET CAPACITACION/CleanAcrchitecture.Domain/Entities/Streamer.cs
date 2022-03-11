namespace CleanAcrchitecture.Domain.Entities
{
    public class Streamer
    {
        public int Id { get; set; }
        //seteamos un valor nulo
        public string? Nombre { get; set; } //= string.Empty;
        //acepta valores nulos
        public string? url { get; set; }
        public ICollection<Video>? Videos { get; set; }
    }
}
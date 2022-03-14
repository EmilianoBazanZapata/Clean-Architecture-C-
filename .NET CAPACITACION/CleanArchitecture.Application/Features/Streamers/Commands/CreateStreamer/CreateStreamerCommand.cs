using MediatR;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class StreamerCommand : IRequest<int>
    {
        //seteamos un valor nulo
        public string Nombre { get; set; } = string.Empty;
        //acepta valores nulos
        public string url { get; set; } = string.Empty;
    }
}
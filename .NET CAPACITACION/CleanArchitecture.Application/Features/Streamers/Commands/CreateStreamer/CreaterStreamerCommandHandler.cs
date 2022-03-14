using AutoMapper;
using CleanAcrchitecture.Domain.Entities;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;
namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandHandler : IRequestHandler<StreamerCommand, int>
    {
        private readonly IStreamerRepository _streamerRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateStreamerCommandHandler> _logger;

        public CreateStreamerCommandHandler(IStreamerRepository streamerRepository, IMapper mapper, IEmailService emailService, ILogger<CreateStreamerCommandHandler> logger)
        {
            _streamerRepository = streamerRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(StreamerCommand request, CancellationToken cancellationToken)
        {
            var streamertEntity = _mapper.Map<Streamer>(request);
            //agrego los valores en la BD
            var NewStreamer = await _streamerRepository.AddAsync(streamertEntity);
            _logger.LogInformation($"Streamer {NewStreamer.Id} fue creado Exitosamente");

            await SendEmail(NewStreamer);

            return NewStreamer.Id;
        }

        private async Task SendEmail(Streamer streamer)
        {
            var email = new Email
            {
                To = "vaxi.drez.social@gmail.com",
                Body = "La compania de Streamer se creo correctamente",
                Subject = "Mensaje de alerta"
            };
            try
            {
                await _emailService.SendEmail(email);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error al enviar el Email de {streamer.Id} - {streamer.Nombre}");
            }
        }
    }
}
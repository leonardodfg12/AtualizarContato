using AtualizarContato.Domain.Domain;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AtualizarContato.Application.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IBus _bus;
        private readonly ILogger<ContatoService> _logger;

        public ContatoService(IBus bus, ILogger<ContatoService> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async void EnviarContatoParaFila(ContactDomain? contatoDto)
        {
            if (contatoDto == null)
            {
                _logger.LogWarning("ContatoDto is null");
                return;
            }

            var mensagem = new ContactDomain
            {
                Id = contatoDto.Id,
                Name = contatoDto.Name,
                DDD = contatoDto.DDD,
                Phone = contatoDto.Phone,
                Email = contatoDto.Email
            };

            _logger.LogInformation("Sending message to queue: atualizar-contato-queue with Id: {MessageId}", mensagem.Id);

            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:atualizar-contato-queue"));
            await endpoint.Send(mensagem);

            _logger.LogInformation("Message sent successfully: {MessageId}", mensagem.Id);
        }
    }
}
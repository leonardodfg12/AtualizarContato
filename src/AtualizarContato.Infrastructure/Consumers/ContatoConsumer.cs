using AtualizarContato.Domain.Domain;
using AtualizarContato.Infrastructure.Data;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AtualizarContato.Infrastructure.Consumers;

public class ContatoConsumer : IConsumer<ContactDomain>
{
    private readonly ContactZoneDbContext _context;
    private readonly ILogger<ContatoConsumer> _logger;

    public ContatoConsumer(ContactZoneDbContext context, ILogger<ContatoConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ContactDomain> context)
    {
        var contatoMessage = context.Message;

        var contato = await _context.Contatos.FindAsync(contatoMessage.Id);
        if (contato == null)
        {
            _logger.LogWarning("Contato with Id {MessageId} not found", contatoMessage.Id);
            return;
        }

        contato.Name = contatoMessage.Name;
        contato.DDD = contatoMessage.DDD;
        contato.Phone = contatoMessage.Phone;
        contato.Email = contatoMessage.Email;

        _context.Contatos.Update(contato);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Contato with Id {MessageId} updated successfully", contatoMessage.Id);
    }
}
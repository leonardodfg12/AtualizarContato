using AtualizarContato.Domain.Domain;

namespace AtualizarContato.Application.Services
{
    public interface IContatoService
    {
        void EnviarContatoParaFila(ContactDomain? contato);
    }
}
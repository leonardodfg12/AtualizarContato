using AtualizarContato.Application.Services;
using AtualizarContato.Domain.Domain;
using AtualizarContato.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtualizarContato.API.Controllers;

[Route("api/")]
[ApiController]
public class AtualizarContatoController : ControllerBase
{
    private readonly IContatoService _contatoService;
    private readonly ContactZoneDbContext _context;

    public AtualizarContatoController(ContactZoneDbContext context, IContatoService contatoService)
    {
        _context = context;
        _contatoService = contatoService;
    }

    [HttpPost("atualizar-contato-mensageria/{id}")]
    public async Task<IActionResult> EnviarContatoParaAtualizar(int id, [FromBody] ContactDomain contatoAtualizado)
    {
        if (id != contatoAtualizado.Id)
            return BadRequest("ID do contato não corresponde.");

        var contato = await _context.Contatos.FirstOrDefaultAsync(x => x.Id == id);

        if (contato == null)
            return NotFound("Contato não encontrado.");

        contato.Name = contatoAtualizado.Name;
        contato.DDD = contatoAtualizado.DDD;
        contato.Phone = contatoAtualizado.Phone;
        contato.Email = contatoAtualizado.Email;
        
        _contatoService.EnviarContatoParaFila(contato);
        return Ok("Contato enviado para a fila para ser atualizado.");
    }
    
    [HttpPut("atualizar-contato-api/{id}")]
    public async Task<IActionResult> AtualizarContato(int id, [FromBody] ContactDomain contatoAtualizado)
    {
        if (id != contatoAtualizado.Id)
            return BadRequest("ID do contato não corresponde.");

        var contato = await _context.Contatos.FirstOrDefaultAsync(x => x.Id == id);

        if (contato == null)
            return NotFound("Contato não encontrado.");

        contato.Name = contatoAtualizado.Name;
        contato.DDD = contatoAtualizado.DDD;
        contato.Phone = contatoAtualizado.Phone;
        contato.Email = contatoAtualizado.Email;

        _context.Contatos.Update(contato);
        await _context.SaveChangesAsync();

        return Ok("Contato atualizado com sucesso.");
    }
    
    [HttpGet("listar-contatos")]
    public async Task<IActionResult> GetAllContatos()
    {
        var contatos = await _context.Contatos.ToListAsync();
        return Ok(contatos);
    }
}
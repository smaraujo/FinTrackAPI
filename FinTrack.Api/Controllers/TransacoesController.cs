using FinTrack.Api.DTOs;
using FinTrack.Api.Models;
using FinTrack.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController(TransacaoService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Transacao>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Transacao>>> Get(CancellationToken cancellationToken)
    {
        var transacoes = await service.ListarAsync(cancellationToken);
        return Ok(transacoes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Transacao>> GetById(int id, CancellationToken cancellationToken)
    {
        var transacao = await service.ObterPorIdAsync(id, cancellationToken);
        return transacao is null ? NotFound() : Ok(transacao);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Transacao), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Transacao>> Post(
        TransacaoRequest request,
        CancellationToken cancellationToken)
    {
        var resultado = await service.CriarAsync(request, cancellationToken);

        if (resultado.Erro is not null)
        {
            return NotFound(new { mensagem = resultado.Erro });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = resultado.Transacao!.Id },
            resultado.Transacao);
    }
}

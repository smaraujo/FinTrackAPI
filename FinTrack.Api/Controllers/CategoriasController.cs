using FinTrack.Api.Data;
using FinTrack.Api.DTOs;
using FinTrack.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Categoria>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Categoria>>> Get(CancellationToken cancellationToken)
    {
        var categorias = await context.Categorias
            .AsNoTracking()
            .OrderBy(c => c.Nome)
            .ToListAsync(cancellationToken);

        return Ok(categorias);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Categoria>> GetById(int id, CancellationToken cancellationToken)
    {
        var categoria = await context.Categorias
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return categoria is null ? NotFound() : Ok(categoria);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Categoria), StatusCodes.Status201Created)]
    public async Task<ActionResult<Categoria>> Post(
        CategoriaRequest request,
        CancellationToken cancellationToken)
    {
        var nome = request.Nome.Trim();
        var jaExiste = await context.Categorias
            .AnyAsync(c => c.Nome.ToLower() == nome.ToLower(), cancellationToken);

        if (jaExiste)
        {
            return Conflict(new { mensagem = "Já existe uma categoria com esse nome." });
        }

        var categoria = new Categoria
        {
            Nome = nome,
            Cor = request.Cor
        };

        context.Categorias.Add(categoria);
        await context.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
    }
}

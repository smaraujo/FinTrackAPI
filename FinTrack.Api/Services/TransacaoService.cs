using FinTrack.Api.Data;
using FinTrack.Api.DTOs;
using FinTrack.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Api.Services;

public class TransacaoService(AppDbContext context)
{
    public async Task<List<Transacao>> ListarAsync(CancellationToken cancellationToken = default)
    {
        return await context.Transacoes
            .AsNoTracking()
            .Include(t => t.Categoria)
            .OrderByDescending(t => t.Data)
            .ToListAsync(cancellationToken);
    }

    public async Task<Transacao?> ObterPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Transacoes
            .AsNoTracking()
            .Include(t => t.Categoria)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<(Transacao? Transacao, string? Erro)> CriarAsync(
        TransacaoRequest request,
        CancellationToken cancellationToken = default)
    {
        var categoriaExiste = await context.Categorias
            .AnyAsync(c => c.Id == request.CategoriaId, cancellationToken);

        if (!categoriaExiste)
        {
            return (null, "A categoria informada não existe.");
        }

        var transacao = new Transacao
        {
            Descricao = request.Descricao.Trim(),
            Valor = decimal.Round(request.Valor, 2),
            Tipo = request.Tipo,
            Data = request.Data,
            CategoriaId = request.CategoriaId
        };

        context.Transacoes.Add(transacao);
        await context.SaveChangesAsync(cancellationToken);

        await context.Entry(transacao).Reference(t => t.Categoria).LoadAsync(cancellationToken);
        return (transacao, null);
    }
}

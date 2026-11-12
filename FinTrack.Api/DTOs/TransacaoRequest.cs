using FinTrack.Api.Models;

namespace FinTrack.Api.DTOs;

public class TransacaoRequest
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public DateTime Data { get; set; }
    public int CategoriaId { get; set; }
}

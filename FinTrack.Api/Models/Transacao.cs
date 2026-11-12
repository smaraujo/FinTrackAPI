using System.ComponentModel.DataAnnotations;

namespace FinTrack.Api.Models;

public class Transacao
{
    public int Id { get; set; }

    [Required]
    [MaxLength(160)]
    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public TipoTransacao Tipo { get; set; }

    public DateTime Data { get; set; }

    public int CategoriaId { get; set; }

    public Categoria? Categoria { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace FinTrack.Api.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required]
    [MaxLength(80)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Cor { get; set; }

    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
}

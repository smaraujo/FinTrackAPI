using FinTrack.Api.DTOs;
using FinTrack.Api.Models;
using FinTrack.Api.Validators;

namespace FinTrack.Tests.Validators;

public class TransacaoRequestValidatorTests
{
    private readonly TransacaoRequestValidator _validator = new();

    [Fact]
    public void DeveSerValido_QuandoDadosEstaoCorretos()
    {
        var request = new TransacaoRequest
        {
            Descricao = "Salário",
            Valor = 5000m,
            Tipo = TipoTransacao.Receita,
            Data = new DateTime(2026, 11, 1),
            CategoriaId = 1
        };

        var resultado = _validator.Validate(request);

        Assert.True(resultado.IsValid);
    }

    [Fact]
    public void DeveSerInvalido_QuandoValorForZero()
    {
        var request = new TransacaoRequest
        {
            Descricao = "Compra",
            Valor = 0m,
            Tipo = TipoTransacao.Despesa,
            Data = DateTime.Today,
            CategoriaId = 1
        };

        var resultado = _validator.Validate(request);

        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.PropertyName == nameof(TransacaoRequest.Valor));
    }

    [Fact]
    public void DeveSerInvalido_QuandoDescricaoEstiverVazia()
    {
        var request = new TransacaoRequest
        {
            Descricao = " ",
            Valor = 10m,
            Tipo = TipoTransacao.Despesa,
            Data = DateTime.Today,
            CategoriaId = 1
        };

        var resultado = _validator.Validate(request);

        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.PropertyName == nameof(TransacaoRequest.Descricao));
    }
}

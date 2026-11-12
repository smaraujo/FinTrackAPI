using FinTrack.Api.DTOs;
using FinTrack.Api.Validators;

namespace FinTrack.Tests.Validators;

public class CategoriaRequestValidatorTests
{
    private readonly CategoriaRequestValidator _validator = new();

    [Fact]
    public void DeveSerValido_QuandoNomeForInformado()
    {
        var resultado = _validator.Validate(new CategoriaRequest { Nome = "Alimentação" });

        Assert.True(resultado.IsValid);
    }

    [Fact]
    public void DeveSerInvalido_QuandoNomeEstiverVazio()
    {
        var resultado = _validator.Validate(new CategoriaRequest { Nome = "" });

        Assert.False(resultado.IsValid);
    }
}

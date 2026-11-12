using FinTrack.Api.DTOs;
using FluentValidation;

namespace FinTrack.Api.Validators;

public class CategoriaRequestValidator : AbstractValidator<CategoriaRequest>
{
    public CategoriaRequestValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
            .MaximumLength(80).WithMessage("O nome deve ter no máximo 80 caracteres.");

        RuleFor(x => x.Cor)
            .MaximumLength(20).WithMessage("A cor deve ter no máximo 20 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Cor));
    }
}

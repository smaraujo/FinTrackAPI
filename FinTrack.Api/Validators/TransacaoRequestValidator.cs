using FinTrack.Api.DTOs;
using FinTrack.Api.Models;
using FluentValidation;

namespace FinTrack.Api.Validators;

public class TransacaoRequestValidator : AbstractValidator<TransacaoRequest>
{
    public TransacaoRequestValidator()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("A descrição é obrigatória.")
            .MaximumLength(160).WithMessage("A descrição deve ter no máximo 160 caracteres.");

        RuleFor(x => x.Valor)
            .GreaterThan(0).WithMessage("O valor deve ser maior que zero.")
            .PrecisionScale(12, 2, false).WithMessage("O valor deve ter no máximo duas casas decimais.");

        RuleFor(x => x.Tipo)
            .IsInEnum().WithMessage("O tipo deve ser Receita (1) ou Despesa (2).");

        RuleFor(x => x.Data)
            .NotEmpty().WithMessage("A data da transação é obrigatória.");

        RuleFor(x => x.CategoriaId)
            .GreaterThan(0).WithMessage("Informe uma categoria válida.");
    }
}

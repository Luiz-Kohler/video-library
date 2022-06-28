using Domain.Entities;
using FluentValidation;

namespace Domain.Common.Validators
{
    public class LocacoesValidator : AbstractValidator<Locacao>
    {
        public LocacoesValidator()
        {
            RuleFor(locacao => locacao.ClienteId).GreaterThan(0).WithMessage("Cliente deve ser valido");
            RuleFor(locacao => locacao.FilmeId).GreaterThan(0).WithMessage("Filme deve ser valido");
        }
    }
}

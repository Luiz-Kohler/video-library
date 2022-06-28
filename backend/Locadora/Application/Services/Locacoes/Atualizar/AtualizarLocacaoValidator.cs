using FluentValidation;

namespace Application.Services.Locacoes.Atualizar
{
    public class AtualizarLocacaoValidator : AbstractValidator<AtualizarLocacaoRequest>
    {
        public AtualizarLocacaoValidator()
        {
            {
                RuleFor(locacao => locacao.LocacaoId).GreaterThan(0).WithMessage("Locação deve ser valida");
                RuleFor(locacao => locacao.FilmeId).GreaterThan(0).WithMessage("Filme deve ser valido");
                RuleFor(locacao => locacao.ClienteId).GreaterThan(0).WithMessage("Cliente deve ser valido");
            }
        }
    }
}

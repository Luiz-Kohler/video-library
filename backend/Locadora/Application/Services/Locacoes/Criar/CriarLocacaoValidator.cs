using FluentValidation;

namespace Application.Services.Locacoes.Criar
{
    public class CriarLocacaoValidator : AbstractValidator<CriarLocacaoRequest>
    {
        public CriarLocacaoValidator()
        {
            RuleFor(locacao => locacao.FilmeId).GreaterThan(0).WithMessage("Filme deve ser valido");
            RuleFor(locacao => locacao.ClienteId).GreaterThan(0).WithMessage("Cliente deve ser valido");
        }
    }
}

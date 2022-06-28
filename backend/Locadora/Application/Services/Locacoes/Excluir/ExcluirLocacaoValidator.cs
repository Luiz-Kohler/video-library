using FluentValidation;

namespace Application.Services.Locacoes.Excluir
{
    public class ExcluirLocacaoValidator : AbstractValidator<ExcluirLocacaoRequest>
    {
        public ExcluirLocacaoValidator()
        {
            RuleFor(locacao => locacao.Id).GreaterThan(0).WithMessage("Locação deve ser valida");
        }
    }
}

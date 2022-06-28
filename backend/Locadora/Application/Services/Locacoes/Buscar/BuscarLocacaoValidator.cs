using FluentValidation;

namespace Application.Services.Locacoes.Buscar
{
    public class BuscarLocacaoValidator : AbstractValidator<BuscarLocacaoRequest>
    {
        public BuscarLocacaoValidator()
        {
            RuleFor(locacao => locacao.Id).GreaterThan(0).WithMessage("Locação deve ser valida");
        }
    }
}

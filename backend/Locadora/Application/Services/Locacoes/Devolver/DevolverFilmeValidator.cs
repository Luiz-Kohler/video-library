using FluentValidation;

namespace Application.Services.Locacoes.Devolver
{
    public class DevolverFilmeValidator : AbstractValidator<DevolverFilmeRequest>
    {
        public DevolverFilmeValidator()
        {
            RuleFor(locacao => locacao.Id).GreaterThan(0).WithMessage("Locação deve ser valida");
        }
    }
}

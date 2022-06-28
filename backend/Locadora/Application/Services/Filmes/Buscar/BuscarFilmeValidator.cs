using FluentValidation;

namespace Application.Services.Filmes.Buscar
{
    public class BuscarFilmeValidator : AbstractValidator<BuscarFilmeRequest>
    {
        public BuscarFilmeValidator()
        {
            RuleFor(filme => filme.Id).GreaterThan(0).WithMessage("Filme deve ser valido");
        }
    }
}

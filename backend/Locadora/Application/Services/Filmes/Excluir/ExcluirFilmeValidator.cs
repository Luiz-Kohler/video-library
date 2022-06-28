using FluentValidation;

namespace Application.Services.Filmes.Excluir
{
    public class ExcluirFilmeValidator : AbstractValidator<ExcluirFilmeRequest>
    {
        public ExcluirFilmeValidator()
        {
            RuleFor(filme => filme.Id).GreaterThan(0).WithMessage("Filme deve ser valido");
        }
    }
}

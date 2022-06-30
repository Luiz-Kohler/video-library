using FluentValidation;

namespace Application.Services.Filmes.Criar
{
    public class CriarFilmeValidator : AbstractValidator<CriarFilmeRequest>
    {
        public CriarFilmeValidator()
        {
            RuleFor(filme => filme.Id).GreaterThan(0).WithMessage("Filme deve ter id valido");
            RuleFor(filme => filme.Titulo).NotEmpty().Length(2, 100).WithMessage("Titulo deve conter de 2 a 100 caracteres");
        }
    }
}

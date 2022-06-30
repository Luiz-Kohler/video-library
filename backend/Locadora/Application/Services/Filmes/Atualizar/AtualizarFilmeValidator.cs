using FluentValidation;

namespace Application.Services.Filmes.Atualizar
{
    public class AtualizarFilmeValidator : AbstractValidator<AtualizarFilmeRequest>
    {
        public AtualizarFilmeValidator()
        {
            RuleFor(filme => filme.Id).GreaterThan(0).WithMessage("Filme deve ser valido");
            RuleFor(filme => filme.Titulo).NotEmpty().Length(2, 100).WithMessage("Titulo deve conter de 2 a 100 caracteres");
            RuleFor(filme => filme.Classificacao).NotEmpty().WithMessage("Titulo deve conter de 2 a 100 caracteres");
        }
    }
}

using Domain.Entities;
using FluentValidation;

namespace Domain.Common.Validators
{
    internal class FilmesValidator : AbstractValidator<Filme>
    {
        public FilmesValidator()
        {
            RuleFor(filme => filme.Titulo).NotEmpty().Length(3, 100).WithMessage("Titúlo deve conter de 3 a 100 caracteres");
            RuleFor(filme => filme.Classificacao).NotNull().WithMessage("Classificação deve ser informada");
            RuleFor(filme => filme.EhLancamento).NotNull().WithMessage("Informar se filme é lançamento");
        }
    }
}

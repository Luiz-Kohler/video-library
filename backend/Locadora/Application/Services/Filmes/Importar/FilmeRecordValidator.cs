using Domain.Common.Enums;
using FluentValidation;

namespace Application.Services.Filmes.Importar
{
    public class FilmeRecordValidator : AbstractValidator<FilmeRecord>
    {
        public FilmeRecordValidator()
        {
            RuleFor(filme => filme.Id).GreaterThan(0).WithMessage("Filme deve ter id valido");
            RuleFor(filme => filme.Titulo).NotEmpty().Length(2, 100).WithMessage("Titulo deve conter de 2 a 100 caracteres");
            RuleFor(filme => filme.Classificacao).NotEmpty().WithMessage("Classificacão deve ser valida");
        }
    }
}

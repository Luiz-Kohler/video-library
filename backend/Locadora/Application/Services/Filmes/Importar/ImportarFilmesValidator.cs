using FluentValidation;

namespace Application.Services.Filmes.Importar
{
    public class ImportarFilmesValidator : AbstractValidator<ImportarFilmesRequest>
    {
        public ImportarFilmesValidator()
        {
            RuleFor(filme => filme.ArquivoCsv).NotNull().WithMessage("Deve enviar arquivo csv");
        }
    }
}

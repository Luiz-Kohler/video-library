using CsvHelper.Configuration.Attributes;
using Domain.Common.Enums;

namespace Application.Services.Filmes.Importar
{
    public class FilmeRecord
    {
        [Name("Id")]
        public int Id { get; set; }
        [Name("Titulo")]
        public string Titulo { get; set; }
        [Name("ClassificacaoIndicativa")]
        public Classificacao? Classificacao { get; set; }
        [Name("Lancamento")]
        public bool Lancamento { get; set; }
    }
}

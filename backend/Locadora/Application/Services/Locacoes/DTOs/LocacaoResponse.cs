namespace Application.Services.Locacoes.DTOs
{
    public class LocacaoResponse
    {
        public int Id { get; set; }
        public ClienteForLocacao Cliente { get; set; }
        public FilmeForLocacao Filme { get; set; }
    }

    public class ClienteForLocacao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class FilmeForLocacao
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
    }
}

using MediatR;

namespace Application.Services.Locacoes.Devolver
{
    public class DevolverFilmeRequest : IRequest<DevolverFilmeResponse>
    {
        public int Id { get; set; }
    }
}

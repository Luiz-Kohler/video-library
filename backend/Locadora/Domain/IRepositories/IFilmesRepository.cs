using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IFilmesRepository : IBaseRepository<Filme>
    {
        Task Ativar(Filme filme);
    }
}

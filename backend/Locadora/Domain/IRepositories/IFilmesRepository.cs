using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.IRepositories
{
    public interface IFilmesRepository : IBaseRepository<Filme>
    {
        Task Ativar(Filme filme);
        Task<IList<Filme>> SelecionarVariasPorIncluindoLocacoes(Expression<Func<Filme, bool>> filtro = null);
        Task<Filme> SelecionarUmaPorIncluindoLocacoes(Expression<Func<Filme, bool>> filtro);
    }
}

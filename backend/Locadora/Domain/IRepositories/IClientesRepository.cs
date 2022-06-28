using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.IRepositories
{
    public interface IClientesRepository : IBaseRepository<Cliente>
    {
        Task<IList<Cliente>> SelecionarVariasPorIncluindoLocacoes(Expression<Func<Cliente, bool>> filtro = null);
        Task<Cliente> SelecionarUmaPorIncluindoLocacoes(Expression<Func<Cliente, bool>> filtro);
    }
}

using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.IRepositories
{
    public interface ILocacoesRepository : IBaseRepository<Locacao>
    {
        Task<IList<Locacao>> SelecionarVariasComRelacionamentos(Expression<Func<Locacao, bool>> filtro = null);
        Task<Locacao> SelecionarUmaComRelacionamentos(Expression<Func<Locacao, bool>> filtro);
    }
}

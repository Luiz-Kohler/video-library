using Domain.Common;
using System.Linq.Expressions;

namespace Domain.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity<TEntity>
    {
        Task Inserir(TEntity entidade);
        Task InserirVarios(IEnumerable<TEntity> entidades);
        Task Atualizar(TEntity entidade);
        Task Excluir(TEntity entidade);
        Task<IList<TEntity>> SelecionarVariasPor(Expression<Func<TEntity, bool>> filtro = null);
        Task<TEntity> SelecionarUmaPor(Expression<Func<TEntity, bool>> filtro);
    }
}

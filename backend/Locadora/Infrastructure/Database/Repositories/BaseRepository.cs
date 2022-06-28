using Domain.Common;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Database.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
    {
        protected readonly DatabaseContext Context;
        protected readonly DbSet<TEntity> Entity;

        protected BaseRepository(DatabaseContext context)
        {
            Context = context;
            Entity = Context.Set<TEntity>();
        }

        public Task Atualizar(TEntity entidade)
        {
            Entity.Update(entidade);
            return Task.CompletedTask;
        }

        public Task AtualizarVarios(IEnumerable<TEntity> entidades)
        {
            Entity.UpdateRange(entidades);
            return Task.CompletedTask;
        }

        public Task Excluir(TEntity entidade)
        {
            entidade.Desativar();
            return Atualizar(entidade);
        }

        public async Task Inserir(TEntity entidade)
        {
            await Entity.AddAsync(entidade);
        }

        public async Task InserirVarios(IEnumerable<TEntity> entidades)
        {
            await Entity.AddRangeAsync(entidades);
        }

        public async Task<TEntity> SelecionarUmaPor(Expression<Func<TEntity, bool>> filtro)
        {
            return await Entity.FirstOrDefaultAsync(filtro);
        }

        public async Task<IList<TEntity>> SelecionarVariasPor(Expression<Func<TEntity, bool>> filtro = null)
        {
            if (filtro == null)
                return await Entity.ToListAsync();

            return await Entity.Where(filtro).ToListAsync();
        }
    }
}

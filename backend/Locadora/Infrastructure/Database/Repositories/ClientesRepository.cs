using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Database.Repositories
{
    public class ClientesRepository : BaseRepository<Cliente>, IClientesRepository
    {
        public ClientesRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Cliente> SelecionarUmaPorIncluindoLocacoes(Expression<Func<Cliente, bool>> filtro)
        {
            return await Entity.Include(cliente => cliente.Locacoes).FirstOrDefaultAsync(filtro);
        }

        public async Task<IList<Cliente>> SelecionarVariasPorIncluindoLocacoes(Expression<Func<Cliente, bool>> filtro = null)
        {
            if (filtro == null)
                return await Entity.Include(cliente => cliente.Locacoes).ToListAsync();

            return await Entity.Include(cliente => cliente.Locacoes).Where(filtro).ToListAsync();
        }
    }
}

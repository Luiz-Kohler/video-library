using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Database.Repositories
{
    public class LocacoesRepository : BaseRepository<Locacao>, ILocacoesRepository
    {
        public LocacoesRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Locacao> SelecionarUmaComRelacionamentos(Expression<Func<Locacao, bool>> filtro)
        {
            return await Entity
                .Include(locacao => locacao.Filme)
                .Include(locacao => locacao.Cliente)
                .FirstOrDefaultAsync(filtro);
        }

        public async Task<IList<Locacao>> SelecionarVariasComRelacionamentos(Expression<Func<Locacao, bool>> filtro = null)
        {
            if (filtro == null)
                return await Entity
                    .Include(locacao => locacao.Filme)
                    .Include(locacao => locacao.Cliente)
                    .ToListAsync();

            return await Entity
                .Include(locacao => locacao.Filme)
                .Include(locacao => locacao.Cliente)
                .Where(filtro)
                .ToListAsync();
        }
    }
}

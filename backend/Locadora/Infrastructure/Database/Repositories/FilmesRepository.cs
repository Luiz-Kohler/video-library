using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Database.Repositories
{
    public class FilmesRepository : BaseRepository<Filme>, IFilmesRepository
    {
        public FilmesRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task Ativar(Filme filme)
        {
            filme.Ativar();
            await Atualizar(filme);
        }

        public async Task<IList<Filme>> SelecionarVariasPorIncluindoLocacoes(Expression<Func<Filme, bool>> filtro = null)
        {
            if (filtro == null)
                return await Entity.Include(filme => filme.Locacoes).ToListAsync();

            return await Entity.Include(filme => filme.Locacoes).Where(filtro).ToListAsync();
        }
    }
}

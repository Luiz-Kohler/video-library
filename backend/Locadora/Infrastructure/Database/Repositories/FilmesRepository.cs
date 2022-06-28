using Domain.Entities;
using Domain.IRepositories;

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
    }
}

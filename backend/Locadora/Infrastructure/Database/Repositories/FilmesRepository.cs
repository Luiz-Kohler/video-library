using Domain.Entities;
using Domain.IRepositories;

namespace Infrastructure.Database.Repositories
{
    public class FilmesRepository : BaseRepository<Filme>, IFilmesRepository
    {
        public FilmesRepository(DatabaseContext context) : base(context)
        {
        }
    }
}

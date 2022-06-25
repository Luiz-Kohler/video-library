using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Common
{
    public interface IBaseMapping
    {
        void MapearEntidade(ModelBuilder modelBuilder);
    }
}

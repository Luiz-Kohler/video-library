using Application.Common.UnitOfWork;
using Infrastructure.Database.Contexts;

namespace Infrastructure.Database.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _dbContext;

        public UnitOfWork(IScopedDatabaseContext scopedContext)
        {
            _dbContext = scopedContext.Context;
        }

        public async Task<IUnitOfWorkTransaction> BeginTransaction()
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            return new UnitOfWorkTransaction(_dbContext, transaction);
        }
    }
}

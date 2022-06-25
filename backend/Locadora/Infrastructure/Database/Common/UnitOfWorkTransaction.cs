using Application.Common.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Database.Common
{
    public class UnitOfWorkTransaction : IUnitOfWorkTransaction
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IDbContextTransaction _transaction;

        public UnitOfWorkTransaction(DatabaseContext databaseContext, IDbContextTransaction transaction)
        {
            _databaseContext = databaseContext;
            _transaction = transaction;
        }

        public async Task CommitAsync()
        {
            await _databaseContext.SaveChangesAsync();
            await _transaction.CommitAsync();
        }

        public Task Rollback()
        {
            Dispose();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _transaction.Dispose();
            }
        }

        public bool Healthy()
        {
            return _databaseContext.Database.CanConnect();
        }
    }
}

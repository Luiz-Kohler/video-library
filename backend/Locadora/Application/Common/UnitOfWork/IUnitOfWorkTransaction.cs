namespace Application.Common.UnitOfWork
{
    public interface IUnitOfWorkTransaction : IDisposable
    {
        Task CommitAsync();
        Task Rollback();
    }
}

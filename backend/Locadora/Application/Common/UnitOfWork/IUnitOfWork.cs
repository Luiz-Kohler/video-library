namespace Application.Common.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<IUnitOfWorkTransaction> BeginTransaction();
    }
}

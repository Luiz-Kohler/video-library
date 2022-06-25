namespace Application.Common.UnitOfWork
{
    public interface IRollbackAction
    {
        void Execute();
    }
}

namespace Application.Common.UnitOfWork
{
    public interface IRollbackActionsExecuter
    {
        void ExecuteActions();
    }
}

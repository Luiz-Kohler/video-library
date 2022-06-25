namespace Application.Common.UnitOfWork
{
    public class RollbackActionsExecuter : IRollbackActionsExecuter
    {
        private readonly IList<IRollbackAction> _rollbackActions;

        public RollbackActionsExecuter(IEnumerable<IRollbackAction> rollbackActions)
        {
            _rollbackActions = rollbackActions.ToArray();
        }

        public void ExecuteActions()
        {
            foreach (var action in _rollbackActions)
            {
                action.Execute();
            }
        }
    }
}

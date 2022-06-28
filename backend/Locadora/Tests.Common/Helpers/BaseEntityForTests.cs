using Domain.Common;

namespace Tests.Common.Helpers
{
    public class BaseEntityForTests : BaseEntity<BaseEntityForTests>
    {
        public BaseEntityForTests() 
            : base()
        {
        }
    }
}

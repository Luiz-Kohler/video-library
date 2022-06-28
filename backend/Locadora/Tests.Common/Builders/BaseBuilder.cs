using Bogus;

namespace Tests.Common.Builders
{
    public abstract class BaseBuilder<T> where T : BaseBuilder<T>
    {
        protected readonly Faker _faker = new Faker();
    }
}

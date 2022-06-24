using Bogus;

namespace Tests.Common.Builders
{
    public abstract class BaseBuilder<T> where T : BaseBuilder<T>
    {
        protected readonly Faker _faker = new Faker();
        protected string _criadoPor;

        protected T CriadoPor(string criadoPor)
        {
            _criadoPor = criadoPor;
            return (T)this;
        } 
    }
}

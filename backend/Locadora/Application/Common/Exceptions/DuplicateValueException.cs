namespace Application.Common.Exceptions
{
    [Serializable]
    public class DuplicateValueException : Exception
    {
        public DuplicateValueException(string message) : base(message)
        {

        }
    }
}

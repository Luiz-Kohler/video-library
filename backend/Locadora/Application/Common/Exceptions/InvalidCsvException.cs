namespace Application.Common.Exceptions
{
    [Serializable]
    public class InvalidCsvException : Exception
    {
        public InvalidCsvException() : base("Arquivo CSV Inválido")
        {

        }
    }
}

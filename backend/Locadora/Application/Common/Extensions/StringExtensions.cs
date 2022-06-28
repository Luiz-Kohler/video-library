namespace Application.Common.Extensions
{
    public static class StringExtensions
    {
        public static string FormatCpf(this string cpf)
        {
            return cpf.Replace(".", "").Replace("-", "").Replace(" ", "");
        }
    }
}

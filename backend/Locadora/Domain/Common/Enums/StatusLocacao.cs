using System.ComponentModel;

namespace Domain.Common.Enums
{
    public enum StatusLocacao
    {
        [Description("Em andamento")]
        Andamento,
        [Description("Devolvido")]
        Devolvido,
        [Description("Devolvido com atraso")]
        DevolvidoComAtraso,
        [Description("Atrasado")]
        Atrasado
    }
}

using FluentValidation;

namespace Application.Services.Clientes.Excluir
{
    public class ExcluirClienteValidator : AbstractValidator<ExcluirClienteRequest>
    {
        public ExcluirClienteValidator()
        {
            RuleFor(cliente => cliente.Id).GreaterThan(0).WithMessage("Cliente deve ser valido");
        }
    }
}

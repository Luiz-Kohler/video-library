using FluentValidation;

namespace Application.Services.Clientes.Buscar
{
    public class BuscarClienteValidator : AbstractValidator<BuscarClienteRequest>
    {
        public BuscarClienteValidator()
        {
            RuleFor(cliente => cliente.Id).GreaterThan(0).WithMessage("Cliente deve ser valido");
        }
    }
}

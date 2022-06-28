using Application.Common.Extensions;
using FluentValidation;

namespace Application.Services.Clientes.Atualizar
{
    public class AtualizarClienteValidator : AbstractValidator<AtualizarClienteRequest>
    {
        public AtualizarClienteValidator()
        {
            RuleFor(cliente => cliente.Id).GreaterThan(0).WithMessage("Cliente deve ser valido");
            RuleFor(cliente => cliente.Nome).NotEmpty().Length(3, 100).WithMessage("Nome deve conter de 3 a 100 caracteres");
            RuleFor(cliente => cliente.DataNascimento).InclusiveBetween(new DateTime(1900, 1, 1), DateTime.UtcNow).WithMessage("Data de nascimento deve ser valida");
        }
    }
}

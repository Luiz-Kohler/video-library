using Domain.Entities;
using FluentValidation;

namespace Domain.Common.Validators
{
    public class ClientesValidator : AbstractValidator<Cliente>
    {
        public ClientesValidator()
        {
            RuleFor(cliente => cliente.Cpf).NotEmpty().Length(11).WithMessage("CPF deve ser valido");
            RuleFor(cliente => cliente.Nome).NotEmpty().Length(3, 100).WithMessage("Nome deve conter de 3 a 100 caracteres");
            RuleFor(cliente => cliente.DataNascimento).InclusiveBetween(new DateTime(1900, 1, 1), DateTime.UtcNow).WithMessage("Data de nascimento deve ser valida");
        }
    }
}

using Application.Common.Extensions;
using FluentValidation;

namespace Application.Services.Clientes.Criar
{
    public class CriarClienteValidator : AbstractValidator<CriarClienteRequest>
    {
        public CriarClienteValidator()
        {
            RuleFor(cliente => cliente.Cpf.FormatCpf()).Length(11).WithMessage("CPF deve ser valido");
            RuleFor(cliente => cliente.Nome).NotEmpty().Length(3, 100).WithMessage("Nome deve conter de 3 a 100 caracteres");
            RuleFor(cliente => cliente.DataNascimento).InclusiveBetween(new DateTime(1900, 1, 1), DateTime.UtcNow).WithMessage("Data de nascimento deve ser valida");
        }
    }
}

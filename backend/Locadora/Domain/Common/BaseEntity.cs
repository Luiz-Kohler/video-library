using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common
{
    public abstract class BaseEntity<TEntity> where TEntity : class
    {
        public int Id { get; }
        public DateTime CriadoEm { get; private init; }
        public DateTime? UltimaAtualizacaoEm { get; protected set; }
        public bool EhAtivo { get; private set; }

        [NotMapped]
        public List<ValidationFailure> Erros { get; protected set; }
        public bool IsValid() => !Erros.Any();
        public bool IsInvalid() => Erros.Any();

        public BaseEntity()
        {
            CriadoEm = DateTime.UtcNow;
            EhAtivo = true;
        }

        public void Desativar()
        {
            EhAtivo = false;
        }
        public void Ativar()
        {
            EhAtivo = true;
        }

        public void AtualizarEntidadeBase()
        {
            UltimaAtualizacaoEm = DateTime.UtcNow;
        }

        public virtual void Validar<TEntityValidator>() where TEntityValidator : AbstractValidator<TEntity>
        {
            var validator = Activator.CreateInstance(typeof(TEntityValidator)) as AbstractValidator<TEntity>;

            if (validator is null)
                throw new Exception("Classe de validação invalida");

            Erros = validator.Validate(this as TEntity).Errors;
        }
    }
}

using API.DTOs;
using API.Models;
using FluentValidation;

namespace API.Validators
{
    public class ContatoCreateDtoValidator : AbstractValidator<ContatoCreateDTO>
    {
        public ContatoCreateDtoValidator()
        {
            RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MinimumLength(3).WithMessage("Nome deve ter pelo menos 3 caracteres.");

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório.")
            .EmailAddress().WithMessage("Email inválido.");

            RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("Telefone é obrigatório.")
            .Matches(@"^\d{10,11}$").WithMessage("Telefone deve conter 10 ou 11 dígitos.");
        }
    }
}

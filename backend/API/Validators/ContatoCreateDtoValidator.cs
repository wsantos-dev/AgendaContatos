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
            .NotEmpty().WithMessage("Email é obrigatório.");
         
            RuleFor(x => x.Email)
    .       Matches(@"^[a-zA-Z0-9]+(\.[a-zA-Z0-9]+)+@[a-zA-Z0-9]+(\.[a-zA-Z0-9]+)+$")
            .WithMessage("Email inválido. O e-mail deve estar no formato nome.sobrenome@dominio.com.br");


            RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("Telefone é obrigatório.")
            .Matches(@"^\d{10,11}$").WithMessage("Telefone deve conter 10 ou 11 dígitos.");

            RuleFor(x => x.Telefone)
            .Matches(@"^(1[1-9]|2[1-9]|3[1-9]|4[1-9]|5[1-9]|6[1-9]|7[1-9]|8[1-9]|9[1-9])\d{8,9}$")
            .WithMessage("Telefone deve começar com um DDD válido e conter 10 ou 11 dígitos.");
        }
    }
}

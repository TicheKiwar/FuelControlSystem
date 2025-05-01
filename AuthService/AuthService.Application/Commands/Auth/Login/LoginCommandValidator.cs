using FluentValidation;

namespace AuthService.AuthService.Application.Commands.Auth.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El nombre de usuario es requerido")
                .MinimumLength(4).WithMessage("Debe tener al menos 4 caracteres");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida")
                .MinimumLength(6).WithMessage("Debe tener al menos 6 caracteres");
        }
    }
}

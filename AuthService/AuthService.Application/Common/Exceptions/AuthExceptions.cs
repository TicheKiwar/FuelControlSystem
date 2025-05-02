namespace AuthService.AuthService.Application.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }
    }

    public class InvalidCredentialsException : UnauthorizedException
    {
        public InvalidCredentialsException() : base("Credenciales inválidas") { }
    }

    public class AccountLockedException : UnauthorizedException
    {
        public AccountLockedException() : base("Cuenta bloqueada temporalmente") { }
    }

    public class TokenExpiredException : UnauthorizedException
    {
        public TokenExpiredException() : base("Token expirado") { }
    }
}
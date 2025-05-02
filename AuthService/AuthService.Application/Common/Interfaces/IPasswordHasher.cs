namespace AuthService.AuthService.Application.Common.Interfaces
{
    public interface IPasswordHasher
    {
        (byte[] Hash, byte[] Salt) CreateHash(string password);
        bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt);
    }
}

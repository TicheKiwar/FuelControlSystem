using DriverServices.Infrastructure.Services.Client;

namespace DriverServices.App.Interfaces
{
    public interface IUserClient : IDisposable
    {
        Task<UserDto> GetUserAsync(string userId);
    }

}

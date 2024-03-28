using RealEstate.Models.Dtos.UserDtos;
using RealEstate.Models.Entities;

namespace RealEstate.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task RegisterAsync(UserRegistrationDto userRegistrationDto);

        Task<User> LoginAsync(UserLoginDto userLoginDto);

        Task<UserDto?> GetUserAsync(string email);
    }
}

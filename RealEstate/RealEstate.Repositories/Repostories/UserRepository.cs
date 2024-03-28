using Microsoft.EntityFrameworkCore;
using RealEstate.Models;
using RealEstate.Models.CustomExceptions;
using RealEstate.Models.Dtos.UserDtos;
using RealEstate.Models.Entities;
using RealEstate.Repositories.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace RealEstate.Repositories.Repostories
{
    public class UserRepository : IUserRepository
    {
        private readonly RealEstateDbContext context;

        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public UserRepository(RealEstateDbContext context)
        {
            this.context = context;
        }

        public async Task<User> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await context.Users
                .SingleOrDefaultAsync(u => u.Username == userLoginDto.Username);

            if (user == null)
            {
                throw new NotFoundException("Невалиден потребител");
            }

            bool isPasswordValid = VerifyPassword(
                userLoginDto.Password,
                user.PasswordHash,
                Convert.FromHexString(user.PasswordSalt));

            if (!isPasswordValid)
            {
                throw new NotFoundException("Невалиден потребител");
            }

            return user;
        }

        public async Task RegisterAsync(UserRegistrationDto userRegistrationDto)
        {
            bool userExists = await context.Users
                .SingleOrDefaultAsync(u => u.Username == userRegistrationDto.Username) != null
                    ? true
                    : false;

            if (userExists)
            {
                throw new BadRequestException("Потребител с този имейл вече съществува");
            }

            var hashedPassword = HashPasword(userRegistrationDto.Password, out var salt);

            var user = new User()
            {
                Username = userRegistrationDto.Username,
                PasswordHash = hashedPassword,
                PasswordSalt = Convert.ToHexString(salt),
                Role = userRegistrationDto.Role
            };

            await context.AddAsync(user);
            await context.SaveChangesAsync();

        }

        public async Task<UserDto?> GetUserAsync(string username)
        {
            var user = await context.Users
                .Where(u => u.Username == username)
                .Select(u => new UserDto()
                {
                    Id = u.Id,
                    Username = u.Username,
                    Role = u.Role
                })
                .SingleOrDefaultAsync();

            return user;
        }

        private string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }

        private bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}

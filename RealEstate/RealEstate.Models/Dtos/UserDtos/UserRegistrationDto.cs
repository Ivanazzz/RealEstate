using RealEstate.Models.Enums;

namespace RealEstate.Models.Dtos.UserDtos
{
    public class UserRegistrationDto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }
    }
}

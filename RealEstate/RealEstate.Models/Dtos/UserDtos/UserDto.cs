using RealEstate.Models.Enums;

namespace RealEstate.Models.Dtos.UserDtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public Role Role { get; set; }
    }
}

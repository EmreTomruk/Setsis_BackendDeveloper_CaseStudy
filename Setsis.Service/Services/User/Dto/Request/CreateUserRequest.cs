using System.ComponentModel.DataAnnotations;

namespace Setsis.Service.Services.User.Dto.Request
{
    public class CreateUserRequest
    {
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

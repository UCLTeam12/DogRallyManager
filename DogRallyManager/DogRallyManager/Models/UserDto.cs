using System.ComponentModel.DataAnnotations;

namespace DogRallyManager.Models
{
    public class UserDto
    {

        public string UserName { get; set; } = string.Empty;
        private string Password { get; set; } = string.Empty;

    }
}

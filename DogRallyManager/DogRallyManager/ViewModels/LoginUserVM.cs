using System.ComponentModel.DataAnnotations;

namespace DogRallyManager.ViewModels
{
    public class LoginUserVM
    {
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public bool RememberMe { get; set; } = false;
    }
}

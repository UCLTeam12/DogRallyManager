using System.ComponentModel.DataAnnotations;

namespace DogRallyManager.ViewModels
{
    public class RegisterUserVM
    {
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set;} = string.Empty;


        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The entered passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}

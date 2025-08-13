using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress, Required]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), MinLength(8)]
        public string Password { get; set; }
    }
}

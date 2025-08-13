using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.ViewModels
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; }
        [Required, StringLength(15, MinimumLength = 3), Display(Name = "Last Name")]
        public string LastName { get; set; }
        [EmailAddress, Required]
        [Remote("CheckEmailExistance", "Users", ErrorMessage = "The Email is already Existed !")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), MinLength(8)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare("Password"), Display(Name = "Confirm Password"), NotMapped]
        public string ConfirmedPassword { get; set; }
        [DataType(DataType.PhoneNumber), Required]
        [RegularExpression(@"01[012][0-9]{8}", ErrorMessage = "Ex: 01234567890")]
        public string Phone { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        public string Address { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApplication2.Models
{
    public class Client
    {
        [Key]
        public int? Client_Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone")]
        public string? Phone { get; set; }


        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Address")]
        public string? Address { get; set; }


        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string? City { get; set; }


        [Required(ErrorMessage = "State is required")]
        [Display(Name = "State")]
        public string? State { get; set; }


        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country")]
        public string? Country { get; set; }
    }

}
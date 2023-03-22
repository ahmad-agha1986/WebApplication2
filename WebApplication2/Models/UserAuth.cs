
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class UserAuth
    {

        [Key]
        public int? Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string? ConfirmPassword { get; set; }

        [Display(Name = "User Name")]
        public string? UserName { get; set; }



        [Display(Name = "Password Hash")]
        public string? PasswordHash { get; set; }


        public ICollection<UserRoles> UserRole { get; set; }
        public virtual ICollection<User>? User { get; set; }



    }



}

using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public int UserId { get; set; }

        public string UserEmail { get; set; }

    }
}

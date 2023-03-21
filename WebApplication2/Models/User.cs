
using System.ComponentModel.DataAnnotations;


namespace WebApplication2.Models
{
    public class User
    {
        [Key]
        public int? User_Id { get; set; }

        [Required(ErrorMessage = "Job title is required")]
        public string? Job_Title { get; set; }


        // [Required(ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }


        // [Required(ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }




        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? Phone { get; set; }




        [Required(ErrorMessage = "Registration date is required")]
        public DateTime? Registration_date { get; set; }

        // [Required(ErrorMessage = "Please indicate whether the user is on leave or not.")]
        public bool? OnLeave { get; set; }

    }
}
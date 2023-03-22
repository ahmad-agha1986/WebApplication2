
using System.ComponentModel.DataAnnotations;


namespace WebApplication2.Models
{
    public class User
    {
        [Key]
        public int? User_Id { get; set; }

        [Required(ErrorMessage = "Job title is required")]
        public string? Job_Title { get; set; }


       
        public string? FirstName { get; set; }


        
        public string? LastName { get; set; }




        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? Phone { get; set; }




        [Required(ErrorMessage = "Registration date is required")]
        public DateTime? Registration_date { get; set; }

       
        public bool? OnLeave { get; set; }

    }
}
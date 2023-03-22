using System.ComponentModel.DataAnnotations;


namespace WebApplication2.Models

{
    public class Roles
    {
        [Key]
        public int? Id { get; set; }
        public string? Name { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}

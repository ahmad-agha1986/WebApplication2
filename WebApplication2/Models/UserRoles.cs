using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models

{
    public class UserRoles
    {
        [Key]
        public int UserRoleId { get; set; }

        public int UserAuthId { get; set; }


        public int RoleId { get; set; }



        public UserAuth UserAuth { get; set; }

        public Roles Role { get; set; }


    }
}

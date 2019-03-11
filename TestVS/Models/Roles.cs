using System;
using System.ComponentModel.DataAnnotations;

namespace TestVS.Models
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }

        [Required]
        public String Description { get; set; }


        public Users Users { get; set; }
    }
}

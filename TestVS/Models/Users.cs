using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestVS.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public static implicit operator Users(List<string> v)
        {
            throw new NotImplementedException();
        }
    }
}

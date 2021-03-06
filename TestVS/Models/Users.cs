﻿using System;
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

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

    }
}

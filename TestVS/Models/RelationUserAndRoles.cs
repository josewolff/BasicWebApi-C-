using System;
using System.ComponentModel.DataAnnotations;

namespace TestVS.Models
{

    public class RelationUserAndRoles
    {

        public Users Users { get; set; }

        public Roles Roles { get; set; }

    }
}

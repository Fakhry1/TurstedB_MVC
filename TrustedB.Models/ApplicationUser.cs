﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrustedB.Models
{
    public class ApplicationUser : IdentityUser
    {
              
        [Required]
        [DisplayName("User Name")]
        public string Name { get; set; }

        [DisplayName("Phone")]
        [ValidateNever]
        public string? Phone { get; set; }

        [DisplayName("Role in Tariqa")]
        [ValidateNever]
        public string? TariqaRole { get; set; }

        [DisplayName("Note")]
        [ValidateNever]
        public string? Note { get; set; }

        [DisplayName("Role")]
        [ValidateNever]
        public string? Role { get; set; }

        [NotMapped]
        public string RoleId { get; set; }
        [NotMapped]
        public string RoleName { get; set; }

    }

}

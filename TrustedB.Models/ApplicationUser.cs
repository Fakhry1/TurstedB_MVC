using Microsoft.AspNetCore.Identity;
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

        //[DisplayName("Role")]
        //[NotMapped]
        //public string Role { get; set; }

        [DisplayName("Note")]
        [ValidateNever]
        public string? Note { get; set; }

    }

}

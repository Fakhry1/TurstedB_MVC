using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace TrustedB.Models
{
    public class StateTransition
    {
        [Key]
        public int StateTransitionId { get; set; }

        [DisplayName("State from")]
        [ValidateNever]
        public int? Statefrom { get; set; }

        [DisplayName("State to")]
        [ValidateNever]
        public int? Stateto { get; set; }

        public Guid? RoleId { get; set; }

    }

}

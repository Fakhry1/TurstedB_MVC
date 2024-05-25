using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrustedB.Models
{
    public class StateHistory
    {
        [Key]
        public Guid StateHistoryId { get; set; }

        [DisplayName("State")]
        [ValidateNever]
        public string? State { get; set; }

        [Required]
        public Guid TopicId { get; set; }
        [ForeignKey("TopicId")]
        [ValidateNever]
        public Topics? Topics { get; set; }

        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }

        public string? StateSetDate { get; set; }

        //public TimeOnly StateTime { get; set; }


    }

}

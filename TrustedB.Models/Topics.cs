using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrustedB.Models
{
    public class Topics
    {
        [Key]
        public Guid TopicId { get; set; }
        [Required]
        [DisplayName("Topic Titel")]
        public string? Titel { get; set; }

        [DisplayName("Topic Discription")]
        [ValidateNever]
        public string? TopicDiscription { get; set; }

        [DisplayName("Topic Classification")]
        [ValidateNever]
        public string? TopicClassification { get; set; }

        [DisplayName("State")]
        [ValidateNever]
        public int? stateId { get; set; }
        [ForeignKey("stateId")]
        public TopicsStates? TopicsStates { get; set; }

        [DisplayName("Active")]
        [ValidateNever]
        public string? Active { get; set; }

        [DisplayName("Creation Date")]
        [ValidateNever]
        public string? CreationDate { get; set; }

        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }

    }

}

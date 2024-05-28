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

        public int? CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category? Category { get; set; }

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

        [DisplayName("User")]
        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }

    }

}

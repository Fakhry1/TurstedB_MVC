using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrustedB.Models
{
    public class Attachments
    {
        [Key]
        public Guid FileId { get; set; }

        [DisplayName("File Name")]
        [ValidateNever]
        public string? FileName { get; set; }

        [DisplayName("File Path")]
        [ValidateNever]
        public string? FilePath { get; set; }

        [DisplayName("File Type")]
        [ValidateNever]
        public string? FileType { get; set; }

        [DisplayName("Topic")]
        public Guid? TopicId { get; set; }
        [ForeignKey("TopicId")]
        [ValidateNever]
        public Topics? Topic { get; set; }

        [DisplayName("User")]
        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }

        [DisplayName("Creation Date")]
        public string? AttachmentSetDate { get; set; }

        [DisplayName("State")]
        [ValidateNever]
        public int? stateId { get; set; }
        [ForeignKey("stateId")]
        public TopicsStates? TopicsStates { get; set; }

        //public TimeOnly StateTime { get; set; }


    }

}

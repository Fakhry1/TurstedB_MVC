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

        [ValidateNever]
        public string? FilePath { get; set; }

        [ValidateNever]
        public string? FileType { get; set; } 

        public Guid? TopicId { get; set; }
        [ForeignKey("TopicId")]
        [ValidateNever]
        public Topics? Topic { get; set; }
        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }

        public string? AttachmentSetDate { get; set; }

        //public TimeOnly StateTime { get; set; }


    }

}

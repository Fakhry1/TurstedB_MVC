using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrustedB.Models
{
    public class CommentHistory
    {
        [Key]
        public Guid CommentHistoryId { get; set; }

        [DisplayName("Comment")]
        [ValidateNever]
        public string Comment { get; set; }
        [Required]
        public Guid TopicId { get; set; }
        [ForeignKey("TopicId")]
        [ValidateNever]
        public Topics? Topic { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }

        public string CommentSetDate { get; set; }

        //public TimeOnly StateTime { get; set; }


    }

}

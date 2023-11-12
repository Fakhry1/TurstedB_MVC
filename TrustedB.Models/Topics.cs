using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrustedB.Models
{
    public class Topics
    {
        [Key]
        public Guid TopicId { get; set; }
       
        [Required]
        [DisplayName("Topic Titel")]
        public string Titel { get; set; }

        [DisplayName("Topic Discription")]
        public string TopicDiscription { get; set; }

        [DisplayName("Topic Classification")]
        public string TopicClassification { get; set; }

        [DisplayName("State")]
        public string State { get; set; }

        //file name
        [DisplayName("Topic File")]
        [ValidateNever]
        public string TopicFile { get; set; }

        [DisplayName("File Size")]
        public string FileSize { get; set; }

        [DisplayName("Creation Date")]
        public DateOnly CreationDate { get; set; }

    }

}

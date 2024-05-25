using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrustedB.Models
{
    public class TopicsStates
    {
        [Key]
        public int stateId { get; set; }

        
        [DisplayName("Arabic Name")]
        public string? ArabicName { get; set; }

        [DisplayName("English Name")]
        public string? EnglishName { get; set; }

        
    }

}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrustedB.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        
        [DisplayName("Arabic Name")]
        public string? ArabicName { get; set; }

        [DisplayName("English Name")]
        public string? EnglishName { get; set; }

        
    }

}

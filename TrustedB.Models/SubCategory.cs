using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrustedB.Models
{
    public class SubCategory
    {
        [Key]
        public int SubCategoryId { get; set; }

        
        [DisplayName("Arabic Name")]
        public string? SubArabicName { get; set; }

        [DisplayName("English Name")]
        public string? SubEnglishName { get; set; }

        public int? CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category? Category { get; set; }


    }

}

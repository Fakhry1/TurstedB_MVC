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
        [ValidateNever]
        public string? SubArabicName { get; set; }

        [DisplayName("English Name")]
        [ValidateNever]
        public string? SubEnglishName { get; set; }

        [ValidateNever]
        public int? CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category? Category { get; set; }


    }

}

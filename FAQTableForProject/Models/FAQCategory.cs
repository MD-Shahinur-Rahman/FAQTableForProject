using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Collections.ObjectModel;

namespace FAQTableForProject.Models
{
    [Table(nameof(FAQCategory))]
    public class FAQCategory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int FAQCategoryId { get; set; }
        [Required, NotNull]
        public string FAQCategoryName { get; set; } 
        public Collection<FAQ>FAQs { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAQTableForProject.Models
{
    [Table(nameof(FAQ))]
    public class FAQ
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public int FAQId { get; set; }
        
        public string Question { get; set; } = null!;   
        public string Answer { get; set;} = null!;
        [ForeignKey(nameof(FAQCategory.FAQCategoryId))]
        public int FAQCategoryId { get; set; }
        public FAQCategory? FAQCategory { get; set; }

    }
}

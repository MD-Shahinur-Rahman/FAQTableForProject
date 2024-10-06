using System.ComponentModel.DataAnnotations;

namespace FAQTableForProject.Models.DTOs
{
    public class FAQCategoryDto
    {
        public int FAQCategoryId { get; set; }
        public string FAQCategoryName { get; set; }
        public ICollection<FAQDto> FAQs { get; set; }
    }

    public class FAQDto
    {
        public int FAQId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int FAQCategoryId { get; set; }
    }
}
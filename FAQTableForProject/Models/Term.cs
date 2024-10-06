using FAQTableForProject.Models;

public class Term
{
    public int TermId { get; set; }

    public int PartnerId { get; set; }

    public virtual  Partner Partner { get; set; } = null!;
    public string Title { get; set; }

    public string TermDescription { get; set; }
}
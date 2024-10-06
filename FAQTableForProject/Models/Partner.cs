
namespace FAQTableForProject.Models;

public class Partner
{
    public int PartnerId { get; set; }
    public string PartnerName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string ContactNumber { get; set; } = null!;
    public string City { get; set; } = null!;
    public int PostCode { get; set; }
    public string? PhotoName { get; set; }
    public DateTime AgreementSignDate { get; set; }
    public DateTime AgreementEndDate { get; set; }
    public decimal AgreementTotal { get; set; }
    public bool IsPaid { get; set; }
    public virtual ICollection<Term> Terms { get; set; } = new List<Term>();



}

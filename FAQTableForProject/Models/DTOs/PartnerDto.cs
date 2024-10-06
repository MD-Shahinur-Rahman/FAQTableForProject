using System.ComponentModel.DataAnnotations;

namespace FAQTableForProject.Models.DTOs;

public class PartnerDto
{
    public int PartnerId { get; set; }
    public string PartnerName { get; set; }
    public string Email { get; set; }
    public string ContactNumber { get; set; }
    public string City { get; set; }
    public int PostCode { get; set; }
    public string? PhotoName { get; set; }
    public IFormFile? PhotoFile { get; set; }
    public DateTime AgreementSignDate { get; set; }
    public DateTime AgreementEndDate { get; set; }
    public decimal AgreementTotal { get; set; }
    public bool IsPaid { get; set; }
    public string Terms { get; set; } //stringfy
}

using Microsoft.AspNetCore.Mvc;
using FAQTableForProject.Models;
using FAQTableForProject.Models.DTOs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FAQTableForProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartnersController : ControllerBase
    {
        private readonly FAQDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        public PartnersController(FAQDbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }

        // GET: api/Partners
        [HttpGet]
        public IActionResult GetAllPartners()
        {
            var partners = _dbContext.Partners
                .Include(p => p.Terms)
                .ToList();

            string jsonString = JsonConvert.SerializeObject(partners, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Content(jsonString, "application/json");
        }

        // GET: api/Partners/5
        [HttpGet("{partnerId}")]
        public IActionResult GetPartnerById(int partnerId)
        {
            var partner = _dbContext.Partners
                .Include(p => p.Terms)
                .FirstOrDefault(p => p.PartnerId == partnerId);
            if (partner == null)
            {
                return NotFound();
            }
            string jsonString = JsonConvert.SerializeObject(partner, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Content(jsonString, "application/json");
        }

        private string GetUploadfile(PartnerDto models)
        {
            string UniqeFile = null;
            if (models.PhotoFile != null)
            {
                string uploadFolder = Path.Combine(_env.WebRootPath, "images");
                UniqeFile = Guid.NewGuid().ToString() + Path.GetExtension(models.PhotoFile.FileName);
                string filePath = Path.Combine(uploadFolder, UniqeFile);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    models.PhotoFile.CopyTo(filestream);
                }
            }
            return UniqeFile;
        }

        [HttpPost]
        public async Task<IActionResult> PostPartner(PartnerDto partnerDto)
        {
            string imageUrl = GetUploadfile(partnerDto);
            Partner partner = new Partner
            {
                PhotoName = partnerDto.PhotoFile != null ? imageUrl : null,
                PartnerName = partnerDto.PartnerName,
                Email = partnerDto.Email,
                ContactNumber = partnerDto.ContactNumber,
                City = partnerDto.City,
                PostCode = partnerDto.PostCode,
                AgreementSignDate = partnerDto.AgreementSignDate,
                AgreementEndDate = partnerDto.AgreementEndDate,
                AgreementTotal = partnerDto.AgreementTotal,
                IsPaid = partnerDto.IsPaid
            };

            _dbContext.Partners.Add(partner);
            await _dbContext.SaveChangesAsync();

            if (partnerDto.Terms != null && !string.IsNullOrEmpty(partnerDto.Terms))
            {
                try
                {
                    List<Term> list = JsonConvert.DeserializeObject<List<Term>>(partnerDto.Terms);

                    if (list != null && list.Count > 0)
                    {
                        foreach (Term term in list)
                        {
                            Term termObj = new Term
                            {
                                PartnerId = partner.PartnerId, // Use partner instead of partnerObj
                                Title = term.Title,
                                TermDescription = term.TermDescription,
                            };
                            _dbContext.Terms.Add(termObj);
                        }
                        await _dbContext.SaveChangesAsync();
                    }
                }
                catch (JsonSerializationException ex)
                {
                    return BadRequest($"Error deserializing terms: {ex.Message}");
                }
            }

            return Ok("Partner saved successfully");
        }
        // PUT: api/Partners/5
        [HttpPut("{partnerId}")]
        public async Task<IActionResult> UpdatePartner(int partnerId, [FromForm] PartnerDto partnerDto)
        {
            var partnerObj = await _dbContext.Partners.FirstOrDefaultAsync(p => p.PartnerId == partnerId);
           
             if (partnerObj == null)
            {
                return NotFound("Partner not found");
            }

            string imageUrl = GetUploadfile(partnerDto);
            // Update Partner fields
            partnerObj.PartnerId     = partnerId;
            partnerObj.PartnerName = partnerDto.PartnerName;
            partnerObj.Email = partnerDto.Email;
            partnerObj.ContactNumber = partnerDto.ContactNumber;
            partnerObj.City = partnerDto.City;
            partnerObj.PostCode = partnerDto.PostCode;
            partnerObj.AgreementSignDate = partnerDto.AgreementSignDate;
            partnerObj.AgreementEndDate = partnerDto.AgreementEndDate;
            partnerObj.AgreementTotal = partnerDto.AgreementTotal;
            partnerObj.IsPaid = partnerDto.IsPaid;
            partnerObj.PhotoName = partnerDto.PhotoName;


            // Remove existing terms
            var existingTerms = _dbContext.Terms.Where(e => e.PartnerId == partnerId);

            if (existingTerms.Any())
            {
                _dbContext.RemoveRange(existingTerms);
            }

           

            

            JsonSerializer serializer = new JsonSerializer();
           
            var list = JsonConvert.DeserializeObject<List<TermDto>>(partnerDto.Terms);

            if (list != null && list.Count > 0)
            {
                foreach (var term in list)
                {
                    Term termObj = new Term
                    {
                        PartnerId = partnerObj.PartnerId,
                        Title = term.Title,
                        TermDescription = term.TermDescription
                    };
                    _dbContext.Terms.Add(termObj);
                }
            }
            await _dbContext.SaveChangesAsync();
            return Ok("Partner updated successfully");
        }

        // DELETE: api/Partners/5
        [HttpDelete("{partnerId}")]
        public async Task<IActionResult> DeletePartner(int partnerId)
        {
            var partnerObj = await _dbContext.Partners.FindAsync(partnerId);
            if (partnerObj == null)
            {
                return NotFound("Partner not found");
            }

            var existingTerms = _dbContext.Terms.Where(t => t.PartnerId == partnerId);
            if (existingTerms.Any())
            {
                _dbContext.RemoveRange(existingTerms);
            }

            _dbContext.Partners.Remove(partnerObj);
            await _dbContext.SaveChangesAsync();
            return Ok("Partner deleted successfully");
        }
    }
}
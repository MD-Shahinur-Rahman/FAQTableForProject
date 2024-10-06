using Microsoft.AspNetCore.Mvc;
using FAQTableForProject.Models;
using FAQTableForProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FAQTableForProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FAQCategoriesController : ControllerBase
    {
        private readonly FAQDbContext _dbContext;

        public FAQCategoriesController(FAQDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/FAQCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FAQCategoryDto>>> GetFAQCategories()
        {
            var faqCategories = await _dbContext.FAQsCategory
                .Include(fc => fc.FAQs)
                .ToListAsync();

            var faqCategoryDtos = faqCategories.Select(fc => new FAQCategoryDto
            {
                FAQCategoryId = fc.FAQCategoryId,
                FAQCategoryName = fc.FAQCategoryName,
                FAQs = fc.FAQs.Select(f => new FAQDto
                {
                    FAQId = f.FAQId,
                    Question = f.Question,
                    Answer = f.Answer,
                    FAQCategoryId = f.FAQCategoryId
                }).ToList()
            });

            return Ok(faqCategoryDtos);
        }

        // GET: api/FAQCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FAQCategoryDto>> GetFAQCategory(int id)
        {
            var faqCategory = await _dbContext.FAQsCategory
                .Include(fc => fc.FAQs)
                .FirstOrDefaultAsync(fc => fc.FAQCategoryId == id);

            if (faqCategory == null)
            {
                return NotFound();
            }

            var faqCategoryDto = new FAQCategoryDto
            {
                FAQCategoryId = faqCategory.FAQCategoryId,
                FAQCategoryName = faqCategory.FAQCategoryName,
                FAQs = faqCategory.FAQs.Select(f => new FAQDto
                {
                    FAQId = f.FAQId,
                    Question = f.Question,
                    Answer = f.Answer,
                    FAQCategoryId = f.FAQCategoryId
                }).ToList()
            };

            return Ok(faqCategoryDto);
        }

        // POST: api/FAQCategories
        [HttpPost]
        public async Task<ActionResult<FAQCategoryDto>> PostFAQCategory(FAQCategoryDto faqCategoryDto)
        {
            var faqCategory = new FAQCategory
            {
                FAQCategoryName = faqCategoryDto.FAQCategoryName
            };

            _dbContext.FAQsCategory.Add(faqCategory);
            await _dbContext.SaveChangesAsync();

            if (faqCategoryDto.FAQs != null)
            {
                foreach (var faqDto in faqCategoryDto.FAQs)
                {
                    var faq = new FAQ
                    {
                        Question = faqDto.Question,
                        Answer = faqDto.Answer,
                        FAQCategoryId = faqCategory.FAQCategoryId
                    };

                    _dbContext.FAQs.Add(faq);
                }

                await _dbContext.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetFAQCategory), new { id = faqCategory.FAQCategoryId }, faqCategoryDto);
        }

        // PUT: api/FAQCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFAQCategory(int id, FAQCategoryDto faqCategoryDto)
        {
            if (id != faqCategoryDto.FAQCategoryId)
            {
                return BadRequest("FAQ Category ID does not match");
            }

            var faqCategory = await _dbContext.FAQsCategory
                .Include(fc => fc.FAQs)
                .FirstOrDefaultAsync(fc => fc.FAQCategoryId == id);

            if (faqCategory == null)
            {
                return NotFound();
            }

            faqCategory.FAQCategoryName = faqCategoryDto.FAQCategoryName;

            if (faqCategoryDto.FAQs != null)
            {
                // Remove existing FAQs
                var existingFAQs = _dbContext.FAQs.Where(f => f.FAQCategoryId == faqCategory.FAQCategoryId);
                _dbContext.FAQs.RemoveRange(existingFAQs);

                // Add new FAQs
                foreach (var faqDto in faqCategoryDto.FAQs)
                {
                    var faq = new FAQ
                    {
                        Question = faqDto.Question,
                        Answer = faqDto.Answer,
                        FAQCategoryId = faqCategory.FAQCategoryId
                    };

                    _dbContext.FAQs.Add(faq);
                }
            }

            await _dbContext.SaveChangesAsync();

            return Ok("FAQ Category updated successfully");
        }

        // DELETE: api/FAQCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFAQCategory(int id)
        {
            var faqCategory = await _dbContext.FAQsCategory
                .Include(fc => fc.FAQs)
                .FirstOrDefaultAsync(fc => fc.FAQCategoryId == id);

            if (faqCategory == null)
            {
                return NotFound();
            }

            _dbContext.FAQsCategory.Remove(faqCategory);
            await _dbContext.SaveChangesAsync();

            return Ok("FAQ Category deleted successfully");
        }
    }
}
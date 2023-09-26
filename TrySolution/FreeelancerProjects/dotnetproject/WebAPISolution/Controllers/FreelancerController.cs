using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreDBFirst.Models;

namespace BookStoreDBFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancerController : ControllerBase
    {
        private readonly FreelancerProjectDbContext _context;

        public FreelancerController(FreelancerProjectDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Freelancer>>> GetAllFreelancers()
        {
            var freelancers = await _context.Freelancers.ToListAsync();
            return Ok(freelancers);
        }
[HttpGet("FreelancerNames")]
public async Task<ActionResult<IEnumerable<string>>> Get()
{
    // Project the FreelancerTitle property using Select
    var FreelancerNames = await _context.Freelancers
        .OrderBy(x => x.FreelancerName)
        .Select(x => x.FreelancerName)
        .ToListAsync();

    return FreelancerNames;
}
        [HttpPost]
        public async Task<ActionResult> AddFreelancer(Freelancer freelancer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return detailed validation errors
            }
            await _context.Freelancers.AddAsync(freelancer);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFreelancer(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Freelancer id");

            var freelancer = await _context.Freelancers.FindAsync(id);
              _context.Freelancers.Remove(freelancer);
                await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

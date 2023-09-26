using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreDBFirst.Models;

namespace BookStoreDBFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly FreelancerProjectDbContext _context;

        public ProjectController(FreelancerProjectDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            var projects = await _context.Projects.ToListAsync();
            return Ok(projects);
        }

        [HttpPost]
        public async Task<ActionResult> AddProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return detailed validation errors
            }
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Project id");

            var project = await _context.Projects.FindAsync(id);
              _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

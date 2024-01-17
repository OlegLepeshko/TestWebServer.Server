using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TestWebServer.Server.Data;
using TestWebServer.Shared;

namespace TestWebServer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubElementController : ControllerBase
    {
        private readonly DataContext _context;  

        public SubElementController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SubElement>>> GetAllSubElements()
        {
            var subElement = await _context.SubElements.ToListAsync();

            return Ok(subElement);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubElement>> GetSubElement(int id)
        {
            var subElement = await _context.SubElements.FindAsync(id);
            if (subElement is null)
                return NotFound("SubElement not found");

            return Ok(subElement);
        }

        [HttpPost]
        public async Task<ActionResult<List<SubElement>>> AddSubElement([FromBody]SubElement subElement)
        {
            var newSubElement = await _context.SubElements.AddAsync(subElement);
            await _context.SaveChangesAsync();

            return Ok(newSubElement);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<List<SubElement>>> UpdateSubElement(int Id, SubElement updatedSubElement)
        {
            var dbSubElement = await _context.SubElements.FindAsync(Id);

            if (dbSubElement is null)
            {
                return NotFound("SubElement not found");
            }

            dbSubElement.WindowId = updatedSubElement.WindowId;
            dbSubElement.Element = updatedSubElement.Element;
            dbSubElement.Type = updatedSubElement.Type;
            dbSubElement.Width = updatedSubElement.Width;
            dbSubElement.Height = updatedSubElement.Height;

            var subElement = await _context.SaveChangesAsync();

            return Ok(dbSubElement);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<SubElement>>> DeleteSubElement(int id)
        {
            var dbSubElement = await _context.SubElements.FindAsync(id);
            if (dbSubElement is null)
                return NotFound("SubElement not found");

            var deletedSubElement = _context.SubElements.Remove(dbSubElement);

            await _context.SaveChangesAsync();

            return Ok(deletedSubElement);
        }
    }
}

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
    public class WindowController : ControllerBase
    {
        private readonly DataContext _context;  

        public WindowController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Window>>> GetAllWindows()
        {
            var windows = await _context.Windows.ToListAsync();

            return Ok(windows);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Window>> GetWindow(int id)
        {
            var window = await _context.Windows.FindAsync(id);
            if (window is null)
                return NotFound("Window not found");

            return Ok(window);
        }

        [HttpPost]
        public async Task<ActionResult<List<Window>>> AddWindow([FromBody]Window window)
        {
            var newWindow = await _context.Windows.AddAsync(window);
            await _context.SaveChangesAsync();

            return Ok(newWindow);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<List<Window>>> UpdateWindow(int Id, Window updatedWindow)
        {
            var dbWindow = await _context.Windows.FindAsync(Id);

            if (dbWindow is null)
            {
                return NotFound("Window not found");
            }

            dbWindow.Name = updatedWindow.Name;
            dbWindow.OrderId = updatedWindow.OrderId;
            dbWindow.Name = updatedWindow.Name;
            dbWindow.QuantityOfWindows = updatedWindow.QuantityOfWindows;
            dbWindow.TotalSubElements = updatedWindow.TotalSubElements;

            var window = await _context.SaveChangesAsync();

            return Ok(dbWindow);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<Window>>> DeleteWindow(int id)
        {
            var dbWindow = await _context.Windows.FindAsync(id);
            if (dbWindow is null)
                return NotFound("Window not found");

            var deletedWindow = _context.Windows.Remove(dbWindow);

            await _context.SaveChangesAsync();

            return Ok(deletedWindow);
        }
    }
}

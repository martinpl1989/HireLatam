using Microsoft.AspNetCore.Mvc;
using MartinAngularProject;
using Microsoft.EntityFrameworkCore;

namespace MartinAngularProject.Controllers;

[ApiController]
[Route("[controller]")]
public class ListController : ControllerBase
{
    private readonly AppDbContext _context;
    public ListController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Items
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetItems()
    {
        return await _context.Items.ToListAsync();
    }

    // GET: api/Items/1
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Item>>> GetItem(int id)
    {
        return await _context.Items.Where(a=>a.Id==id).ToListAsync();
    }


    // POST: api/Items
    [HttpPost]
    public async Task<ActionResult<Item>> PostItem(Item item)
    {
        _context.Items.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
    }

    // PUT: api/Items/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutItem(int id, Item item)
    {
        if (id != item.Id && item.Id>0)
        {
            return BadRequest();
        }

        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Items/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Item>> DeleteItem(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
        return item;
    }

}



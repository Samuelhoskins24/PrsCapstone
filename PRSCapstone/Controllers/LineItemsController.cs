using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using PRSCapstone.Models;

namespace PRSCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineItemsController : ControllerBase
    {
        private readonly PRSContext _context;

        public LineItemsController(PRSContext context)
        {
            _context = context;
        }

        // GET: api/LineItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LineItems>>> GetLineItem()
        {
            return await _context.LineItem.ToListAsync();
        }

        // GET: api/LineItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LineItems>> GetLineItems(int id)
        {
            var lineItems = await _context.LineItem.FindAsync(id);

            if (lineItems == null)
            {
                return NotFound();
            }

            return lineItems;
        }

        [HttpGet("lines-for-req/{reqid}")]

        public async Task<ActionResult<object>> GetReqIdLi(int reqid)
        {

            var li = await _context.LineItem.Where(l => l.RequestId == reqid)
                                            .Include(r => r.Request)
                                            .ToListAsync();

            return li;

        }

        // PUT: api/LineItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLineItems(int id, LineItems lineItems)
        {
            if (id != lineItems.Id)
            {
                return BadRequest();
            }

            decimal test3 = _context.LineItem
                            .Where(rl => rl.RequestId == id)
                            .Include(rl => rl.Product)
                            .Select(rl => new { linetotal = rl.Quantity * rl.Product.Price })
                            .Sum(s => s.linetotal);


                _context.Entry(lineItems).State = EntityState.Modified;
                await _context.SaveChangesAsync();


            return NoContent();
        }



        // POST: api/LineItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LineItems>> PostLineItems(LineItems lineItems)
        {
            _context.LineItem.Add(lineItems);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLineItems", new { id = lineItems.Id }, lineItems);
        }

        // DELETE: api/LineItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLineItems(int id)
        {
            var lineItems = await _context.LineItem.FindAsync(id);
            if (lineItems == null)
            {
                return NotFound();
            }

            decimal test3 = _context.LineItem
                .Where(rl => rl.RequestId == id)
                .Include(rl => rl.Product)
                .Select(rl => new { linetotal = rl.Quantity * rl.Product.Price })
                .Sum(s => s.linetotal);

            _context.LineItem.Remove(lineItems);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LineItemsExists(int id)
        {
            return _context.LineItem.Any(e => e.Id == id);
        }
    }
}

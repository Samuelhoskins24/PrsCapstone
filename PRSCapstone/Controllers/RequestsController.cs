using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using NuGet.Protocol.Plugins;
using PRSCapstone.Models;

namespace PRSCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly PRSContext _context;

        public RequestsController(PRSContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requests>>> GetRequest()
        {
            return await _context.Request.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Requests>> GetRequests(int id)
        {
            var requests = await _context.Request.FindAsync(id);

            if (requests == null)
            {
                return NotFound();
            }

            return requests;
        }

        [HttpGet("list-review/{userid}")]

        public async Task<ActionResult<object>> GetREVIEWRequests(int userid)
        {

            var requests = await _context.Request.Where(r => r.Status == "REVIEW" && r.UserID != userid).ToListAsync();

            return requests;
        }

        private string getNextRequestNumber()
        {

            // requestNumber format: R2409230011 

            // 11 chars, 'R' + YYMMDD + 4 digit # w/ leading zeros 

            string requestNbr = "R";

            // add YYMMDD string 

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            requestNbr += today.ToString("yyMMdd");

            // get maximum request number from db 

            string maxReqNbr = _context.Request.Max(r => r.RequestNumber);

            String reqNbr = "";

            if (maxReqNbr != null)
            {

                // get last 4 characters, convert to number 

                String tempNbr = maxReqNbr.Substring(7);

                int nbr = Int32.Parse(tempNbr);

                nbr++;

                // pad w/ leading zeros 

                reqNbr += nbr;

                reqNbr = reqNbr.PadLeft(4, '0');

            }

            else
            {

                reqNbr = "0001";

            }

            requestNbr += reqNbr;

            return requestNbr;
        }


        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequests(int id, Requests requests)
        {
            if (id != requests.Id)
            {
                return BadRequest();
            }

            _context.Entry(requests).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut("submit-REVIEW/{id}")]

        public async Task<ActionResult<Requests>> PutRequestREVIEW(int id,[FromBody] Requests req)
        {
            var request = await _context.Request.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            req.Total = request.Total;
            if (req.Total > 50)
            {
                request.Status = "REVIEW";
            }
            else 
            {
                request.Status = "APPROVED";
            }

            request.SubmittedDate = DateTime.Now;

            try
            {
                _context.Entry(request).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(request);
            }
            catch (Exception)
            {
                return NoContent();
            }

        }

        [HttpPut("approve/{id}")]

        public async Task<ActionResult> PutApprovalRequest(int id, Requests req) 
        {
            var requests = await _context.Request.FindAsync(id);

            req.Status = requests.Status;
            if ( requests.Status == "REVIEW")
            {
                requests.Status = "APPROVED";
                _context.Entry(requests).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(requests);
                 
            }
            else
            {
                return NotFound();
            }


        }

        [HttpPut("reject/{id}")]

        public async Task<ActionResult<Requests>> PutRejectRequest(int id, string reason)
        {
            var request = await _context.Request.FindAsync(id);
            
            if(request == null)
            {
                return NotFound();
            }

            request.Status = "REJECTED";

            request.ReasonForRejection = reason;

                
            await _context.SaveChangesAsync();
                
            return Ok(request);


        }


        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<object>> PostRequests(Requests requests)
        { 

            //(requests.UserID, requests.Description, requests.Justification, requests.DateNeeded, requests.DeliveryMode)

            _context.Request.Add(requests);
            await _context.SaveChangesAsync();

            return await _context.Request.Where(x => x.Id == requests.Id)
                                         .Select(v => new { v.UserID, v.Description, v.Justification, v.DateNeeded, v.DeliveryMode })
                                         .ToListAsync();
        }


        //CreatedAtAction("GetRequests", new { id = requests.Id

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequests(int id)
        {
            var requests = await _context.Request.FindAsync(id);
            if (requests == null)
            {
                return NotFound();
            }

            _context.Request.Remove(requests);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestsExists(int id)
        {
            return _context.Request.Any(e => e.Id == id);
        }
    }
}

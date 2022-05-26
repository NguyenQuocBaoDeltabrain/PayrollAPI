using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OverTimesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public OverTimesController(DatabaseContext context)
        {
            _context = context;
        }
        
        // GET: api/OverTimes/5
        [HttpGet("{id}")]
        public ActionResult<OverTime> GetOverTime(int id)
        {
          if (_context.OverTimes == null)
          {
              return NotFound();
          }
            var overTime = _context.OverTimes.Find(id);

            if (overTime == null)
            {
                return NotFound();
            }

            return overTime;
        }

        // PUT: api/OverTimes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult PutOverTime(int id, OverTime overTime)
        {
            if (id != overTime.Id)
            {
                return BadRequest();
            }

            _context.Entry(overTime).State = EntityState.Modified;

          
             _context.SaveChanges();
            

            return NoContent();
        }

        // POST: api/OverTimes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<OverTime> PostOverTime(OverTime overTime)
        {
          if (_context.OverTimes == null)
          {
              return Problem("Entity set 'DatabaseContext.OverTimes'  is null.");
          }
            _context.OverTimes.Add(overTime);
           _context.SaveChanges();

            return CreatedAtAction("GetOverTime", new { id = overTime.Id }, overTime);
        }

        // DELETE: api/OverTimes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOverTime(int id)
        {
            if (_context.OverTimes == null)
            {
                return NotFound();
            }
            OverTime overTime = _context.OverTimes.Find(id);
            if (overTime == null)
            {
                return NotFound();
            }

            _context.OverTimes.Remove(overTime);
           _context.SaveChanges();

            return NoContent();
        }
    }
}

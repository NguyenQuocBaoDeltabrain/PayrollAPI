
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Validations;
using SuperHeroAPI.Services;
using SuperHeroAPI.Models;


namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffsService _staffService;
        public StaffsController(IStaffsService staffService)
        {
            _staffService = staffService;
        }


        // GET: api/Staffs/5
        [HttpGet("{id}")]
        public IActionResult GetStaff(int id)
        {
            var response = _staffService.GetStaffByID(id);
            return Ok(response);
        }

        // PUT: api/Staffs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaff(int id, StaffRequest dto)
        {
              _staffService.Update(id, dto);
              return Ok( new { message = "Updated successfully" });
           
        }

        // POST: api/Staffs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostStaff(StaffRequest dto)
        {
            _staffService.Create(dto);
            return Created("Ok", new { message = "Register new user successfully" });
        }

        // DELETE: api/Staffs/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStaff(int id)
        {
            _staffService.Remove(id);
            return Ok();
        }

    }
}


using Microsoft.AspNetCore.Mvc;
using PayrollAPI.Services;
using PayrollAPI.Validations;


namespace PayrollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffService _staffService;
        public StaffsController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet("filter-staff")]
        public IActionResult Find([FromQuery] int? id, [FromQuery] string? name, [FromQuery] bool state)
        {
            StaffResponse response = _staffService.Find(id,name,state);
            return Ok(response);
        }

        [HttpPost]
        public ActionResult Create(StaffRequest dto)
        {
            _staffService.Create(dto);
            return Created("Ok", new { message = "Create Successfully" });
        }

        [HttpPost("disable-active/{id}")]
        public ActionResult DisableActive(int id)
        {
            _staffService.DisableActive(id);
            return Created("Ok", new { message = "Disable Active State Successfully" });
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, StaffRequest dto)
        {
            _staffService.Update(id, dto);
            return Ok(new { message = "Updated Successfully" });
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(int id)
        {
            _staffService.Remove(id);
            return Ok(new { message = "Remove Successfully" });
        }
    }
}
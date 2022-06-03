
using Microsoft.AspNetCore.Mvc;
using PayrollAPI.Services;
using PayrollAPI.Validations;


namespace PayrollAPI.Controllers
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

        [HttpGet("{id}")]
        public IActionResult findById(int id)
        {
            StaffResponse response = _staffService.findById(id);
            return Ok(response);
        }

        [HttpPost]
        public ActionResult Create(StaffRequest dto)
        {
            _staffService.Create(dto);
            return Created("Ok", new { message = "Create Successfully" });
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
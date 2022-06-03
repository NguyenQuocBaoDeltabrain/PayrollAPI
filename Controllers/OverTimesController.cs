using Microsoft.AspNetCore.Mvc;
using PayrollAPI.Services;
using PayrollAPI.Validations;

namespace PayrollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OverTimesController : ControllerBase
    {
        private readonly IOverTimeService _overTimeService;
        public OverTimesController(IOverTimeService overTimeService)
        {
            _overTimeService = overTimeService;
        }

        [HttpGet("{staffId}")]
        public ActionResult finsByStaffIdAndMonth(int staffId, SalaryRequest salary)
        {
            List<OvertimeResponse> response = _overTimeService.finsByStaffIdAndMonth(staffId, salary);
            return Ok(response);
        }

        [HttpPost]
        public ActionResult Create(OverTimeRequest dto)
        {
            _overTimeService.Create(dto);
            return Created("Ok", new { message = "Create Successfully" });
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(int id)
        {
            _overTimeService.Remove(id);
            return Ok(new { message = "Delete Successfully" });
        }
    }
}
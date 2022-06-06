using Microsoft.AspNetCore.Mvc;
using PayrollAPI.Services;
using PayrollAPI.Validations.DTO;
using PayrollAPI.Validations.RO;

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
        public ActionResult FinsByStaffIdAndMonth(int staffId, SalaryRequest salary)
        {
            List<OvertimeResponse> response = _overTimeService.FinsByStaffIdAndMonth(staffId, salary);
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
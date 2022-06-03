using Microsoft.AspNetCore.Mvc;
using PayrollAPI.Services;
using PayrollAPI.Validations;


namespace PayrollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly ISalaryService _salaryService;
        public SalariesController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        [HttpGet]
        public IActionResult finds()
        {
            List<SalaryResponse> salaries = _salaryService.finds();
            return Ok(salaries);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SalaryRequest salary)
        {
            ActionResult<object> response = _salaryService.Create(salary);
            return Ok(response.Value);
        }

        [HttpPost("findsByStaffIdAndMonth")]
        public IActionResult FilterSalaryWithMonthAndStaffId(FindStaffAndOTRequest dto)
        {
            List<SalaryResponse> response = _salaryService.findsByStaffIdAndMonth(dto);
            return Ok(response);
        }
    }
}
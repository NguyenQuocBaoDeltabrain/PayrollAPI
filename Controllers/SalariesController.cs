using Microsoft.AspNetCore.Mvc;
using PayrollAPI.Services;
using PayrollAPI.Validations.DTO;
using PayrollAPI.Validations.RO;
//using System.Web.Http;

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
        public IActionResult Finds([FromQuery] string month)
        {
            List<SalaryResponse> salaries = _salaryService.Finds(month);
            return Ok(salaries);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SalaryRequest salary)
        {
            ActionResult<object> response = _salaryService.Create(salary);
            return Ok(response.Value);
        }

        [HttpPost("finds-by-month")]
        public IActionResult FindsByMonth(FindStaffAndOTRequest dto)
        {
            List<SalaryResponse> response = _salaryService.FindsByMonth(dto);
            return Ok(response);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using PayrollAPI.Services;
using PayrollAPI.Validations.DTO;
using PayrollAPI.Validations.RO;

namespace PayrollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidaysController : ControllerBase
    {
        private readonly IHolidayService _holidayService;
        public HolidaysController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            HolidayResponse response = _holidayService.FindById(id);
            return Ok(response);
        }

        [HttpPost]
        public ActionResult Create(HolidayRequest dto)
        {
            _holidayService.Create(dto);
            return Created("Ok", new { message = "Create Successfully" });
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, HolidayRequest dto)
        {
            _holidayService.Update(id, dto);
            return Ok(new { message = "Updated Successfully" });
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(int id)
        {
            _holidayService.Remove(id);
            return Ok(new { message = "Remove Successfully" });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Models;
using SuperHeroAPI.Services;
using SuperHeroAPI.Validations;


namespace SuperHeroAPI.Controllers
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

        
        // GET: api/Salaries/5
        [HttpGet("{id}")]
        public IActionResult GetSalary(int id)
        {
            var response = _salaryService.GetSalaryByID(id);
            return Ok(response);
        }

        [HttpPost("test")]
        public IActionResult DaysInMoth(SalaryRequest salary)
        {
            var response =  _salaryService.GetDayInMonth(salary.Month);
            return Ok(response);
        }

        // POST: api/Salaries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //Task<ActionResult<IEnumerable<Staff>>>
        [HttpPost]
        public async Task<IActionResult> PostSalary(SalaryRequest salary)
        {
            var response = _salaryService.CreatedSalary(salary);
            //return Ok(response);
            return Ok(new { message = "Create Payroll Successfully", quanlity = response.Value });
        }

        // PUT: api/Staffs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalary(int id, SalaryRequest dto)
        {
            _salaryService.Update(id, dto);
            return Ok(new { message = "Updated successfully" });

        }

        // DELETE: api/Staffs/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSalary(int id)
        {
            _salaryService.Remove(id);
            return Ok();
        }




    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Models;
using SuperHeroAPI.Validations;
using AutoMapper;
using System;

namespace SuperHeroAPI.Services
{
    public interface ISalaryService
    {
        ActionResult<string> CreatedSalary(SalaryRequest salary);
        SalaryResponse GetSalaryByID(int id);
        void Update(int id, SalaryRequest salary);
        void Remove(int id);
        ActionResult<string> GetDayInMonth(String month);
    }

    public class SalaryService : ISalaryService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public SalaryService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public string[] holidays = new string[] { "2022-01-01", "2022-02-10", "2022-02-11", "2022-02-12", "2022-02-13", "2022-02-14", "2022-02-15", "2022-02-16", "2022-04-21", "2022-04-30", "2022-05-01", "2022-09-02", "2022-12-24", "2022-12-25" };

        public float SalaryOTEachStaff(Staff staff)
        {
            float TotalSalaryOT = 0;
            var OverTimes = _context.OverTimes.Where(ot => ot.StaffId == staff.Id);
            Console.WriteLine("Over time {0}",OverTimes);
            foreach (var OverTime in OverTimes)
            {
                float HourOT = (float)(OverTime.EndAt - OverTime.StartAt).TotalHours;
                Console.WriteLine("Hour OT {0}", HourOT);
                float SalaryOT;
                Boolean IsHoliday = false;
                Boolean IsWeekend = false;

                foreach (var day in holidays)
                {
                    var date = OverTime.StartAt;
                    String covertString = date.ToString("yyyy-MM-dd");
                    Console.WriteLine("STRING {0}", covertString);
                    if (covertString == day)
                    {
                        IsHoliday = true;
                        SalaryOT = (float)(HourOT * (staff.Salary / 192) * 3);
                        TotalSalaryOT = TotalSalaryOT + SalaryOT;
                    }    
                }

                if ((OverTime.StartAt.DayOfWeek == DayOfWeek.Saturday || OverTime.StartAt.DayOfWeek == DayOfWeek.Sunday) && IsHoliday == false)
                {
                    Console.WriteLine("Weekend");
                    IsWeekend= true;
                    SalaryOT = (float)(HourOT * (staff.Salary / 192) * 2);
                    TotalSalaryOT = TotalSalaryOT + SalaryOT;
                }

                if(IsHoliday==false&& IsWeekend==false){
                    Console.WriteLine("Workday");
                    SalaryOT = (float)(HourOT * (staff.Salary / 192) * 1.5);
                    TotalSalaryOT = TotalSalaryOT + SalaryOT;
                }

                IsWeekend=false;
                IsHoliday = false;
                OverTime.IsSalary = true;
 
            }
            _context.SaveChanges();
            return TotalSalaryOT;      
         }

        public float TaxEachStaff(float TotalSalary)
        {
            float Tax;
            switch (TotalSalary)
            {
                case < 5000:
                    Tax = (float)(TotalSalary * 0.05);
                    break;
                case < 10000:
                    Tax = (float)(TotalSalary * 0.1 - 250);
                    break;
                case < 18000:
                    Tax = (float)(TotalSalary * 0.15 - 750);
                    break;
                case < 32000:
                    Tax = (float)(TotalSalary * 0.2 - 1650);
                    break;
                case < 52000:
                    Tax = (float)(TotalSalary * 0.25 - 3250);
                    break;
                case < 80000:
                    Tax = (float)(TotalSalary * 0.3 - 5850);
                    break;
                default:
                    Tax = (float)(TotalSalary * 0.35 - 9850);
                    break;
            }
            return Tax;
        }

        public ActionResult<string> CreatedSalary(SalaryRequest salary)
        {
            using var transaction = _context.Database.BeginTransaction();
            double TotalRecordCreatedSuccess = 0;
            List<Staff> staffs =  _context.Staffs.ToList();
            float SalaryOT;
            float Tax;
            try
            {
                foreach (Staff staff in staffs)
                {
                    //SalaryOT
                    SalaryOT = SalaryOTEachStaff(staff);
                    float SalaryBasic = staff.Salary;
                    float TotalSalary = SalaryBasic + SalaryOT;
                    //Tax
                    Tax = TaxEachStaff(TotalSalary);
                    //Insurance
                    float Insurance = (float)(SalaryBasic * 0.105);
                    float SalaryReceived = TotalSalary - Tax - Insurance;
                    Salary SalaryOfStaff = new Salary { Month = salary.Month, SalaryBasic = SalaryBasic, SalaryOT = SalaryOT, Tax = Tax, Insurance = Insurance, SalaryReceived = SalaryReceived, Staff = staff };
                    _context.Salaries.Add(SalaryOfStaff);
                    _context.SaveChanges();
                    TotalRecordCreatedSuccess = TotalRecordCreatedSuccess + 1;
                }  
                transaction.Commit();
                return "Create Succces " + TotalRecordCreatedSuccess;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return "Error";
                
            }
        }

        public SalaryResponse GetSalaryByID(int id)
        {
            if (_context.Salaries == null)
            {
                throw new KeyNotFoundException("Salary table not exist record");
            }
            Salary salary = _context.Salaries.Find(id) ?? throw new KeyNotFoundException("Salary Not Found"); ;
            var response = _mapper.Map<SalaryResponse>(salary);
            return response;
        }

        public void Remove(int id)
        {
            if (_context.Salaries == null)
            {
                throw new KeyNotFoundException("Salary table not exist record");
            }
            Salary salary = _context.Salaries.Find(id);

            _context.Salaries.Remove(salary);
            _context.SaveChanges();
        }

        public void Update(int id, SalaryRequest dto)
        {
            if (_context.Salaries == null)
            {
                throw new KeyNotFoundException("Salary table not exist record");
            }
            Salary IsSalary = _context.Salaries.Find(id);
            if (IsSalary == null)
            {
                throw new KeyNotFoundException("Salary Not Found");
            }
            _mapper.Map(dto, IsSalary);
            _context.Salaries.Update(IsSalary);
            _context.SaveChanges();
        }

       
        public ActionResult<string> GetDayInMonth(string month)
        {

       
            Dictionary<string, string> dict = new Dictionary<string, string>{
             { "1","2022-01-01"},
             {"2", "2022-02-01"},
             {"3", "2022-03-01"},
             {"4", "2022-04-01"},
             { "5","2022-05-01"},
             {"6","2022-06-01"},
             {"7","2022-07-01"},
             {"8","2022-08-01"},
             {"9","2022-09-01"},
             {"10","2022-10-01"},
             {"11","2022-11-01"},
             {"12","2022-12-01"},
            };
            string result = dict.ContainsKey(month) ? dict[month] : null;
            return result;
        }
    }
}

using Microsoft.EntityFrameworkCore;
namespace PayrollAPI.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Staff> Staffs { get; set; } = null;
        public DbSet<OverTime> OverTimes { get; set; } = null;
        public DbSet<Salary> Salaries { get; set; } = null;
        public DbSet<Holiday> Holidays { get; set; } = null;
    }
}
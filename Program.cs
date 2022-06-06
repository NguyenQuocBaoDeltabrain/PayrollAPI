using Microsoft.EntityFrameworkCore;
using PayrollAPI;
using PayrollAPI.Models;
using PayrollAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var services = builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefautConnect")));

builder.Services.AddEndpointsApiExplorer();
services.AddScoped<ISalaryService, SalaryService>();
services.AddScoped<IStaffService, StaffService>();
services.AddScoped<IOverTimeService, OverTimeService>();
services.AddScoped<IHolidayService, HolidayService>();
services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
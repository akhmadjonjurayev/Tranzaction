using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.Models;
namespace Transaction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly StudentDbContext _db;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, StudentDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost("tranzac")]
        public IActionResult Trancation([FromBody] StudentViewModel studentView)
        {
            using var tranzaction = _db.Database.BeginTransaction();
            try
            {
                tranzaction.CreateSavepoint("address_student");
                var student = new Student()
                {
                    FullName = studentView.FullName,
                    IsStudent = studentView.IsStudent,
                    Birthday = studentView.Birthday
                };
                _db.Students.Add(student);
                _db.SaveChanges();
                var address = new Address()
                {
                    PochtaIndex = studentView.PochtaIndex,
                    StudentId = student.StudentId
                };
                _db.Addresses.Add(address);
                if (_db.SaveChanges() > 0)
                {
                    tranzaction.Commit();
                    return Ok("ok !");
                }
                else
                {
                    tranzaction.RollbackToSavepoint("address_student");
                    return Ok("error !");
                }
            }
            catch (Exception ex)
            {
                tranzaction.RollbackToSavepoint("address_student");
                return Ok("error with exception !");
            }
        }
        [HttpPost("execute")]
        public IActionResult ExecuteTransaction([FromBody] StudentViewModel studentView)
        {
            var execute = _db.Database.CreateExecutionStrategy();
            return execute.Execute(() =>
            {
                using var tranzaction = _db.Database.BeginTransaction();
                try
                {
                    tranzaction.CreateSavepoint("address_student");
                    var student = new Student()
                    {
                        FullName = studentView.FullName,
                        IsStudent = studentView.IsStudent,
                        Birthday = studentView.Birthday
                    };
                    _db.Students.Add(student);
                    _db.SaveChanges();
                    var address = new Address()
                    {
                        PochtaIndex = studentView.PochtaIndex,
                        StudentId = student.StudentId
                    };
                    _db.Addresses.Add(address);
                    if (_db.SaveChanges() > 0)
                    {
                        tranzaction.Commit();
                        return Ok("ok !");
                    }
                    else
                    {
                        tranzaction.RollbackToSavepoint("address_student");
                        return Ok("error !");
                    }
                }
                catch (Exception ex)
                {
                    tranzaction.RollbackToSavepoint("address_student");
                    return Ok("error with exception !");
                }
            });
        }
    }
}

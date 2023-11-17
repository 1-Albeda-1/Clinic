using Clinic.API.Models.Response;
using Clinic.Services.Contracts.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с расписанием
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "TimeTable")]
    public class TimeTableController : ControllerBase
    {
        private readonly ITimeTableService timeTableService;
        public TimeTableController(ITimeTableService timeTableService)
        {
            this.timeTableService = timeTableService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await timeTableService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new TimeTableResponse
            {
                Id = x.Id,
                Time = x.Time,
                Office = x.Office,
                Doctor = x.Doctor,
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await timeTableService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти поликлиники с идентификатором {id}");
            }

            return Ok(new TimeTableResponse
            {
                Id = result.Id,
                Time = result.Time,
                Office = result.Office,
                Doctor = result.Doctor,
            });
        }
    }
}
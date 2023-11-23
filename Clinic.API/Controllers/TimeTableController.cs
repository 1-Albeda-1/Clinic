using AutoMapper;
using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Models.Response;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;
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
        private readonly IDoctorService doctorService;
        private readonly IMapper mapper;
        public TimeTableController(ITimeTableService timeTableService, IDoctorService doctorService, IMapper mapper)
        {
            this.timeTableService = timeTableService;
            this.doctorService = doctorService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<TimeTableResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await timeTableService.GetAllAsync(cancellationToken);
            var result2 = result.Select(x => mapper.Map<TimeTableResponse>(x));
            return Ok(result2);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TimeTableResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await timeTableService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Рассписания с таким Id нет!");
            }

            return Ok(mapper.Map<TimeTableResponse>(item));
        }

        [HttpPost]
        [ProducesResponseType(typeof(TimeTableResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(CreateTimeTableRequest model, CancellationToken cancellationToken)
        {
            //var result = await timeTableService.AddAsync(model.Time, model.Office, model.Doctor, cancellationToken);
            //return Ok(mapper.Map<TimeTableResponse>(result));
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(typeof(TimeTableResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(TimeTableRequest request, CancellationToken cancellationToken)
        {
            //var model = mapper.Map<TimeTableModel>(request);

            //model.Doctor = await doctorService.GetByIdAsync(request.Doctor, cancellationToken);

            //var result = await timeTableService.EditAsync(model, cancellationToken);
            //return Ok(mapper.Map<TimeTableResponse>(result));
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            //await timeTableService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
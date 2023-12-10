using AutoMapper;
using Azure.Core;
using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Models;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;
using Clinic.Services.Contracts.ModelsRequest;
using Microsoft.AspNetCore.Mvc;
using Clinic.API.Models.Exceptions;

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

        /// <summary>
        /// Получить рассписание по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TimeTableResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await timeTableService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<TimeTableResponse>(item));
        }

        /// <summary>
        /// Добавить рассписание
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TimeTableResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateTimeTableRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<TimeTableRequestModel>(request);
            var result = await timeTableService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<TimeTableResponse>(result));
        }

        /// <summary>
        /// Изменить рассписание по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(TimeTableResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(TimeTableRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<TimeTableRequestModel>(request);
            var result = await timeTableService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<TimeTableResponse>(result));
        }

        /// <summary>
        /// Удалить рассписание по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await timeTableService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
using AutoMapper;
using Azure.Core;
using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Models;
using Clinic.Services.Contracts;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.ModelsRequest;
using Clinic.Services.Contracts.Models;
using Clinic.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using Clinic.API.Models.Exceptions;
using Clinic.API.Infrastructures.Validator;

namespace Clinic.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с записями на прием
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "BookingAppointment")]
    public class BookingAppointmentController : ControllerBase
    {
        private readonly IBookingAppointmentService bookingAppointmentService;
        private readonly IMapper mapper;
        private readonly IApiValidatorService validatorService;

        public BookingAppointmentController(IBookingAppointmentService bookingAppointmentService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.bookingAppointmentService = bookingAppointmentService;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookingAppointmentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await bookingAppointmentService.GetAllAsync(cancellationToken);
            var result2 = result.Select(x => mapper.Map<BookingAppointmentResponse>(x));
            return Ok(result2);
        }

        /// <summary>
        /// Получить запись по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BookingAppointmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await bookingAppointmentService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<BookingAppointmentResponse>(item));
        }

        /// <summary>
        /// Добавить запись
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(BookingAppointmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateBookingAppointmentRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<BookingAppointmentRequestModel>(request);
            var result = await bookingAppointmentService.AddAsync(model, cancellationToken);
            return Ok(mapper.Map<BookingAppointmentResponse>(result));
        }

        /// <summary>
        /// Изменить запись по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(BookingAppointmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(BookingAppointmentRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<BookingAppointmentRequestModel>(request);
            var result = await bookingAppointmentService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<BookingAppointmentResponse>(result));
        }

        /// <summary>
        /// Удалить запись по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            await bookingAppointmentService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}

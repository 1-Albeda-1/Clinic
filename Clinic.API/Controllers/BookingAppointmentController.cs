using AutoMapper;
using Clinic.API.Models.Response;
using Clinic.Services.Contracts.Interface;
using Microsoft.AspNetCore.Mvc;

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
        public BookingAppointmentController(IBookingAppointmentService bookingAppointmentService, IMapper mapper)
        {
            this.bookingAppointmentService = bookingAppointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await bookingAppointmentService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new BookingAppointmentResponse
            {
                Id = x.Id,
                Patient = x.Patient,
                TimeTable = x.TimeTable,
                Сomplaint = x.Сomplaint,
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await bookingAppointmentService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти записи на прием с идентификатором {id}");
            }

            return Ok(new BookingAppointmentResponse
            {
                Id = result.Id,
                Patient = result.Patient,
                TimeTable = result.TimeTable,
                Сomplaint = result.Сomplaint,
            });
        }
    }
}

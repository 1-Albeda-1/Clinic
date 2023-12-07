﻿using AutoMapper;
using Azure.Core;
using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Models.Response;
using Clinic.Services.Contracts;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.ModelsRequest;
using Clinic.Services.Contracts.Models;
using Clinic.Services.Implementations;
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
        private readonly IMapper mapper;

        public BookingAppointmentController(IBookingAppointmentService bookingAppointmentService, IMapper mapper)
        {
            this.bookingAppointmentService = bookingAppointmentService;
            this.mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookingAppointmentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await bookingAppointmentService.GetAllAsync(cancellationToken);
            var result2 = result.Select(x => mapper.Map<BookingAppointmentResponse>(x));
            return Ok(result2);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BookingAppointmentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await bookingAppointmentService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Записи на прием с таким Id нет!");
            }
            return Ok(mapper.Map<BookingAppointmentResponse>(item));
        }

        [HttpPost]
        [ProducesResponseType(typeof(BookingAppointmentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(CreateBookingAppointmentRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<BookingAppointmentRequestModel>(request);
            var result = await bookingAppointmentService.AddAsync(model, cancellationToken);
            return Ok(mapper.Map<BookingAppointmentResponse>(result));
        }

        [HttpPut]
        [ProducesResponseType(typeof(BookingAppointmentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(BookingAppointmentRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<BookingAppointmentRequestModel>(request);
            var result = await bookingAppointmentService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<BookingAppointmentResponse>(result));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await bookingAppointmentService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}

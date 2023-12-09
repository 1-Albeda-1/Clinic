using AutoMapper;
using Clinic.API.Enums;
using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Models.Response;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с врачами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService doctorService;
        private readonly IMapper mapper;
        public DoctorController(IDoctorService doctorService, IMapper mapper)
        {
            this.doctorService = doctorService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DoctorResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await doctorService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<DoctorResponse>(x)));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(DoctorResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await doctorService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Врача с таким Id нет!");
            }
            return Ok(mapper.Map<DoctorResponse>(item));
        }

        [HttpPost]
        [ProducesResponseType(typeof(DoctorResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(CreateDoctorRequest model, CancellationToken cancellationToken)
        {
            var doctorModel = mapper.Map<DoctorModel>(model);
            var result = await doctorService.AddAsync(doctorModel, cancellationToken);
            return Ok(mapper.Map<DoctorResponse>(result));
        }

        [HttpPut]
        [ProducesResponseType(typeof(DoctorResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(DoctorRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<DoctorModel>(request);
            var result = await doctorService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<DoctorResponse>(result));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await doctorService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}


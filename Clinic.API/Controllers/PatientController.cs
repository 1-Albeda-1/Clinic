using AutoMapper;
using Azure.Core;
using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Models;
using Clinic.Services.Contracts;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;
using Clinic.Services.Contracts.ModelsRequest;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с пациентами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Patient")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;
        private readonly IMedClinicService medClinicService;
        private readonly IDiagnosisService diagnosisService;
        private readonly IMapper mapper;
        public PatientController(IPatientService patientService, IMapper mapper, IMedClinicService medClinicService, IDiagnosisService diagnosisService)
        {
            this.patientService = patientService;
            this.mapper = mapper;
            this.diagnosisService = diagnosisService;
            this.medClinicService = medClinicService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<PatientResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await patientService.GetAllAsync(cancellationToken);
            var result2 = result.Select(x => mapper.Map<PatientResponse>(x));
            return Ok(result2);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await patientService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Пациента с таким Id нет!");
            }

            return Ok(mapper.Map<PatientResponse>(item));
        }

        [HttpPost]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(CreatePatientRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<PatientRequestModel>(request);
            var result = await patientService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<PatientResponse>(result));
        }

        [HttpPut]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(PatientRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<PatientRequestModel>(request);
            var result = await patientService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<PatientResponse>(result));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await patientService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}


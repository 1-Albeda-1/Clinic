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
using Clinic.API.Models.Exceptions;
using Clinic.API.Infrastructures.Validator;

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
        private readonly IApiValidatorService validatorService;
        public PatientController(IPatientService patientService, IMapper mapper, IMedClinicService medClinicService, IDiagnosisService diagnosisService, IApiValidatorService validatorService)
        {
            this.patientService = patientService;
            this.mapper = mapper;
            this.diagnosisService = diagnosisService;
            this.medClinicService = medClinicService;
            this.validatorService = validatorService;
        }

        /// <summary>
        /// Получить список пациентов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<PatientResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await patientService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<PatientResponse>>(result));
        }

        /// <summary>
        /// Получить пациента по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await patientService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<PatientResponse>(item));
        }

        /// <summary>
        /// Добавить пациента
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreatePatientRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<PatientRequestModel>(request);
            var result = await patientService.AddAsync(model, cancellationToken);
            return Ok(mapper.Map<PatientResponse>(result));
        }


        /// <summary>
        /// Изменить пациениа по Id
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(PatientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(PatientRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<PatientRequestModel>(request);
            var result = await patientService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<PatientResponse>(result));
        }

        /// <summary>
        /// Удалить пациента по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await patientService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}


using AutoMapper;
using Clinic.API.Enums;
using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Models;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using Clinic.API.Models.Exceptions;
using Clinic.API.Infrastructures.Validator;
using Azure.Core;

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
        private readonly IApiValidatorService validatorService;
        public DoctorController(IDoctorService doctorService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.doctorService = doctorService;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        /// <summary>
        /// Получить список врачей
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DoctorResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await doctorService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<DoctorResponse>(x)));
        }

        /// <summary>
        /// Получить врача по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(DoctorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await doctorService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<DoctorResponse>(item));
        }

        /// <summary>
        /// Добавить врача
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(DoctorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateDiagnosisRequest model, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(model, cancellationToken);

            var doctorModel = mapper.Map<DoctorModel>(model);
            var result = await doctorService.AddAsync(doctorModel, cancellationToken);
            return Ok(mapper.Map<DoctorResponse>(result));
        }

        /// <summary>
        /// Изменить врача по Id
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(DoctorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(DoctorRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<DoctorModel>(request);
            var result = await doctorService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<DoctorResponse>(result));
        }

        /// <summary>
        /// Удалить врача по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await doctorService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}


using AutoMapper;
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
    /// CRUD контроллер по работе с диагнозами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Diagnosis")]
    public class DiagnosisController : ControllerBase
    {
        private readonly IDiagnosisService diagnosisService;
        private readonly IMapper mapper;
        private readonly IApiValidatorService validatorService;
        public DiagnosisController(IDiagnosisService diagnosisService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.diagnosisService = diagnosisService;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        /// <summary>
        /// Получить список диагнозов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DiagnosisResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await diagnosisService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<DiagnosisResponse>(x)));
        }

        /// <summary>
        /// Получить диагноз по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(DiagnosisResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await diagnosisService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<DiagnosisResponse>(item));
        }

        /// <summary>
        /// Добавить диагноз
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(DiagnosisResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateDiagnosisRequest model, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(model, cancellationToken);

            var diagnosisModel = mapper.Map<DiagnosisModel>(model);

            var result = await diagnosisService.AddAsync(diagnosisModel, cancellationToken);
            return Ok(mapper.Map<DiagnosisResponse>(result));
        }

        /// <summary>
        /// Изменить диагноз
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(DiagnosisResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(DiagnosisRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<DiagnosisModel>(request);
            var result = await diagnosisService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<DiagnosisResponse>(result));
        }

        /// <summary>
        /// Удалить диагноз по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await diagnosisService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}

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
    /// CRUD контроллер по работе с поликлиниками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "MedClinic")]
    public class MedClinicController : ControllerBase
    {
        private readonly IMedClinicService medClinicService;
        private readonly IMapper mapper;
        private readonly IApiValidatorService validatorService;
        public MedClinicController(IMedClinicService medClinicService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.medClinicService = medClinicService;
            this.mapper = mapper;
            this.validatorService = validatorService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MedClinicResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await medClinicService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<MedClinicResponse>(x)));
        }

        /// <summary>
        /// Получить поликлинику по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(MedClinicResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await medClinicService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<MedClinicResponse>(item));
        }

        /// <summary>
        /// Добавить поликлинику
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(MedClinicResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateMedClinicRequest model, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(model, cancellationToken);

            var medClinicModel = mapper.Map<MedClinicModel>(model);
            var result = await medClinicService.AddAsync(medClinicModel, cancellationToken);
            return Ok(mapper.Map<MedClinicResponse>(result));
        }

        /// <summary>
        /// Изменить поликлинику по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(MedClinicResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(MedClinicRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<MedClinicModel>(request);
            var result = await medClinicService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<MedClinicResponse>(result));
        }

        /// <summary>
        /// Удалить поликлинику по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await medClinicService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}


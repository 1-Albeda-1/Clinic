using AutoMapper;
using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.API.Models.Response;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

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
        public DiagnosisController(IDiagnosisService diagnosisService, IMapper mapper)
        {
            this.diagnosisService = diagnosisService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DiagnosisResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await diagnosisService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<DiagnosisResponse>(x)));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(DiagnosisResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await diagnosisService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Диагноза с таким Id нет!");
            }
            return Ok(mapper.Map<DiagnosisResponse>(item));
        }

        [HttpPost]
        [ProducesResponseType(typeof(DiagnosisResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(CreateDiagnosisRequest model, CancellationToken cancellationToken)
        {
            //var result = await diagnosisService.AddAsync(model.Name, model.Medicament, cancellationToken);
            //return Ok(mapper.Map<DiagnosisResponse>(result));
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(typeof(DiagnosisResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(DiagnosisRequest request, CancellationToken cancellationToken)
        {
            //var model = mapper.Map<DiagnosisModel>(request);
            //var result = await diagnosisService.EditAsync(model, cancellationToken);
            //return Ok(mapper.Map<DiagnosisResponse>(result));
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            //await diagnosisService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}

using Clinic.API.Models.Response;
using Clinic.Services.Contracts.Interface;
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
        public DiagnosisController(IDiagnosisService diagnosisService)
        {
            this.diagnosisService = diagnosisService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await diagnosisService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new DiagnosisResponse
            {
                Id = x.Id,
                Name = x.Name,
                Medicament = x.Medicament,
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await diagnosisService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти клиентов с идентификатором {id}");
            }

            return Ok(new DiagnosisResponse
            {
                Id = result.Id,
                Name = result.Name,
                Medicament = result.Medicament,
            });
        }
    }
}

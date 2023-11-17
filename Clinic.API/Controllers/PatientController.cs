using Clinic.API.Models.Response;
using Clinic.Services.Contracts;
using Clinic.Services.Contracts.Interface;
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
        public PatientController(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await patientService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new PatientResponse
            {
                Id = x.Id,
                Surname = x.Surname,
                Name = x.Name,
                Patronymic = x.Patronymic,
                Phone = x.Phone,
                Policy = x.Policy,
                Birthday = x.Birthday,
                MedClinic = x.MedClinic,
                Diagnosis = x.Diagnosis,
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await patientService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти записи на прием с идентификатором {id}");
            }

            return Ok(new PatientResponse
            {
                Id = result.Id,
                Surname = result.Surname,
                Name = result.Name,
                Patronymic = result.Patronymic,
                Phone = result.Phone,
                Policy = result.Policy,
                Birthday = result.Birthday,
                MedClinic = result.MedClinic,
                Diagnosis = result.Diagnosis,
            });
        }
    }
}


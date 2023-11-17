using Clinic.API.Models.Response;
using Clinic.Services.Contracts.Interface;
using Microsoft.AspNetCore.Mvc;

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
        public MedClinicController(IMedClinicService medClinicService)
        {
            this.medClinicService = medClinicService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await medClinicService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new MedClinicResponse
            {
                Id = x.Id,
                Address = x.Address,
                Name = x.Name,                
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await medClinicService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти поликлиники с идентификатором {id}");
            }

            return Ok(new MedClinicResponse
            {
                Id = result.Id,
                Address = result.Address,
                Name = result.Name,          
            });
        }
    }
}


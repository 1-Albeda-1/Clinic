using Clinic.API.Enums;
using Clinic.API.Models.Response;
using Clinic.Services.Contracts.Interface;
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
        public DoctorController(IDoctorService doctorService)
        {
            this.doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await doctorService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new DoctorResponse
            {
                Id = x.Id,
                Surname = x.Surname,
                Name = x.Name,
                Patronymic = x.Patronymic,
                CategoriesType = (CategoriesTypesResponse)x.CategoriesType,
                DepartmentType = (DepartmentTypesResponse)x.DepartmentType,
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await doctorService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти клиентов с идентификатором {id}");
            }

            return Ok(new DoctorResponse
            {
                Id = result.Id,
                Surname = result.Surname,
                Name = result.Name,
                Patronymic = result.Patronymic,
                CategoriesType = (CategoriesTypesResponse)result.CategoriesType,
                DepartmentType = (DepartmentTypesResponse)result.DepartmentType,
            });
        }
    }
}


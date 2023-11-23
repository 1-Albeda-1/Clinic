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
    /// CRUD контроллер по работе с поликлиниками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "MedClinic")]
    public class MedClinicController : ControllerBase
    {
        private readonly IMedClinicService medClinicService;
        private readonly IMapper mapper;
        public MedClinicController(IMedClinicService medClinicService, IMapper mapper)
        {
            this.medClinicService = medClinicService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MedClinicResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await medClinicService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<MedClinicResponse>(x)));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(MedClinicResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await medClinicService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Поликлиники с таким Id нет!");
            }
            return Ok(mapper.Map<MedClinicResponse>(item));
        }

        [HttpPost]
        [ProducesResponseType(typeof(MedClinicResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(CreateMedClinicRequest model, CancellationToken cancellationToken)
        {
            //var result = await medClinicService.AddAsync(model.Name, model.Address, cancellationToken);
            //return Ok(mapper.Map<MedClinicResponse>(result));
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(typeof(MedClinicResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(MedClinicRequest request, CancellationToken cancellationToken)
        {
            //var model = mapper.Map<MedClinicModel>(request);
            //var result = await medClinicService.EditAsync(model, cancellationToken);
            //return Ok(mapper.Map<MedClinicResponse>(result));
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            //await medClinicService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}


using AutoMapper;
using Clinic.Repositories.Contracts.WriteRepositoriesContracts;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Services.Anchors;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;
using Clinic.Common.Interface;
using Clinic.Context.Contracts.Models;
using Clinc.Services.Contracts.Exceptions;
using Clinic.Services.Contracts.Exceptions;

namespace Clinic.Services.Implementations
{
    public class MedClinicService : IMedClinicService, IServiceAnchor
    {
        private readonly IMedClinicReadRepository medClinicReadRepository;
        private readonly IMedClinicWriteRepository medClinicWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public MedClinicService(IMedClinicReadRepository medClinicReadRepository, IMapper mapper, 
            IMedClinicWriteRepository medClinicWriteRepository, IUnitOfWork unitOfWork)
        {
            this.medClinicReadRepository = medClinicReadRepository;
            this.mapper = mapper;
            this.medClinicWriteRepository = medClinicWriteRepository;
            this.unitOfWork = unitOfWork;
        }
        async Task<MedClinicModel> IMedClinicService.AddAsync(string address, string name, CancellationToken cancellationToken)
        {
            var item = new MedClinic
            {
                Id = Guid.NewGuid(),
                Address = address,
                Name = name,
            };

            medClinicWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<MedClinicModel>(item);
        }

        async Task IMedClinicService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetMedClinic = await medClinicReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetMedClinic == null)
            {
                throw new TimeTableEntityNotFoundException<MedClinic>(id);
            }


            if (targetMedClinic.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Поликлиника с идентификатором {id} уже удалена");
            }

            medClinicWriteRepository.Delete(targetMedClinic);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<MedClinicModel> IMedClinicService.EditAsync(MedClinicModel source, CancellationToken cancellationToken)
        {
            var targetMedClinic = await medClinicReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetMedClinic == null)
            {
                throw new TimeTableEntityNotFoundException<MedClinic>(source.Id);
            }

            targetMedClinic.Name = source.Name;
            targetMedClinic.Address = source.Address;

            medClinicWriteRepository.Update(targetMedClinic);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<MedClinicModel>(targetMedClinic);
        }
        async Task<IEnumerable<MedClinicModel>> IMedClinicService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await medClinicReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<MedClinicModel>(x));
        }

        async Task<MedClinicModel?> IMedClinicService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await medClinicReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return null;
            }
            return mapper.Map<MedClinicModel>(item);
        }
    }

}

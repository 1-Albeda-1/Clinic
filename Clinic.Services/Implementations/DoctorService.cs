using AutoMapper;
using Clinic.Repositories.Contracts.WriteRepositoriesContracts;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Services.Anchors;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;
using Clinic.Common.Interface;
using Clinic.Context.Contracts.Enums;
using Clinic.Context.Contracts.Models;
using Clinc.Services.Contracts.Exceptions;
using Clinic.Services.Contracts.Exceptions;

namespace Clinic.Services.Implementations
{
    public class DoctorService : IDoctorService,  IServiceAnchor
    {
        private readonly IDoctorReadRepository doctorReadRepository;
        private readonly IDoctorWriteRepository doctorWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public DoctorService(IDoctorReadRepository doctorReadRepository, IMapper mapper, 
            IDoctorWriteRepository doctorWriteRepository, IUnitOfWork unitOfWork)
        {
            this.doctorReadRepository = doctorReadRepository;
            this.mapper = mapper;
            this.doctorWriteRepository = doctorWriteRepository;
            this.unitOfWork = unitOfWork;
        }
        async Task<DoctorModel> IDoctorService.AddAsync(DoctorModel model, CancellationToken cancellationToken)
        {
            var item = mapper.Map<Doctor>(model);
            doctorWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<DoctorModel>(item);
        }

        async Task IDoctorService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetDoctor = await doctorReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetDoctor == null)
            {
                throw new TimeTableEntityNotFoundException<Doctor>(id);
            }


            if (targetDoctor.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Врач с идентификатором {id} уже удален");
            }

            doctorWriteRepository.Delete(targetDoctor);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<DoctorModel> IDoctorService.EditAsync(DoctorModel source, CancellationToken cancellationToken)
        {
            var targetDoctor = await doctorReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetDoctor == null)
            {
                throw new TimeTableEntityNotFoundException<Doctor>(source.Id);
            }

            targetDoctor.Surname = source.Surname;
            targetDoctor.Name = source.Name;
            targetDoctor.Patronymic = source.Patronymic;
            targetDoctor.CategoriesType = (CategoriesTypes)source.CategoriesType;
            targetDoctor.DepartmentType = (DepartmentTypes)source.DepartmentType;
            doctorWriteRepository.Update(targetDoctor);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<DoctorModel>(targetDoctor);
        }
        async Task<IEnumerable<DoctorModel>> IDoctorService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await doctorReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<DoctorModel>(x));
        }

        async Task<DoctorModel?> IDoctorService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await doctorReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return null;
            }
            return mapper.Map<DoctorModel>(item);
        }
    }
}

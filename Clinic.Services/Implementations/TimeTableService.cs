﻿using AutoMapper;
using Clinic.Repositories.Contracts.WriteRepositoriesContracts;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Services.Anchors;
using Clinic.Services.Contracts.Interface;
using Clinic.Services.Contracts.Models;
using Clinic.Common.Interface;
using Clinic.Context.Contracts.Models;
using Clinc.Services.Contracts.Exceptions;
using Clinic.Services.Contracts.Exceptions;
using System.IO;
using System.Net.Sockets;
using Clinic.Services.Contracts.ModelsRequest;

namespace Clinic.Services.Implementations
{
    public class TimeTableService : ITimeTableService, IServiceAnchor
    {
        private readonly ITimeTableReadRepository timeTableReadRepository;
        private readonly ITimeTableWriteRepository timeTableWriteRepository;
        private readonly IDoctorReadRepository doctorReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public TimeTableService(ITimeTableReadRepository timeTableReadRepository, IMapper mapper, 
            IDoctorReadRepository doctorReadRepository, ITimeTableWriteRepository timeTableWriteRepository, IUnitOfWork unitOfWork)
        {
            this.timeTableReadRepository = timeTableReadRepository;
            this.mapper = mapper;
            this.doctorReadRepository = doctorReadRepository;
            this.timeTableWriteRepository = timeTableWriteRepository;
            this.unitOfWork = unitOfWork;
        }
        async Task<TimeTableModel> ITimeTableService.AddAsync(TimeTableRequestModel model, CancellationToken cancellationToken)
        {
            var timeTable = mapper.Map<TimeTable>(model);
            timeTable.Time = model.Time;
            timeTable.Office = model.Office;
            timeTable.Doctor = await doctorReadRepository.GetByIdAsync(timeTable.DoctorId, cancellationToken);

            timeTableWriteRepository.Add(timeTable);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var timeTableModel = mapper.Map<TimeTableModel>(timeTable);
            timeTableModel.Doctor = mapper.Map<DoctorModel>(timeTable.Doctor);

            return timeTableModel;
        }

        async Task ITimeTableService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetTimeTable = await timeTableReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetTimeTable == null)
            {
                throw new TimeTableEntityNotFoundException<TimeTable>(id);
            }

            if (targetTimeTable.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Рассписание с идентификатором {id} уже удалено");
            }

            timeTableWriteRepository.Delete(targetTimeTable);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<TimeTableModel> ITimeTableService.EditAsync(TimeTableRequestModel model, CancellationToken cancellationToken)
        {
            var timeTable = await timeTableReadRepository.GetByIdAsync(model.Id, cancellationToken);

            if (timeTable == null)
            {
                throw new TimeTableEntityNotFoundException<TimeTable>(model.Id);
            }


            timeTable.Time = model.Time;
            timeTable.Office = model.Office;
            timeTable.Doctor = await doctorReadRepository.GetByIdAsync(timeTable.DoctorId, cancellationToken);

            timeTableWriteRepository.Update(timeTable);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var timeTableModel = mapper.Map<TimeTableModel>(timeTable);
            timeTableModel.Doctor = mapper.Map<DoctorModel>(timeTable.Doctor);


            return timeTableModel;
        }
        async Task<IEnumerable<TimeTableModel>> ITimeTableService.GetAllAsync(CancellationToken cancellationToken)
        {
            var timeTables = await timeTableReadRepository.GetAllAsync(cancellationToken);
            var doctors = await doctorReadRepository
                .GetByIdsAsync(timeTables.Select(x => x.DoctorId).Distinct(), cancellationToken);

            var result = new List<TimeTableModel>();

            foreach (var timeTable in timeTables)
            {
                if (!doctors.TryGetValue(timeTable.DoctorId, out var doctor))
                {
                    continue;
                }
                else
                {
                    var timeTableModel = mapper.Map<TimeTableModel>(timeTable);

                    timeTableModel.Doctor = mapper.Map<DoctorModel>(doctor);

                    result.Add(timeTableModel);
                }
            }
            return result;
        }

        async Task<TimeTableModel?> ITimeTableService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await timeTableReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return null;
            }

            var doctor = await doctorReadRepository.GetByIdAsync(item.DoctorId, cancellationToken);
            var timeTableModel = mapper.Map<TimeTableModel>(item);

            timeTableModel.Doctor = mapper.Map<DoctorModel>(doctor);

            return timeTableModel;
        }
    }
}

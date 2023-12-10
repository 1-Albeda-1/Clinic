﻿using FluentValidation;
using Clinic.API.Models.Request;
using Clinic.API.Models.CreateRequest;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;

namespace Clinic.API.Validators.BookingAppointment
{
    public class CreateBookingAppointmentRequestValidator : AbstractValidator<CreateBookingAppointmentRequest>
    {
        public CreateBookingAppointmentRequestValidator(
            ITimeTableReadRepository timeTableReadRepository,
            IPatientReadRepository patientReadRepository)
        {

            RuleFor(x => x.Сomplaint)
                .NotNull()
                .NotEmpty()
                .WithMessage("Жалоба не должна быть пустой или null");

            RuleFor(x => x.TimeTable)
                .NotNull()
                .NotEmpty()
                .WithMessage("Рассписание не должно быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var timeTable = await timeTableReadRepository.GetByIdAsync(id, CancellationToken);
                    return timeTable != null;
                })
                .WithMessage("Такого рассписания не существует!");

            RuleFor(x => x.Patient)
               .NotNull()
               .NotEmpty()
               .WithMessage("Пациент не должен быть пустым или null")
               .MustAsync(async (id, CancellationToken) =>
               {
                   var patient = await patientReadRepository.GetByIdAsync(id, CancellationToken);
                   return patient != null;
               })
               .WithMessage("Такого пациента не существует!");
        }
    }
}
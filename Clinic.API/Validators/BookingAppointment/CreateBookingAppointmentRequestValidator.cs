using FluentValidation;
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

            RuleFor(x => x.TimeTableId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Рассписание не должно быть пустым или null")
                .MustAsync(async (x, cancellationToken) => await timeTableReadRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage("Такого рассписания не существует!");

            RuleFor(x => x.PatientId)
               .NotNull()
               .NotEmpty()
               .WithMessage("Пациент не должен быть пустым или null")
               .MustAsync(async (x, cancellationToken) => await patientReadRepository.IsNotNullAsync(x, cancellationToken))
               .WithMessage("Такого пациента не существует!");
        }
    }
}

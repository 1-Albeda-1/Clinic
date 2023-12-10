using FluentValidation;
using Clinic.API.Models.Request;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;

namespace Clinic.API.Validators.TimeTable
{
    public class TimeTableRequestValidator : AbstractValidator<TimeTableRequest>
    {
        public TimeTableRequestValidator(
           IDoctorReadRepository doctorReadRepository)
        {

            RuleFor(x => x.Id)
              .NotNull()
              .NotEmpty()
              .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.Time)
                .NotNull()
                .NotEmpty()
                .WithMessage("Время не должно быть пустой или null");

            RuleFor(x => x.Office)
                .NotNull()
                .NotEmpty()
                .WithMessage("Кабинет не должен быть пустой или null");

            RuleFor(x => x.Doctor)
                .NotNull()
                .NotEmpty()
                .WithMessage("Врач не должен быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var doctor = await doctorReadRepository.GetByIdAsync(id, CancellationToken);
                    return doctor != null;
                })
                .WithMessage("Такого врача не существует!");
        }
    }
}

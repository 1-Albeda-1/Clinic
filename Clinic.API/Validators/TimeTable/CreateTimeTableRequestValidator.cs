using Clinic.API.Models.CreateRequest;
using Clinic.API.Models.Request;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using FluentValidation;

namespace Clinic.API.Validators.TimeTable
{
    /// <summary>
    /// Валидатор <see cref="TimeTableRequest"/>
    /// </summary>
    public class CreateTimeTableRequestValidator : AbstractValidator<CreateTimeTableRequest>
    {
        public CreateTimeTableRequestValidator(
           IDoctorReadRepository doctorReadRepository)
        {
            RuleFor(x => x.Time)
                .NotNull()
                .NotEmpty()
                .WithMessage("Время не должно быть пустой или null");

            RuleFor(x => x.Office)
                .NotNull()
                .NotEmpty()
                .WithMessage("Кабинет не должен быть пустой или null");

            RuleFor(x => x.DoctorId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Врач не должен быть пустым или null")
                .MustAsync(async (x, cancellationToken) => await doctorReadRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage("Такого врача не существует!");
        }
    }
}

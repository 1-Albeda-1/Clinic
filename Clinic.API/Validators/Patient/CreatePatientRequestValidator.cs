using FluentValidation;
using Clinic.API.Models.Request;
using Clinic.API.Models.CreateRequest;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;
using Clinic.Repositories.ReadRepositories;

namespace Clinic.API.Validators.Patient
{
    /// <summary>
    /// Валидатор <see cref="PatientRequest"/>
    /// </summary>
    public class CreatePatientRequestValidator : AbstractValidator<CreatePatientRequest>
    {
        public CreatePatientRequestValidator(
        IMedClinicReadRepository medClinicReadRepository,
        IDiagnosisReadRepository diagnosisReadRepository)
        {

            RuleFor(x => x.Surname)
                .NotNull()
                .NotEmpty()
                .WithMessage("Фамилия не должна быть пустой или null");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя не должно быть пустой или null");

            RuleFor(x => x.Patronymic)
                .NotNull()
                .NotEmpty()
                .WithMessage("Отчество не должно быть пустой или null");

            RuleFor(x => x.Phone)
               .NotNull()
               .NotEmpty()
               .WithMessage("Номер телефона не должен быть пустой или null");

            RuleFor(x => x.Policy)
               .NotNull()
               .NotEmpty()
               .WithMessage("Полис не должен быть пустой или null");

            RuleFor(x => x.Birthday)
               .NotNull()
               .NotEmpty()
               .WithMessage("Дата рождения не должна быть пустой или null");

            RuleFor(x => x.MedClinicId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Поликлиника не должна быть пустым или null")
                .MustAsync(async (x, cancellationToken) => await medClinicReadRepository.IsNotNullAsync(x!.Value, cancellationToken))
                .WithMessage("Такой поликлиники не существует!");

            RuleFor(x => x.DiagnosisId)
               .NotNull()
               .NotEmpty()
               .WithMessage("Диагноз не должен быть пустым или null")
               .MustAsync(async (x, cancellationToken) => await diagnosisReadRepository.IsNotNullAsync(x, cancellationToken))
               .WithMessage("Такого диагноза не существует!");
        }
    }
}

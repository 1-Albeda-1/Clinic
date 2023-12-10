using FluentValidation;
using Clinic.API.Models.Request;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;

namespace Clinic.API.Validators.Patient
{
    public class PatientRequestValidator : AbstractValidator<PatientRequest>
    {
        public PatientRequestValidator(
           IMedClinicReadRepository medClinicReadRepository,
           IDiagnosisReadRepository diagnosisReadRepository)
        {

            RuleFor(x => x.Id)
              .NotNull()
              .NotEmpty()
              .WithMessage("Id не должен быть пустым или null");

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

            RuleFor(x => x.MedClinic)
                .NotNull()
                .NotEmpty()
                .WithMessage("Поликлиника не должна быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var medClinic = await medClinicReadRepository.GetByIdAsync(id!.Value, CancellationToken);
                    return medClinic != null;
                })
                .WithMessage("Такой поликлиники не существует!");

            RuleFor(x => x.Diagnosis)
               .NotNull()
               .NotEmpty()
               .WithMessage("Диагноз не должен быть пустым или null")
               .MustAsync(async (id, CancellationToken) =>
               {
                   var diagnosis = await diagnosisReadRepository.GetByIdAsync(id, CancellationToken);
                   return diagnosis != null;
               })
               .WithMessage("Такого диагноза не существует!");
        }
    }
}

using FluentValidation;
using Clinic.API.Models.Request;

namespace Clinic.API.Validators.Diagnosis
{
    public class DiagnosisRequestValidator : AbstractValidator<DiagnosisRequest>
    {
        public DiagnosisRequestValidator()
        {
            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Название не должно быть пустым или null");

            RuleFor(x => x.Medicament)
                .NotNull()
                .NotEmpty()
                .WithMessage("Препарат не должен быть пустым или null");
        }
    }
}

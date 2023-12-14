using FluentValidation;
using Clinic.API.Models.Request;
using Clinic.API.Models.CreateRequest;

namespace Clinic.API.Validators.Diagnosis
{
    public class CreateDiagnosisRequestValidator : AbstractValidator<CreateDiagnosisRequest>
    {
        public CreateDiagnosisRequestValidator()
        {
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

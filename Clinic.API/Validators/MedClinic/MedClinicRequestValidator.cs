using FluentValidation;
using Clinic.API.Models.Request;

namespace Clinic.API.Validators.MedClinic
{
    public class MedClinicRequestValidator : AbstractValidator<MedClinicRequest>
    {
        public MedClinicRequestValidator()
        {
            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.Address)
                .NotNull()
                .NotEmpty()
                .WithMessage("Адрес не должен быть пустым или null");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Номер не должен быть пустым или null");
        }
    }
}

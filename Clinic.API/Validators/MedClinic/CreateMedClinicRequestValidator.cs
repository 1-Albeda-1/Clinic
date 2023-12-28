using FluentValidation;
using Clinic.API.Models.Request;
using Clinic.API.Models.CreateRequest;

namespace Clinic.API.Validators.MedClinic
{
    /// <summary>
    /// Валидатор <see cref="MedClinicRequest"/>
    /// </summary>
    public class CreateMedClinicRequestValidator : AbstractValidator<CreateMedClinicRequest>
    {
        public CreateMedClinicRequestValidator()
        {
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

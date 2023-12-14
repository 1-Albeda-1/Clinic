using FluentValidation;
using Clinic.API.Models.Request;
using Clinic.API.Models.CreateRequest;

namespace Clinic.API.Validators.Doctor
{
    public class CreateDoctorRequestValidator : AbstractValidator<CreateDoctorRequest>
    {
        public CreateDoctorRequestValidator()
        {
            RuleFor(x => x.Surname)
                .NotNull()
                .NotEmpty()
                .WithMessage("Начало занятия не должно быть пустым или null");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Конец занятия не должно быть пустым или null");

            RuleFor(x => x.Patronymic)
                .NotNull()
                .NotEmpty()
                .WithMessage("Номер кабинета не должен быть пустым или null");

            RuleFor(x => x.CategoriesType)
                 .NotNull()
                 .WithMessage("Категория врача не должна быть null");

            RuleFor(x => x.DepartmentType)
                 .NotNull()
                 .WithMessage("Отделение не должно быть null");
        }
    }
}

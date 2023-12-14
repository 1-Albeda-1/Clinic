using FluentValidation;
using Clinic.API.Models.Request;

namespace Clinic.API.Validators.Doctor
{
    public class DoctorRequestValidator : AbstractValidator<DoctorRequest>
    {
        public DoctorRequestValidator()
        {

            RuleFor(x => x.Id)
              .NotNull()
              .NotEmpty()
              .WithMessage("Id не должен быть пустым или null");

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

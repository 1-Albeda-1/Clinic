using FluentValidation;
using Clinic.API.Validators.BookingAppointment;
using Clinic.API.Validators.Diagnosis;
using Clinic.API.Validators.Doctor;
using Clinic.API.Validators.MedClinic;
using Clinic.API.Validators.Patient;
using Clinic.API.Validators.TimeTable;
using Clinic.Repositories.Contracts;
using Clinic.Services.Contracts.Exceptions;
using Clinic.General;
using Clinic.Repositories.Contracts.ReadRepositoriesContracts;

namespace Clinic.API.Infrastructures.Validator
{
    internal sealed class ApiValidatorService : IApiValidatorService
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        public ApiValidatorService(IPatientReadRepository patientReadRepository,
            IDoctorReadRepository doctorReadRepository,
            IMedClinicReadRepository medClinicReadRepository,
            IDiagnosisReadRepository diagnosisReadRepository,
            ITimeTableReadRepository timeTableReadRepository)
        {
            Register<CreateDiagnosisRequestValidator>();
            Register<DiagnosisRequestValidator>();

            Register<CreateMedClinicRequestValidator>();
            Register<MedClinicRequestValidator>();

            Register<CreateDoctorRequestValidator>();
            Register<DoctorRequestValidator>();

            Register<CreatePatientRequestValidator>(medClinicReadRepository, diagnosisReadRepository);
            Register<PatientRequestValidator>(medClinicReadRepository, diagnosisReadRepository);

            Register<CreateTimeTableRequestValidator>(doctorReadRepository);
            Register<TimeTableRequestValidator>(doctorReadRepository);

            Register<CreateBookingAppointmentRequestValidator>(timeTableReadRepository, patientReadRepository);
            Register<BookingAppointmentRequestValidator>(timeTableReadRepository, patientReadRepository);
        }

        ///<summary>
        /// Регистрирует валидатор в словаре
        /// </summary>
        public void Register<TValidator>(params object[] constructorParams)
            where TValidator : IValidator
        {
            var validatorType = typeof(TValidator);
            var innerType = validatorType.BaseType?.GetGenericArguments()[0];
            if (innerType == null)
            {
                throw new ArgumentNullException($"Указанный валидатор {validatorType} должен быть generic от типа IValidator");
            }

            if (constructorParams?.Any() == true)
            {
                var validatorObject = Activator.CreateInstance(validatorType, constructorParams);
                if (validatorObject is IValidator validator)
                {
                    validators.TryAdd(innerType, validator);
                }
            }
            else
            {
                validators.TryAdd(innerType, Activator.CreateInstance<TValidator>());
            }
        }

        public async Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class
        {
            var modelType = model.GetType();
            if (!validators.TryGetValue(modelType, out var validator))
            {
                throw new InvalidOperationException($"Не найден валидатор для модели {modelType}");
            }

            var context = new ValidationContext<TModel>(model);
            var result = await validator.ValidateAsync(context, cancellationToken);

            if (!result.IsValid)
            {
                throw new ClinicValidationException(result.Errors.Select(x =>
                InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}

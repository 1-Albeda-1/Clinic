using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Clinic.API.Models.Exceptions;
using Clinic.Services.Contracts.Exceptions;
using Clinc.Services.Contracts.Exceptions;

namespace Clinic.API.Infrastructures
{
    /// <summary>
    /// Фильтр для обработки ошибок раздела администрирования
    /// </summary>
    public class ClinicExceptionFilter : IExceptionFilter
    {
        /// <inheritdoc/>
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception as ClinicException;
            if (exception == null)
            {
                return;
            }

            switch (exception)
            {
                case ClinicValidationException ex:
                    SetDataToContext(
                        new ConflictObjectResult(new ApiValidationExceptionDetail
                        {
                            Errors = ex.Errors,
                        }),
                        context);
                    break;

                case ClinicInvalidOperationException ex:
                    SetDataToContext(
                        new BadRequestObjectResult(new ApiExceptionDetail { Message = ex.Message, })
                        {
                            StatusCode = StatusCodes.Status406NotAcceptable,
                        },
                        context);
                    break;

                case ClinicNotFoundException ex:
                    SetDataToContext(new NotFoundObjectResult(new ApiExceptionDetail
                    {
                        Message = ex.Message,
                    }), context);
                    break;

                default:
                    SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail
                    {
                        Message = exception.Message,
                    }), context);
                    break;
            }
        }

        /// <summary>
        /// Определяет контекст ответа
        /// </summary>
        static protected void SetDataToContext(ObjectResult data, ExceptionContext context)
        {
            context.ExceptionHandled = true;
            var response = context.HttpContext.Response;
            response.StatusCode = data.StatusCode ?? StatusCodes.Status400BadRequest;
            context.Result = data;
        }
    }
}

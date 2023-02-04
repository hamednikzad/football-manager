using System.Net;
using FootballManager.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;

namespace FootballManager.Api.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("")]
public class ErrorController : ControllerBase
{
    private IActionResult HandleResponseException(ResponseException exception)
    {
        return ValidationProblem(modelStateDictionary: exception.Errors, title: exception.Message,
            statusCode: (int?)exception.StatusCode);
    }

    private IActionResult ErrorLocalDevelopmentHandler(IWebHostEnvironment webHostEnvironment)
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        Log.Logger.Error(context!.Error, "Error controller:");
        return AnalyseException(context.Error);
    }

    private ActionResult AnalyseException(Exception exception)
    {
        switch (exception)
        {
            case ResponseException responseException:
                return ValidationProblem(title: exception.Message,
                    statusCode: (int?)responseException.StatusCode,
                    modelStateDictionary: responseException.Errors);

            case ArgumentException argumentException:
                var dictionary = new ModelStateDictionary();
                dictionary.AddModelError(argumentException.ParamName!, argumentException.Message);
                return ValidationProblem(title: argumentException.Message,
                    statusCode: (int)HttpStatusCode.BadRequest, modelStateDictionary: dictionary);

            case NotFoundException notFoundException:
                return ValidationProblem(title: notFoundException.Message,
                    statusCode: (int?)HttpStatusCode.BadRequest);
            
            default:
                return Problem(title: exception.Message);
        }
    }
    
    [Route("error")]
    public IActionResult ErrorLocalDevelopment(
        [FromServices] IWebHostEnvironment webHostEnvironment)
    {
        return ErrorLocalDevelopmentHandler(webHostEnvironment);
    }
    
}
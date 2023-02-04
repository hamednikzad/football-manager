using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FootballManager.Application.Common.Exceptions;

public class ResponseException : Exception
{
    public HttpStatusCode? StatusCode { get; }
    public ModelStateDictionary? Errors { get; }

    public ResponseException(string message, HttpStatusCode? httpStatusCode, ModelStateDictionary? errors = null) : base(message)
    {
        StatusCode = httpStatusCode;
        Errors = errors;
    }
}
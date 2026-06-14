
using Microsoft.AspNetCore.Mvc;
using NutriFacts.Domain.Exceptions;

public static class ExceptionToProblemDetailsMapper
{
    public static ProblemDetails MapToProblemDetails(this Exception ex, string traceId)
    {
        return ex switch
        {
            ProductNotFoundException => new ProblemDetails
            {
                Type = "https://httpstatuses.com/404",
                Title = "Produto não encontrado",
                Status = StatusCodes.Status404NotFound,
                Detail = ex.Message,
                Extensions = { ["traceId"] = traceId }
            },
            InvalidEntryIdException or InvalidQuantityException or InvalidBarcodeException => new ProblemDetails
            {
                Type = "https://httpstatuses.com/400",
                Title = "Requisição inválida",
                Status = StatusCodes.Status400BadRequest,
                Detail = ex.Message,
                Extensions = { ["traceId"] = traceId }
            },
            InvalidUserIdException => new ProblemDetails
            {
                Type = "https://httpstatuses.com/401",
                Title = "Usuário inválido",
                Status = StatusCodes.Status401Unauthorized,
                Detail = ex.Message,
                Extensions = { ["traceId"] = traceId }
            },
            HttpRequestException => new ProblemDetails
            {
                Type = "https://httpstatuses.com/502",
                Title = "Serviço externo indisponível",
                Status = StatusCodes.Status502BadGateway,
                Detail = "Falha ao consultar serviço externo",
                Extensions = { ["traceId"] = traceId }
            },
            _ => new ProblemDetails
            {
                Type = "https://httpstatuses.com/500",
                Title = "Erro interno",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "Ocorreu um erro inesperado",
                Extensions = { ["traceId"] = traceId }
            }
        };
    }
}
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // On laisse la requête s'exécuter normalement
                await _next(context);
            }
            catch (Exception ex)
            {
                // Si un crash survient n'importe où dans le code, on l'attrape ici !
                _logger.LogError(ex, "Une erreur non gérée est survenue : {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Par défaut, toute erreur inconnue est une 500
            var statusCode = HttpStatusCode.InternalServerError;
            var title = "Une erreur interne du serveur est survenue.";

            // TRADUCTION DES EXCEPTIONS EN CODES HTTP 🎯
            switch (exception)
            {
                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound; // Transforme en 404 !
                    title = "Ressource introuvable.";
                    break;

                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest; // Transforme en 400 !
                    title = "Requête invalide.";
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            // Structure standard de réponse d'erreur (Problem Details)
            var problemDetails = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = title,
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            var jsonResult = JsonSerializer.Serialize(problemDetails);
            return context.Response.WriteAsync(jsonResult);
        }
    }
}
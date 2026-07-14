using ForecastExceptionPortal.Api.Models;
using ForecastExceptionPortal.Api.Requests;
using ForecastExceptionPortal.Api.Services;
using ForecastExceptionPortal.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ExceptionService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=forecast-exceptions.db");
}
);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();




app.MapGet("/api/health", () =>
{
    return Results.Ok(new
    {
        status = "ok",
        serviceName = "Forecast Exception Portal API",
        timestamp = DateTime.UtcNow
    });
});

app.MapGet("/api/exceptions", (string? status, string? severity, ExceptionService exceptionService) =>
{
    return Results.Ok(exceptionService.GetAll(status, severity));
});

app.MapGet("/api/exceptions/{id:int}", (int id, ExceptionService exceptionService) =>
{

    var exceptionRecord = exceptionService.GetById(id);
    return exceptionRecord is not null
    ? Results.Ok(exceptionRecord)
    : Results.NotFound();

});

app.MapGet("/api/exceptions/status/{status}", (string status, ExceptionService exceptionService) =>
{
    var matchingExceptions = exceptionService.GetAll(status, null);

    return Results.Ok(matchingExceptions);
});

app.MapPatch("/api/exceptions/{id:int}/status", (int id, UpdateExceptionStatusRequest request, ExceptionService exceptionService) =>
{
    if (string.IsNullOrWhiteSpace(request.Status))
    {
        return Results.BadRequest("Status cannot be null or empty.");
    }

    string updatedStatus;

    if (request.Status.Equals("New", StringComparison.OrdinalIgnoreCase))
    {
        updatedStatus = "New";
    }
    else if (request.Status.Equals("Investigating", StringComparison.OrdinalIgnoreCase))
    {
        updatedStatus = "Investigating";
    }
    else if (request.Status.Equals("Resolved", StringComparison.OrdinalIgnoreCase))
    {
        updatedStatus = "Resolved";
    }
    else
    {
        return Results.BadRequest("Invalid status value. Allowed values are: New, Investigating, Resolved.");
    }

    var updatedExceptionRecord = exceptionService.UpdateStatus(id, updatedStatus);

    if (updatedExceptionRecord is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(updatedExceptionRecord);
});

app.MapPatch("/api/exceptions/{id:int}/assignment", (int id, UpdateExceptionAssignmentRequest request, ExceptionService exceptionService) =>
{
    if (string.IsNullOrWhiteSpace(request.AssignedTo))
    {
        return Results.BadRequest("AssignedTo cannot be null or empty.");
    }

    var assignedTo = request.AssignedTo.Trim();

    var updatedExceptionRecord = exceptionService.UpdateAssignment(id, assignedTo);

    if (updatedExceptionRecord is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(updatedExceptionRecord);

});

app.MapDelete("/api/exceptions/{id:int}/assignment", (int id, ExceptionService exceptionService) =>
{

    var updatedExceptionRecord = exceptionService.ClearAssignment(id);

    if (updatedExceptionRecord is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(updatedExceptionRecord);

});

app.Run();

public partial class Program { }
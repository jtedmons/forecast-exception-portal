var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var exceptions = new List<ExceptionRecord>
{
    new(
        Id: 1,
        LocationName: "Store 183",
        BusinessDate: new DateTime(2026, 6, 15),
        Sales: 8450.25m,
        ExpectedSales: 10250.00m,
        SalesVariancePct: -0.1756m,
        Customers: 812,
        ExpectedCustomers: 950,
        CustomersVariancePct: -0.1453m,
        ExceptionScore: 0.42m,
        Severity: "At Risk",
        Status: "New",
        AssignedTo: null
    ),
    new(
        Id: 2,
        LocationName: "Store 421",
        BusinessDate: new DateTime(2026, 6, 15),
        Sales: 12200.00m,
        ExpectedSales: 9800.00m,
        SalesVariancePct: 0.2449m,
        Customers: 1105,
        ExpectedCustomers: 925,
        CustomersVariancePct: 0.1946m,
        ExceptionScore: 0.51m,
        Severity: "Critical",
        Status: "Investigating",
        AssignedTo: "Josh"
    )
};

app.MapGet("/api/health", () =>
{
    return Results.Ok(new
    {
        status = "ok",
        serviceName = "Forecast Exception Portal API",
        timestamp = DateTime.UtcNow
    });
});

app.MapGet("/api/exceptions", () =>
{
    return Results.Ok(exceptions);
});

app.MapGet("/api/exceptions/{id:int}", (int id) =>
{
    var exceptionRecord = exceptions.FirstOrDefault(e => e.Id == id);

    return exceptionRecord is null
        ? Results.NotFound()
        : Results.Ok(exceptionRecord);
});

app.MapGet("/api/exceptions/status/{status}", (string status) =>
{
    var matchingExceptions = exceptions.Where(e => e.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();

    return Results.Ok(matchingExceptions);
});

app.Run();

public record ExceptionRecord(
    int Id,
    string LocationName,
    DateTime BusinessDate,
    decimal Sales,
    decimal ExpectedSales,
    decimal SalesVariancePct,
    int Customers,
    int ExpectedCustomers,
    decimal CustomersVariancePct,
    decimal ExceptionScore,
    string Severity,
    string Status,
    string? AssignedTo
);
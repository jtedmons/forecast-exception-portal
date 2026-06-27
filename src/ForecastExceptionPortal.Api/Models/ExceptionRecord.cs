namespace ForecastExceptionPortal.Api.Models;

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
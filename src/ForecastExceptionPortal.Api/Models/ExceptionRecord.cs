namespace ForecastExceptionPortal.Api.Models;

public class ExceptionRecord
{
    public int Id { get; set; } 
    public string LocationName { get; set; } = String.Empty;
    public DateTime BusinessDate { get; set; }
    public decimal Sales { get; set; }
    public decimal ExpectedSales { get; set; }
    public decimal SalesVariancePct { get; set; }
    public int Customers { get; set; }
    public int ExpectedCustomers { get; set; }
    public decimal CustomersVariancePct { get; set; }
    public decimal ExceptionScore { get; set; }
    public string Severity { get; set; } = String.Empty;
    public string Status { get; set; } = String.Empty;
    public string? AssignedTo { get; set; }

}
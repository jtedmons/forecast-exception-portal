using ForecastExceptionPortal.Api.Models;

namespace ForecastExceptionPortal.Api.Services;

public class ExceptionService
{
    private readonly List<ExceptionRecord> _exceptions = new()
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

    public List<ExceptionRecord> GetAll(string? status, string? severity)
    {
        var filteredExceptions = _exceptions.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(status))
        {
            filteredExceptions = filteredExceptions.Where(e =>
                e.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(severity))
        {
            filteredExceptions = filteredExceptions.Where(e =>
                e.Severity.Equals(severity, StringComparison.OrdinalIgnoreCase));
        }

        return filteredExceptions.ToList();
    }

    public ExceptionRecord? GetById(int id)
    {
        return _exceptions.FirstOrDefault(e => e.Id == id);
    }

    public ExceptionRecord? UpdateStatus(int id, string newStatus)
    {
        var exceptionIndex = _exceptions.FindIndex(e => e.Id == id);
        if (exceptionIndex == -1)
        {
            return null;
        }

        var exceptionRecord = _exceptions[exceptionIndex];

        var updatedExceptionRecord = exceptionRecord with
        {
            Status = newStatus
        };

        _exceptions[exceptionIndex] = updatedExceptionRecord;

        return updatedExceptionRecord;
    }

    public ExceptionRecord? UpdateAssignment(int id, string newAssignedTo)
    {
        var exceptionIndex = _exceptions.FindIndex(e => e.Id == id);
        if (exceptionIndex == -1)
        {
            return null;
        }

        var exceptionRecord = _exceptions[exceptionIndex];

        var updatedExceptionRecord = exceptionRecord with
        {
            AssignedTo = newAssignedTo
        };

        _exceptions[exceptionIndex] = updatedExceptionRecord;

        return updatedExceptionRecord;
    }
    public ExceptionRecord? ClearAssignment(int id)
    {
        var exceptionIndex = _exceptions.FindIndex(e => e.Id == id);
        if (exceptionIndex == -1)
        {
            return null;
        }

        var exceptionRecord = _exceptions[exceptionIndex];

        var updatedExceptionRecord = exceptionRecord with
        {
            AssignedTo = null
        };

        _exceptions[exceptionIndex] = updatedExceptionRecord;

        return updatedExceptionRecord;
    }
}

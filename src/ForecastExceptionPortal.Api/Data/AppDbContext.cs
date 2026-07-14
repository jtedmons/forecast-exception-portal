using Microsoft.EntityFrameworkCore;
using ForecastExceptionPortal.Api.Models;

namespace ForecastExceptionPortal.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<ExceptionRecord> Exceptions { get; set; } = null!;


}
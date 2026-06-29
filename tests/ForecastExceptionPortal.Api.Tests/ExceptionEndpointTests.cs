namespace ForecastExceptionPortal.Api.Tests;

using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net.Http.Json;
using ForecastExceptionPortal.Api.Models;

public class ExceptionEndpointTests
{
    [Fact]
    public async Task GetExceptions_ReturnsOk()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        var response = await client.GetAsync("/api/exceptions");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetExceptionById_WithExistingId_ReturnsOk()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        var response = await client.GetAsync("/api/exceptions/1");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetExceptionById_WithMissingId_ReturnsNotFound()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        var response = await client.GetAsync("/api/exceptions/999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetExceptionsByStatus_ReturnsOk()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        var response = await client.GetAsync("/api/exceptions/status/new");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetExceptions_WithStatusAndSeverityQuery_ReturnsOk()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        var response = await client.GetAsync("/api/exceptions?status=Investigating&severity=Critical");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ClearExceptionAssignment_WithExistingId_ReturnsOk()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.DeleteAsync("/api/exceptions/2/assignment");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var updatedExceptionRecord = await response.Content.ReadFromJsonAsync<ExceptionRecord>();
        Assert.NotNull(updatedExceptionRecord);
        Assert.Null(updatedExceptionRecord.AssignedTo);
    }

    [Fact]
    public async Task ClearExceptionAssignment_WithMissingId_ReturnsNotFound()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.DeleteAsync("/api/exceptions/999/assignment");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
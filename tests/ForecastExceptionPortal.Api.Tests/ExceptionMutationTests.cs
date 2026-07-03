namespace ForecastExceptionPortal.Api.Tests;

using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net.Http.Json;
using ForecastExceptionPortal.Api.Models;

public class ExceptionMutationTests
{
    [Fact]
    public async Task PatchExceptionStatus_WithValidStatus_ReturnsOk()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.PatchAsync("/api/exceptions/1/status", JsonContent.Create(new { Status = "Resolved" }));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var updatedException = await response.Content.ReadFromJsonAsync<ExceptionRecord>();

        Assert.Equal("Resolved", updatedException!.Status);
    }

    [Fact]
    public async Task PatchExceptionStatus_WithInvalidStatus_ReturnsBadRequest()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.PatchAsync("/api/exceptions/1/status", JsonContent.Create(new { Status = "Closed" }));
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    }

    [Fact]
    public async Task PatchExceptionStatus_WithMissingId_ReturnsNotFound()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.PatchAsync("/api/exceptions/999/status", JsonContent.Create(new { Status = "Resolved" }));
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

    }

    [Fact]
    public async Task PatchExceptionAssignment_WithValidOwner_ReturnsOk()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.PatchAsync("/api/exceptions/1/assignment", JsonContent.Create(new { AssignedTo = " Josh " }));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var updatedException = await response.Content.ReadFromJsonAsync<ExceptionRecord>();

        Assert.Equal("Josh", updatedException!.AssignedTo);

    }

    [Fact]
    public async Task PatchExceptionAssignment_WithBlankOwner_ReturnsBadRequest()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.PatchAsync("/api/exceptions/1/assignment", JsonContent.Create(new { AssignedTo = "" }));
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    }

    [Fact]
    public async Task PatchExceptionAssignment_WithMissingId_ReturnsNotFound()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.PatchAsync("/api/exceptions/999/assignment", JsonContent.Create(new { AssignedTo = "Josh" }));
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

    }

}
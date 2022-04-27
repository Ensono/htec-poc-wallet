using System;
using System.Linq;
using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;
using HTEC.POC.API.Controllers;
using HTEC.POC.API.Models.Requests;
using HTEC.POC.CQRS.Commands;

namespace HTEC.POC.API.UnitTests.Controllers;

public class AddWalletCategoryControllerTests
{
    [Fact]
    public void AddWalletCategoryController_Should_BeDerivedFrom_ApiControllerBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(AddWalletCategoryController)
            .Should()
            .BeDerivedFrom<ApiControllerBase>();
    }

    [Fact]
    public void Constructor_Should_ThrowArgumentNullException_When_ICommandHandlerIsNull()
    {
        // Arrange
        // Act
        Action action = () => new AddWalletCategoryController(null);

        // Assert
        action
            .Should()
            .Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'commandHandler')");
    }

    [Fact]
    public void Constructor_Should_Not_Throw_When_ICommandHandlerIsPresent()
    {
        // Arrange
        // Act
        Action action = () => new AddWalletCategoryController(Substitute.For<ICommandHandler<CreateCategory, Guid>>());

        // Assert
        action
            .Should()
            .NotThrow();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ConsumesAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(DeleteCategoryController)
            .Should()
            .BeDecoratedWith<ConsumesAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ProducesAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(DeleteCategoryController)
            .Should()
            .BeDecoratedWith<ProducesAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ApiExplorerSettingsAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(DeleteCategoryController)
            .Should()
            .BeDecoratedWith<ApiExplorerSettingsAttribute>();
    }

    [Fact]
    public void AddWalletCategory_Should_BeDecoratedWith_HttpPostAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(AddWalletCategoryController)
            .Methods()
            .First(x => x.Name == "AddWalletCategory")
            .Should()
            .BeDecoratedWith<HttpPostAttribute>(attribute => attribute.Template == "/v1/wallet/{id}/category/");
    }

    [Fact]
    public void AddWalletCategory_Should_BeDecoratedWith_AuthorizeAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(AddWalletCategoryController)
            .Methods()
            .First(x => x.Name == "AddWalletCategory")
            .Should()
            .BeDecoratedWith<AuthorizeAttribute>();
    }

    [Fact]
    public void AddWalletCategory_Should_BeDecoratedWith_ProducesResponseTypeAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(AddWalletCategoryController)
            .Methods()
            .First(x => x.Name == "AddWalletCategory")
            .Should()
            .BeDecoratedWith<ProducesResponseTypeAttribute>();
    }

    [Fact]
    public async Task AddWalletCategory_Should_Return_StatusCodeCreated()
    {
        // Arrange
        var body = new CreateCategoryRequest
        {
            Name = "testName",
            Description = "testDescription"
        };
        var fakeCommandHandler = Substitute.For<ICommandHandler<CreateCategory, Guid>>();
        var categoryId = Guid.NewGuid();
        var correlationId = Guid.NewGuid();
        fakeCommandHandler.HandleAsync(Arg.Any<CreateCategory>()).Returns(Task.FromResult(categoryId));

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["x-correlation-id"] = correlationId.ToString();

        var sut = new AddWalletCategoryController(fakeCommandHandler)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            }
        };

        // Act
        var result = await sut.AddWalletCategory(Guid.Empty, body);

        // Assert
        await fakeCommandHandler.Received().HandleAsync(Arg.Any<CreateCategory>());

        result
            .Should()
            .BeOfType<ObjectResult>()
            .Which
            .StatusCode?
            .Should()
            .Be(201);
    }
}

using System;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Htec.Poc.API.Controllers;

namespace Htec.Poc.API.UnitTests.Controllers;

public class GetWalletByIdV2ControllerTests
{
    [Fact]
    public void GetWalletByIdV2Controller_Should_BeDerivedFrom_ApiControllerBase()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetWalletByIdV2Controller)
            .Should()
            .BeDerivedFrom<ApiControllerBase>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ConsumesAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetWalletByIdV2Controller)
            .Should()
            .BeDecoratedWith<ConsumesAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ProducesAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetWalletByIdV2Controller)
            .Should()
            .BeDecoratedWith<ProducesAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ApiExplorerSettingsAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetWalletByIdV2Controller)
            .Should()
            .BeDecoratedWith<ApiExplorerSettingsAttribute>();
    }

    [Fact]
    public void Constructor_Should_BeDecoratedWith_ApiControllerAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetWalletByIdV2Controller)
            .Should()
            .BeDecoratedWith<ApiControllerAttribute>();
    }

    [Fact]
    public void GetWalletV2_Should_BeDecoratedWith_HttpGetAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetWalletByIdV2Controller)
            .Methods()
            .First(x => x.Name == "GetWalletV2")
            .Should()
            .BeDecoratedWith<HttpGetAttribute>(attribute => attribute.Template == "/v2/wallet/{id}");
    }

    [Fact]
    public void GetWalletV2_Should_BeDecoratedWith_AuthorizeAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetWalletByIdV2Controller)
            .Methods()
            .First(x => x.Name == "GetWalletV2")
            .Should()
            .BeDecoratedWith<AuthorizeAttribute>();
    }

    [Fact]
    public void GetWalletV2_Should_BeDecoratedWith_ProducesResponseTypeAttribute()
    {
        // Arrange
        // Act
        // Assert
        typeof(GetWalletByIdV2Controller)
            .Methods()
            .First(x => x.Name == "GetWalletV2")
            .Should()
            .BeDecoratedWith<ProducesResponseTypeAttribute>();
    }

    [Fact]
    public void GetWalletV2_Should_Return_StatusCodeNoContent()
    {
        // Arrange
        var sut = new GetWalletByIdV2Controller();

        // Act
        var result = sut.GetWalletV2(Guid.Empty);

        // Assert
        result
            .Should()
            .BeOfType<BadRequestResult>();
    }
}

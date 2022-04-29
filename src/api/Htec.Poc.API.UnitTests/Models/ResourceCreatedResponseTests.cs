using System;
using System.Linq;
using FluentAssertions;
using Xunit;
using Htec.Poc.API.Models.Responses;

namespace Htec.Poc.API.UnitTests.Models;

public class ResourceCreatedResponseTests
{
    [Fact]
    public void Id_Should_ReturnGuid()
    {
        // Arrange
        // Act
        // Assert
        typeof(ResourceCreatedResponse)
            .Properties()
            .First(x => x.Name == "Id")
            .Should()
            .Return<Guid>();
    }
}

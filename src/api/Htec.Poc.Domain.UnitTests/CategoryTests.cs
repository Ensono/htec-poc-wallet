﻿using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;
using Htec.Poc.Domain.Entities;

namespace Htec.Poc.Domain.UnitTests;

[Trait("TestType", "UnitTests")]
public class CategoryTests
{
    [Theory, AutoData]
    public void Constructor(
        Guid categoryId,
        string name,
        string description)
    {
        // Arrange
        // Act
        var category = new Category(categoryId, name, description);

        // Assert
        category.Id.Should().Be(categoryId);
        category.Name.Should().Be(name);
        category.Description.Should().Be(description);
    }
}

﻿using LighthouseSocial.Application.Dtos;
using LighthouseSocial.Application.Features.Lighthouse;
using LighthouseSocial.Domain.Countries;
using LighthouseSocial.Domain.Interfaces;
using Moq;

namespace LighthouseSocial.Tests.Features.Lighthouse;

public class CreateLighthouseHandlerTests
{
    private readonly Mock<ILighthouseRepository> _repositoryMock;
    private readonly Mock<ICountryRegister> _registryMock;
    private readonly CreateLighthouseHandler _handler;

    public CreateLighthouseHandlerTests()
    {
        _repositoryMock = new Mock<ILighthouseRepository>();
        _registryMock = new Mock<ICountryRegister>();
        _handler = new CreateLighthouseHandler(_repositoryMock.Object, _registryMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnSuccess_WhenInputIsValid()
    {
        // Arrange
        var dto = new LighthouseDto(Guid.Empty, "Roman Rock", 27, 34.10, 34.13);
        var country = new Country(27, "South Africa");

        _registryMock.Setup(r => r.GetById(dto.CountryId)).Returns(country);

        // Act
        var result = await _handler.HandleAsync(dto);

        // Assert
        Assert.True(result.Success);
        Assert.NotEqual(Guid.Empty, result.Data);

        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Domain.Entities.Lighthouse>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnFail_WhenCountryNotFound()
    {
        // Arrange
        var dto = new LighthouseDto(Guid.Empty, "Green Point", 999, 0, 0);
        _registryMock.Setup(r => r.GetById(It.IsAny<int>())).Throws(new Exception("Not found"));

        // Act
        var result = await _handler.HandleAsync(dto);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("Invalid country", result.ErrorMessage);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnFail_WhenLighthouseNameIsEmpty()
    {
        // Arrange
        var dto = new LighthouseDto(Guid.Empty, string.Empty, 27, 0, 0);
        var country = new Country(27, "South Africa");

        _registryMock.Setup(r => r.GetById(dto.CountryId)).Returns(country);

        // Act
        var result = await _handler.HandleAsync(dto);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("Lighthouse name is required", result.ErrorMessage);
    }
}
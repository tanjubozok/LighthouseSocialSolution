using FluentValidation;
using LighthouseSocial.Application.Common;
using LighthouseSocial.Application.Dtos;
using LighthouseSocial.Domain.Countries;
using LighthouseSocial.Domain.Interfaces;
using LighthouseSocial.Domain.ValueObjects;

namespace LighthouseSocial.Application.Features.Lighthouse;

public class CreateLighthouseHandler
{
    private readonly ILighthouseRepository _lighthouseRepository;
    private readonly ICountryRegister _countryRegister;
    private readonly IValidator<LighthouseDto> _validator;

    public CreateLighthouseHandler(ILighthouseRepository lighthouseRepository, ICountryRegister countryRegister, IValidator<LighthouseDto> validator)
    {
        _lighthouseRepository = lighthouseRepository;
        _countryRegister = countryRegister;
        _validator = validator;
    }

    public async Task<Result<Guid>> HandleAsync(LighthouseDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Result<Guid>.Fail($"Validation failed: {errors}");
        }

        Country? country = _countryRegister.GetById(dto.CountryId);
        if (country == null)
            return Result<Guid>.Fail($"Country with ID {dto.CountryId} not found.");

        try
        {
            var location = new Coordinates(dto.Latitude, dto.Longitude);
            var lighthouse = new Domain.Entities.Lighthouse(dto.Name, country, location);

            await _lighthouseRepository.AddAsync(lighthouse);

            return Result<Guid>.Ok(lighthouse.Id);
        }
        catch (Exception ex)
        {
            return Result<Guid>.Fail($"Failed to create lighthouse: {ex.Message}");
        }
    }
}

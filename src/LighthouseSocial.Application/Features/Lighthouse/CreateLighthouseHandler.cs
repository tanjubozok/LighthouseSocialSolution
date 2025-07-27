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

    public CreateLighthouseHandler(ILighthouseRepository lighthouseRepository, ICountryRegister countryRegister)
    {
        _lighthouseRepository = lighthouseRepository;
        _countryRegister = countryRegister;
    }

    public async Task<Result<Guid>> HandleAsync(LighthouseDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return Result<Guid>.Fail("Lighthouse name cannot be empty.");

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

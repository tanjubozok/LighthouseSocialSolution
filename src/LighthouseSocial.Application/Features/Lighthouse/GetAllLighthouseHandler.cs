using LighthouseSocial.Application.Common;
using LighthouseSocial.Application.Dtos;
using LighthouseSocial.Domain.Interfaces;

namespace LighthouseSocial.Application.Features.Lighthouse;

public class GetAllLighthouseHandler
{
    private readonly ILighthouseRepository _lighthouseRepository;

    public GetAllLighthouseHandler(ILighthouseRepository lighthouseRepository)
    {
        _lighthouseRepository = lighthouseRepository;
    }

    public async Task<Result<IEnumerable<LighthouseDto>>> HandleAsync()
    {
        var lighthouses = await _lighthouseRepository.GetAllAsync();
        if (lighthouses == null || !lighthouses.Any())
        {
            return Result<IEnumerable<LighthouseDto>>.Fail("No lighthouses found.");
        }

        var listDto = lighthouses.Select(x => new LighthouseDto(
            x.Id,
            x.Name,
            x.CountryId,
            x.Location.Latitude,
            x.Location.Longitude
        ));

        return Result<IEnumerable<LighthouseDto>>.Ok(listDto);
    }
}
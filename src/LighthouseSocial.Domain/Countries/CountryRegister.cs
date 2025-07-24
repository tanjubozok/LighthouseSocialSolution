namespace LighthouseSocial.Domain.Countries;

public class CountryRegister(IEnumerable<Country> countries)
        : ICountryRegister
{
    private readonly Dictionary<int, Country> _countries = countries.ToDictionary(x => x.Id);

    public IReadOnlyList<Country> GetAll()
    {
        return [.. _countries.Values];
    }

    public Country GetById(int id)
    {
        return _countries.TryGetValue(id, out var country)
            ? country
            : throw new KeyNotFoundException($"Country with ID {id} not found.");
    }
}
﻿using LighthouseSocial.Domain.Common;
using LighthouseSocial.Domain.Countries;
using LighthouseSocial.Domain.ValueObjects;

namespace LighthouseSocial.Domain.Entities;

public class Lighthouse
    : EntityBase
{
    public string Name { get; private set; }

    public int CountryId { get; private set; }
    public Country Country { get; private set; }

    public Coordinates Location { get; private set; }
    public List<Photo> Photos { get; set; } = [];

    protected Lighthouse() { }

    public Lighthouse(string name, Country country, Coordinates location)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Country = country ?? throw new ArgumentNullException(nameof(country));
        CountryId = country.Id;
        Location = location ?? throw new ArgumentNullException(nameof(location));
    }
}
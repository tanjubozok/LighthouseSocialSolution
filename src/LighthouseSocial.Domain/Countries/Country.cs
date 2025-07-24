namespace LighthouseSocial.Domain.Countries;

public class Country
{
    internal Country(int id, string name)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public int Id { get; }
    public string Name { get; }

    override public string ToString() => Name;
}
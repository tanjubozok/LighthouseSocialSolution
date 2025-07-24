using LighthouseSocial.Domain.Common;

namespace LighthouseSocial.Domain.Enumerations;

public sealed class PhotoCategory
    : EnumerationBase
{
    public static readonly PhotoCategory Profile = new(1, nameof(Profile));
    public static readonly PhotoCategory Cover = new(2, nameof(Cover));
    public static readonly PhotoCategory Post = new(3, nameof(Post));
    public static readonly PhotoCategory Story = new(4, nameof(Story));
    public static readonly PhotoCategory Gallery = new(5, nameof(Gallery));
    public static readonly PhotoCategory Event = new(6, nameof(Event));
    public static readonly PhotoCategory Album = new(7, nameof(Album));
    public static readonly PhotoCategory Other = new(8, nameof(Other));
    private PhotoCategory(int id, string name) : base(id, name) { }

    public static IEnumerable<PhotoCategory> List() => [Profile, Cover, Post, Story, Gallery, Event, Album, Other];

    public static PhotoCategory From(int id) =>
        List().SingleOrDefault(x => x.Id == id)
        ?? throw new ArgumentException($"No PhotoCategory with id {id} found.", nameof(id));
}

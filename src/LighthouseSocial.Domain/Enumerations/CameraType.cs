using LighthouseSocial.Domain.Common;

namespace LighthouseSocial.Domain.Enumerations;

public sealed class CameraType
    : EnumerationBase
{
    private CameraType(int id, string name) : base(id, name) { }

    public static readonly CameraType SLR = new(1, "SLR");
    public static readonly CameraType DSLR = new(2, "DSLR");
    public static readonly CameraType Mirrorless = new(3, "Mirrorless");
    public static readonly CameraType PointAndShoot = new(4, "Point and Shoot");
    public static readonly CameraType Film = new(5, "Film");
    public static readonly CameraType ActionCamera = new(6, "Action Camera");
    public static readonly CameraType Drone = new(7, "Drone");
    public static readonly CameraType InstantCamera = new(8, "Instant Camera");
    public static readonly CameraType Webcam = new(9, "Webcam");
    public static readonly CameraType Smartphone = new(10, "Smartphone");

    public static IEnumerable<CameraType> List() =>
    [
        SLR,
        DSLR,
        Mirrorless,
        PointAndShoot,
        Film,
        ActionCamera,
        Drone,
        InstantCamera,
        Webcam,
        Smartphone
    ];
    public static CameraType FromId(int id) =>
        List().FirstOrDefault(x => x.Id == id)
        ?? throw new ArgumentException($"No CameraType with ID {id} found.");

    public static CameraType FromName(string name) =>
        List().FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
        ?? throw new ArgumentException($"No CameraType with Name '{name}' found.");
}

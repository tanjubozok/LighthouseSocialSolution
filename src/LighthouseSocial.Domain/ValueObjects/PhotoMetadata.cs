namespace LighthouseSocial.Domain.ValueObjects;

public record class PhotoMetadata(string Lens, string Resolution, string CameraModel, DateTime TakenAt);
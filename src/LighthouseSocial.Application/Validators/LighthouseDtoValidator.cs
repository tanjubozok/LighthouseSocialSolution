using FluentValidation;
using LighthouseSocial.Application.Dtos;

namespace LighthouseSocial.Application.Validators;

public class LighthouseDtoValidator : AbstractValidator<LighthouseDto>
{
    public LighthouseDtoValidator()
    {
        this.RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.CountryId)
            .GreaterThan(0).WithMessage("CountryId must be greater than 0.");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90 degrees.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180 degrees.");
    }
}
using AssetMarketplace.Application.DTOs;
using AssetMarketplace.Domain.Constants;
using FluentValidation;

namespace AssetMarketplace.Application.Validation;

public sealed class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(createUserDto => createUserDto.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Incorrect format");

        RuleFor(createUserDto => createUserDto.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(ValidationConstants.MinPasswordLength).WithMessage($"Password can`t be smaller than {ValidationConstants.MinPasswordLength} symbols");

        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage("A non existent role was specified. Valid values: 0, 1, 2, 4.");
    }
}

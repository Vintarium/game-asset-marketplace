using AssetMarketplace.Application.DTOs;
using FluentValidation;

namespace AssetMarketplace.Application.Validation;

public sealed class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(updateUserDto => updateUserDto.Email)
       .NotEmpty().WithMessage("Email cannot be empty")
       .EmailAddress().WithMessage("Incorrect format");
    }
}

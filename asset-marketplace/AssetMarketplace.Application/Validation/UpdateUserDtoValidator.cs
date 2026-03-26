using AssetMarketplace.Application.DTOs;
using FluentValidation;

namespace AssetMarketplace.Application.Validation;

public sealed class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    private const string EmailEmptyMessage = "Email cannot be empty";
    private const string EmailInvalidMessage = "Incorrect format";
    public UpdateUserDtoValidator()
    {
        RuleFor(updateUserDto => updateUserDto.Email)
            .NotEmpty().WithMessage(EmailEmptyMessage)
            .EmailAddress().WithMessage(EmailInvalidMessage);
    }
}

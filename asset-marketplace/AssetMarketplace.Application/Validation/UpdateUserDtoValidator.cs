using AssetMarketplace.Application.DTOs;
using AssetMarketplace.Domain.Constants;
using FluentValidation;

namespace AssetMarketplace.Application.Validation;

public sealed class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(updateUserDto => updateUserDto.Email)
            .NotEmpty().WithMessage(ValidationMessages.EmailEmpty)
            .EmailAddress().WithMessage(ValidationMessages.EmailInvalid);
    }
}

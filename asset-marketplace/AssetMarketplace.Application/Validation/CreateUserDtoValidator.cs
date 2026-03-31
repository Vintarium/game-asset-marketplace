using AssetMarketplace.Application.DTOs;
using AssetMarketplace.Domain.Constants;
using AssetMarketplace.Domain.Interfaces;
using FluentValidation;

namespace AssetMarketplace.Application.Validation;

public sealed class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator(IUserRepository userRepository)
    {
        RuleFor(createUserDto => createUserDto.Email)
            .NotEmpty().WithMessage(ValidationMessages.EmailEmpty)
            .EmailAddress().WithMessage(ValidationMessages.EmailInvalid);

        RuleFor(createUserDto => createUserDto.Password)
            .NotEmpty().WithMessage(ValidationMessages.PasswordEmpty)
            .MinimumLength(ValidationConstants.MinPasswordLength).WithMessage($"{ValidationMessages.PasswordInvalid} {ValidationConstants.MinPasswordLength}");

        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage(ValidationMessages.RoleInvalid);
    }
}

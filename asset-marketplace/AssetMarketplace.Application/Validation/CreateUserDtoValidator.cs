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
            .NotEmpty().WithMessage(ValidationMessages.EmptyValue("Email"))
            .EmailAddress().WithMessage(ValidationMessages.InvalidValue("Email"));

        RuleFor(createUserDto => createUserDto.Password)
            .NotEmpty().WithMessage(ValidationMessages.EmptyValue("Password"))
            .MinimumLength(ValidationConstants.MinPasswordLength).WithMessage($"{ValidationMessages.InvalidValue("Password")}");

        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage(ValidationMessages.InvalidRole("Role"));
    }
}

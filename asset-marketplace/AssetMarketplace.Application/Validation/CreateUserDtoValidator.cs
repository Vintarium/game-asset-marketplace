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
            .NotEmpty().WithMessage(ValidationMessages.EmptyValue(nameof(CreateUserDto.Email)))
            .EmailAddress().WithMessage(ValidationMessages.InvalidValue(nameof(CreateUserDto.Password)));

        RuleFor(createUserDto => createUserDto.Password)
            .NotEmpty().WithMessage(ValidationMessages.EmptyValue(nameof(CreateUserDto.Password)))
            .MinimumLength(ValidationConstants.MinPasswordLength).WithMessage($"{ValidationMessages.InvalidValue("Password")}");

        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage(ValidationMessages.InvalidRole(nameof(CreateUserDto.Role)));
    }
}

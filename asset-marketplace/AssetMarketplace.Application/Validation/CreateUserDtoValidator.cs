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
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Incorrect format")

            .MustAsync(async (email, CancellationToken) =>
            {
                var existingUser = await userRepository.GetByEmailAsync(email, CancellationToken);

                return existingUser is null;
            })
            .WithMessage("User with this email already exists");

        RuleFor(createUserDto => createUserDto.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(ValidationConstants.MinPasswordLength).WithMessage($"Password can`t be smaller than {ValidationConstants.MinPasswordLength} symbols");

        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage("A non existent role was specified. Valid values: 0, 1, 2, 4.");
    }
}

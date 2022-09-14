using FluentValidation;

using Core.DTOs;

using SharedSources.Errors;

namespace Core.Validations;

/// <summary>
/// <see cref="AccessModelDTO"/> validation class
/// </summary>
public sealed class AccessModelDTOValidation : AbstractValidator<AccessModelDTO>
{
    /// <summary>
    /// <see cref="AccessModelDTOValidation"/> ctor
    /// </summary>
    public AccessModelDTOValidation()
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
                .WithMessage(ExceptionsTranslated.requiredField)
            .EmailAddress()
                .WithMessage(ExceptionsTranslated.invalidEmail)
            .MaximumLength(100)
                .WithMessage(ExceptionsTranslated.longField);

        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
                .WithMessage(ExceptionsTranslated.requiredField)
            .MinimumLength(6)
                .WithMessage(ExceptionsTranslated.shortField)
            .MaximumLength(64)
                .WithMessage(ExceptionsTranslated.longField);

            // if need lowercase into password
            //.Matches("[a-z]").WithMessage(ErrorsTranslated.lowercaseLetterRequired)
            // if need numbers into password
            //.Matches("[0-9]").WithMessage(ErrorsTranslated.digitRequired);
            // if need uppercase into password
            //.Matches("[A-Z]").WithMessage(ErrorsTranslated.uppercaseLetterRequired);
            // if need special characters
            //.Matches("[^a-zA-Z0-9]").WithMessage(ErrorsTranslated.specialCharacterRequired);
    }
}

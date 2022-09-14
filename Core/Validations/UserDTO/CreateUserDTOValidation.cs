using FluentValidation;

using Core.DTOs;

using SharedSources.Errors;
using Core.Helpers;

namespace Core.Validations;

/// <summary>
/// <see cref="CreateUserDTO"/> validation class
/// </summary>
public sealed class CreateUserDTOValidation : AbstractValidator<CreateUserDTO>
{
    /// <summary>
    /// <see cref="CreateUserDTOValidation"/> ctor
    /// </summary>
    public CreateUserDTOValidation()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Continue)
            .GenericRequiredValidations()
            .GenericLengthValidations();

        RuleFor(x => x.LastName)
            .Cascade(CascadeMode.Continue)
            .GenericRequiredValidations()
            .GenericLengthValidations();

        RuleFor(x => x.UserName)
            .Cascade(CascadeMode.Continue)
            .GenericRequiredValidations()
            .GenericLengthValidations();

        RuleFor(x => x.Role)
            .Cascade(CascadeMode.Continue)
            .GenericRequiredValidations()
            .GenericLengthValidations();
    }

}

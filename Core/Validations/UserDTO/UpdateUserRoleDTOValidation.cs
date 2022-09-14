using FluentValidation;

using Core.DTOs;
using Core.Helpers;

namespace Core.Validations;

/// <summary>
/// <see cref="UpdateUserRoleDTOValidation"/> validation class
/// </summary>
public sealed class UpdateUserRoleDTOValidation : AbstractValidator<UpdateUserRoleDTO>
{
    /// <summary>
    /// <see cref="UpdateUserNameAndLastnameDTOValidation"/> ctor
    /// </summary>
    public UpdateUserRoleDTOValidation()
    {
        RuleFor(x => x.Role)
             .Cascade(CascadeMode.Continue)
             .GenericRequiredValidations()
             .GenericLengthValidations();
    }
}

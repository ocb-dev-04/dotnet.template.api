using FluentValidation;

using Core.DTOs;
using Core.Helpers;

namespace Core.Validations;

/// <summary>
/// <see cref="UpdateUserGeneralDataDTO"/> validation class
/// </summary>
public sealed class UpdateUserNameAndLastnameDTOValidation : AbstractValidator<UpdateUserGeneralDataDTO>
{
    /// <summary>
    /// <see cref="UpdateUserNameAndLastnameDTOValidation"/> ctor
    /// </summary>
    public UpdateUserNameAndLastnameDTOValidation()
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
    }
}

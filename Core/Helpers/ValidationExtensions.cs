using FluentValidation;

using SharedSources.Errors;

namespace Core.Helpers;

/// <summary>
/// <see cref="ValidationsExtensions"/> extensions methods
/// </summary>
public static class ValidationsExtensions
{
    /// <summary>
    /// Set prop as not null or empty
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="ruleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, TProperty> GenericRequiredValidations<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        => ruleBuilder.NotEmpty()
                .WithMessage(ExceptionsTranslated.requiredField)
            .NotNull()
                .WithMessage(ExceptionsTranslated.requiredField);

    /// <summary>
    /// Set a min - max length
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ruleBuilder"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> GenericLengthValidations<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder.MinimumLength(2)
                .WithMessage(ExceptionsTranslated.shortField)
            .MaximumLength(64)
                .WithMessage(ExceptionsTranslated.longField);
}

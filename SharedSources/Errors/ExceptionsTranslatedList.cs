namespace SharedSources.Errors;

/// <summary>
/// Class to make all error on server
/// </summary>
public static class ExceptionsTranslated
{
    public const string unknowError = "unknowError";
    //public const string emailRegistered = "emailRegistered";
    public const string invalidEmail = "invalidEmail";
    public const string emailNotFound = "emailNotFound";
    public const string requiredField = "requiredField";
    public const string wrongPassword = "wrongPassword";
    public const string needAuthenticate = "needAuthentication"; 
    public const string shortField = "shortField";
    public const string longField = "longField";
    public const string errorWhileSignup = "errorWhileSignup";
    public const string errorWhileProccess = "errorWhileProccess";

    public const string nothingChanged = "nothingChanged";
    public const string notFound = "notFound";
    public const string noHavePermissions = "noHavePermissions";

    public const string digitRequired = "digitRequired";
    public const string lowercaseLetterRequired = "lowercaseLetterRequired";
    public const string uppercaseLetterRequired = "uppercaseLetterRequired";
    public const string passwordSpecialCharacter = "specialCharacterRequired";

    public const string needHigherValue = "needHigherValue";
    public const string needLowerValue = "needLowerValue";
    public const string duplicated = "duplicated";

    // not found
    public const string userNotFound = "userNotFound";
    public const string userAlreadyExists = "userAlreadyExists";
    public const string credentialsNotFound = "credentialsNotFound";
    public const string credentialsAlreadyExists = "credentialsAlreadyExists";
}

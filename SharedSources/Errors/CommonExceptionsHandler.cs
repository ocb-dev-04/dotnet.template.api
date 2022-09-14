using System.Net;

namespace SharedSources.Errors;

/// <summary>
/// <see cref="CommonExceptionsHandler"/> class
/// </summary>
public static class CommonExceptionsHandler
{
    /// <summary>
    /// User not found
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public static void UserNotFound()
    {
        string error = ExceptionsBuilder.Build((int)HttpStatusCode.NotFound, ExceptionsTranslated.userNotFound);
        throw new ArgumentException(error);
    }
    
    /// <summary>
    /// User already exist
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public static void UserAlreadyExist()
    {
        string error = ExceptionsBuilder.Build((int)HttpStatusCode.NotFound, ExceptionsTranslated.userAlreadyExists);
        throw new ArgumentException(error);
    }

    /// <summary>
    /// Duplicated data
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public static void Duplicated() 
    {
            string error = ExceptionsBuilder.Build((int)HttpStatusCode.BadRequest, ExceptionsTranslated.duplicated);
            throw new ArgumentException(error);
    }

    /// <summary>
    /// Nothing changed
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public static void NothingChanged() 
    {
            string error = ExceptionsBuilder.Build((int)HttpStatusCode.BadRequest, ExceptionsTranslated.nothingChanged);
            throw new ArgumentException(error);
    }

    /// <summary>
    /// Wrong password
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public static void WrongPassword() 
    {
            string error = ExceptionsBuilder.Build((int)HttpStatusCode.BadRequest, ExceptionsTranslated.wrongPassword);
            throw new ArgumentException(error);
    }

    /// <summary>
    /// Credentials not found
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public static void CredentialsNotFound()
    {
        string error = ExceptionsBuilder.Build((int)HttpStatusCode.NotFound, ExceptionsTranslated.credentialsNotFound);
        throw new ArgumentException(error);
    }

    /// <summary>
    /// Credentials already exist
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public static void CredentialsAlreadyExist()
    {
        string error = ExceptionsBuilder.Build((int)HttpStatusCode.NotFound, ExceptionsTranslated.credentialsAlreadyExists);
        throw new ArgumentException(error);
    }

    /// <summary>
    /// Need authenticate
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public static void NeedAuthenticate() 
    {
            string error = ExceptionsBuilder.Build((int)HttpStatusCode.BadRequest, ExceptionsTranslated.needAuthenticate);
            throw new ArgumentException(error);
    }

    /// <summary>
    /// Internal server error
    /// </summary>
    public static void ErrorWhileProccess() 
          => CommonExceptionsHandler.ErrorWhileProccess();
}
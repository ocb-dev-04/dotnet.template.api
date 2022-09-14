using Microsoft.AspNetCore.Mvc;

using Core.DTOs;
using Core.Models;
using Core.Interfaces;
using SharedSources.Models;

namespace API.Controllers;

/// <summary>
/// <see cref="AuthController"/> class
/// </summary>
[Route("api/v1/[controller]")]
[Produces("application/json")]
[ApiController]
public sealed class AuthController : BaseController
{
    #region Props & Ctor

    private readonly IAuthCommand _authCommand;

    /// <summary>
    /// <see cref="AuthController"/> ctor
    /// </summary>
    /// <param name="authCommand"></param>
    public AuthController(IAuthCommand authCommand)
    {
        _authCommand = authCommand;
    }

    #endregion

    /// <summary>
    /// Login with credentials
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandarResponseModel<JwtModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(StandarResponseModel<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandarResponseModel<string>))]
    public async Task<IActionResult> Login([FromBody] AccessModelDTO data)
        => StandarOk<JwtModel>(await _authCommand.Login(data));

    /// <summary>
    /// Signup with credentials
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost("signup")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandarResponseModel<JwtModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(StandarResponseModel<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandarResponseModel<string>))]
    public async Task<IActionResult> Signup([FromBody] AccessModelDTO data)
        => StandarOk<JwtModel>(await _authCommand.Signup(data));
}

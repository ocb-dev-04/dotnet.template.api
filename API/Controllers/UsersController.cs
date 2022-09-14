using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using Core.DTOs;

using Core.Interfaces;
using SharedSources.Models;

namespace API.Controllers;

/// <summary>
/// <see cref="UsersController"/> class
/// </summary>
[Route("api/v1/[controller]")]
[Produces("application/json")]
[ApiController]
public sealed class UsersController : BaseController
{
    #region Props

    private readonly IUserQueries _userQueries;
    private readonly IUserCommand _userCommand;

    /// <summary>
    /// <see cref="UsersController"/> contructor
    /// </summary>
    /// <param name="userQueries"></param>
    /// <param name="userCommand"></param>
    public UsersController(
        IUserQueries userQueries,
        IUserCommand userCommand)
    {
        _userQueries = userQueries;
        _userCommand = userCommand;
    }

    #endregion

    #region Sigle queries

    /// <summary>
    /// Get account by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("by-id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandarResponseModel<UserDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(StandarResponseModel<string>))]
    public async Task<IActionResult> GetById([FromQuery, Required] int id)
        => StandarOk<UserDTO>(await _userQueries.GetById(id));

    /// <summary>
    /// Get account data with jwt based
    /// </summary>
    /// <returns></returns>
    [HttpGet("self-data")]
    [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(StandarResponseModel<UserDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(StandarResponseModel<string>))]
    public async Task<IActionResult> GetSelfAccountData()
        => StandarOk<UserDTO>(await _userQueries.GetSelfUserData());

    #endregion

    #region Commands methods

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandarResponseModel<UserDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandarResponseModel<string>))]
    public async Task<IActionResult> Create([FromBody] CreateUserDTO data)
        => StandarOk<UserDTO>(await _userCommand.Create(data));

    /// <summary>
    /// Update general user data
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPatch("update-general-data")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandarResponseModel<UserDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(StandarResponseModel<string>))]
    public async Task<IActionResult> UpdateGeneralData([FromBody] UpdateUserGeneralDataDTO data)
        => StandarOk<UserDTO>(await _userCommand.UpdateGeneralData(data));
    
    /// <summary>
    /// Update user role
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPatch("update-role")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandarResponseModel<UserDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(StandarResponseModel<string>))]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateUserRoleDTO data)
        => StandarOk<UserDTO>(await _userCommand.UpdateRole(data));

    /// <summary>
    /// Remove user
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandarResponseModel<UserDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(StandarResponseModel<string>))]
    public async Task<IActionResult> Remove([FromQuery, Required] int id)
        => StandarOk<string>(string.Empty);

    #endregion
}

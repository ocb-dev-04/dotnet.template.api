using Microsoft.AspNetCore.Mvc;
using SharedSources.Models;

namespace API.Controllers;

/// <summary>
/// <see cref="BaseController"/> controller
/// </summary>
public class BaseController : ControllerBase
{
    /// <summary>
    /// Standar Ok response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    public IActionResult StandarOk<T>(T data) => Ok(new StandarResponseModel<T>().SetData(data));
}

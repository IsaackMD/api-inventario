using Microsoft.AspNetCore.Mvc;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult FromResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        protected IActionResult FromCreated<T>(Result<T> result, string actionName) where T : IEntity
        {
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return CreatedAtAction(
                actionName,
                new { id = result?.Value?.Id },
                result.Value);
        }
    }
}

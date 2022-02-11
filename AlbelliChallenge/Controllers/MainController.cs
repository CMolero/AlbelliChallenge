using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AlbelliChallenge.Controllers
{
    public class MainController : ControllerBase
    {
        protected async Task<ActionResult<T>> ExecuteGet<T>(Func<Task<T>> action)
        {
            try
            {
                return Ok(await action());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        protected async Task<ActionResult> ExecutePost(Func<Task> action)
        {
            try
            {
                await action();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
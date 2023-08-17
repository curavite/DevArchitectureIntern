
using Business.Handlers.WashingControll_FloorControlls.Commands;
using Business.Handlers.WashingControll_FloorControlls.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// WashingControll_FloorControlls If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
        [ApiController]
    public class WashingControll_FloorControllsController : BaseApiController
    {
        ///<summary>
        ///List WashingControll_FloorControlls
        ///</summary>
        ///<remarks>WashingControll_FloorControlls</remarks>
        ///<return>List WashingControll_FloorControlls</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WashingControll_FloorControll>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetWashingControll_FloorControllsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>WashingControll_FloorControlls</remarks>
        ///<return>WashingControll_FloorControlls List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WashingControll_FloorControll))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetWashingControll_FloorControllQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add WashingControll_FloorControll.
        /// </summary>
        /// <param name="createWashingControll_FloorControll"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateWashingControll_FloorControllCommand createWashingControll_FloorControll)
        {
            var result = await Mediator.Send(createWashingControll_FloorControll);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update WashingControll_FloorControll.
        /// </summary>
        /// <param name="updateWashingControll_FloorControll"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateWashingControll_FloorControllCommand updateWashingControll_FloorControll)
        {
            var result = await Mediator.Send(updateWashingControll_FloorControll);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete WashingControll_FloorControll.
        /// </summary>
        /// <param name="deleteWashingControll_FloorControll"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteWashingControll_FloorControllCommand deleteWashingControll_FloorControll)
        {
            var result = await Mediator.Send(deleteWashingControll_FloorControll);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}

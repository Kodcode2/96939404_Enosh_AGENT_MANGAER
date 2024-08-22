using AgentRestApi.Dto;
using AgentRestApi.Models;
using AgentRestApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentRestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TargetSController(ITargetService targetService) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateTarget([FromBody] TargetDto targetDto)
        {
            try
            {
                var target = await targetService.CreateTargetAsync(targetDto);
                return Created("{\"id\": 1}", $"{{\"id\": {target.Id}}}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllTargets() =>
          Ok(await targetService.GetAllTargetAsync());



        
        
        [HttpPut("{id}/pin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PinTarget(LocationDto location, int id)
        {
            try
            {
                return Ok(await targetService.PinTarget(location, id));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

      
        
        [HttpPut("" +
            "{id}/move")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> MoveTarget(DirectionDto direction, int id)
        {
            try
            {
                return Ok(await targetService.MoveTarget(direction, id));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

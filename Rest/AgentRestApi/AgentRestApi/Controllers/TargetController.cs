using AgentRestApi.Dto;
using AgentRestApi.Models;
using AgentRestApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetController(ITargetService targetService) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateTarget([FromBody] TargetDto targetDto)
        {
            try
            {
                var target = await targetService.CreateTargetAsync(targetDto);
                return Created("Create new agent", target?.Id);
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



        //[HttpPut("{id}/pin")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        ////public async Task<ActionResult> PinTarget (int id)
        ////{
        ////    return 
        ////}
    }
}

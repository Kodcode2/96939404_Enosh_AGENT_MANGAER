﻿using AgentRestApi.Dto;
using AgentRestApi.Models;
using AgentRestApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentRestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentsController(IAgentService agentService) : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAgent([FromBody] AgentDto agentDto)
        {
            try
            {
                var agent = await agentService.CreateAgentAsync(agentDto);
                return Created("{\"id\": 1}", $"{{\"id\": {agent.Id}}}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllAgents() =>
            Ok(await agentService.GetAllAgentsAsync());


       
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAgentSingle(int id)
        {
            try
            {
                var user = await agentService.GetAgentById(id);
                if (user == null) { return NotFound("not found"); }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromBody] AgentDto agentDto, int id)
        {
            try
            {
                var model = await agentService.UpdateAgentAsync(agentDto, id);
                return Ok(model);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var user = await agentService.DeleteAgentAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}/pin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PinTarget(LocationDto location, int id)
        {
            try
            {
                return Ok(await agentService.PinAgent(location, id));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        
        [HttpPut("{id}/move")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> MoveTarget(DirectionDto location, int id)
        {
            try
            {
                return Ok(await agentService.MoveAgent(location, id));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

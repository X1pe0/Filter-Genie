using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilterGenie.Entities;
using FilterGenie.Interfaces;
using FilterGenie.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FilterGenie.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgentController : ControllerBase
    {
        private readonly ILogger<AgentController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AgentController(ILogger<AgentController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet("register/{orgKey}/{hostName}")]
        public async Task<IActionResult> RegisterAgent(string orgKey, string hostName)
        {
            Agent agent = new Agent
            {
                OrgKey = orgKey,
                AgentKey = Guid.NewGuid().ToString(),
                Hostname = hostName
            };
            
            var data = await _unitOfWork.Agents.AddAsync(agent);

            if (data == 1)
                return Ok(agent.AgentKey);
            else
                return BadRequest("400");
        }

        [HttpGet("update/{agentKey}/{hash}")]
        public async Task<IActionResult> CheckHash(string agentKey, string hash)
        {
            Agent agent = await _unitOfWork.Agents.GetByKeyAsync(agentKey);    
            
            var globalHosts = await _unitOfWork.Orgs.GetHosts(agent.OrgKey);
            var personalHosts = await _unitOfWork.Agents.GetHosts(agent.AgentKey);

            string hostFile = HostService.GenerateHostFile(globalHosts, personalHosts);
            agent.PersonalHash = HostService.ComputeHash(hostFile);

            var test = await _unitOfWork.Agents.UpdateAsync(agent);

            if (agent.PersonalHash == hash)
                return Ok("200");

            return Ok( new { Hash = agent.PersonalHash, HostFile = hostFile});
        }
    }
}

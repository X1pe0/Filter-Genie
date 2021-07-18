using System.Collections.Generic;
using System.Threading.Tasks;
using FilterGenie.Entities;

namespace FilterGenie.Interfaces
{
    public interface IAgentRepository : IGenericRepository<Agent>
    {
        Task<IReadOnlyList<Agent>> GetByOrgKeyAsync(string orgKey);
        Task<int> GetCountPerOrgAsync(string orgKey);
        Task<Agent> GetByKeyAsync(string agentKey);
        Task<IReadOnlyList<string>> GetHosts(string agentKey);
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FilterGenie.Entities;
using FilterGenie.Interfaces;
using FilterGenie.Services;
using Microsoft.Extensions.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace FilterGenie.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private readonly IConfiguration configuration;
        
        public AgentRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Adds Agent into the database
        /// </summary>
        /// <param name="entity">Agent</param>
        /// <returns>Result</returns>
        public async Task<int> AddAsync(Agent entity)
        {
            var org = await getByOrgKeyAsync(entity.OrgKey);
            int count = await GetCountPerOrgAsync(entity.OrgKey);
            
            if (org == null || org.MaxAgents <= count)
                return 0;

            var sql = "INSERT INTO agents (org_key, agent_key, host_name) VALUES (@OrgKey, @AgentKey, @Hostname)";

            using (var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    
        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM agents WHERE id=@Id";

            using (var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Agent>> GetAllAsync()
        {
            var sql = "SELECT * FROM agents";

            using (var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Agent>(sql);
                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<Agent>> GetByOrgKeyAsync(string orgKey)
        {
            var sql = "SELECT * FROM agents WHERE org_key=@OrgKey";

            using (var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Agent>(sql, new {OrgKey = orgKey});
                return result.ToList();
            }
        }

        public async Task<Agent> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM agents WHERE id=@Id";

            using (var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Agent>(sql, new {Id = id});
                return result;
            }
        }

        public async Task<Agent> GetByKeyAsync(string agentKey)
        {
            var sql = "SELECT * FROM agents WHERE agent_key=@AgentKey";

            using (var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Agent>(sql, new {AgentKey = agentKey});
                return result;
            }
        }

        public async Task<int> UpdateAsync(Agent entity)
        {
            var sql = "UPDATE agents SET org_key = @OrgKey, agent_key = @AgentKey, host_name = @Hostname, personal_hash = @PersonalHash WHERE id = @Id";

            using (var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> GetCountPerOrgAsync(string orgKey)
        {
            var sql = "SELECT COUNT(*) FROM agents WHERE org_key=@OrgKey";

            using (var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<int>(sql, new {OrgKey = orgKey});
                return result;
            }
        }

        private async Task<Organization> getByOrgKeyAsync(string orgKey)
        {
            var sql = "SELECT * FROM orgs WHERE org_key=@OrgKey";

            using (var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Organization>(sql, new {OrgKey = orgKey});
                return result;
            }
        }

        public async Task<IReadOnlyList<string>> GetHosts(string agentKey)
        {
            var sql = "SELECT host FROM agent_hosts WHERE agent_key = @AgentKey";

            using (var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<string>(sql, new {AgentKey = agentKey});
                return result.ToList();
            }
        }
    }
}
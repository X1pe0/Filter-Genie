using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
    public class OrgRepository : IOrgRepository
    {
        private readonly IConfiguration configuration;
        
        public OrgRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(Organization entity)
        {
            var sql = "INSERT INTO orgs (org_key, max_agents) VALUES (@OrgKey, @MaxAgents)";

            using (var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM orgs WHERE id=@Id";

            using (var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Organization>> GetAllAsync()
        {
            var sql = "SELECT * FROM orgs";

            using (var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Organization>(sql);
                return result.ToList();
            }
        }

        public async Task<Organization> GetByOrgKeyAsync(string orgKey)
        {
            var sql = "SELECT * FROM orgs WHERE org_key=@OrgKey";

            using (var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Organization>(sql, new {OrgKey = orgKey});
                return result;
            }
        }

        public async Task<Organization> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM orgs WHERE id=@Id";

            using (var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Organization>(sql, new {Id = id});
                return result;
            }
        }

        public async Task<int> UpdateAsync(Organization entity)
        {
            var sql = "UPDATE orgs SET org_key = @OrgKey, max_agents = @MaxAgents WHERE id = @Id";

            using (var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<IReadOnlyList<string>> GetHosts(string orgKey)
        {
            var sql = "SELECT host FROM global_hosts WHERE org_key = @OrgKey";

            using (var connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<string>(sql, new {OrgKey = orgKey});
                return result.ToList();
            }
        }
    }
}
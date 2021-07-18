using System.Collections.Generic;
using System.Threading.Tasks;
using FilterGenie.Entities;

namespace FilterGenie.Interfaces
{
    public interface IOrgRepository : IGenericRepository<Organization>
    {
        Task<IReadOnlyList<string>> GetHosts(string orgKey);
    }
}
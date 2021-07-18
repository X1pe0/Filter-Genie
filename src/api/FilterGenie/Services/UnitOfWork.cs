using FilterGenie.Interfaces;

namespace FilterGenie.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAgentRepository Agents { get; }
        public IOrgRepository Orgs { get; }

        public UnitOfWork(IAgentRepository agentRepository, IOrgRepository orgs)
        {
            Agents = agentRepository;
            Orgs = orgs;
        }
    }
}
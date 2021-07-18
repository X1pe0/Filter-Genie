namespace FilterGenie.Interfaces
{
    public interface IUnitOfWork
    {
        IAgentRepository Agents { get; }
        IOrgRepository Orgs { get; }
    }
}
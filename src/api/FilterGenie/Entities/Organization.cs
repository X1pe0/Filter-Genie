using System.ComponentModel;

namespace FilterGenie.Entities
{
    public class Organization
    {
        public int Id { get; set; }

        public string OrgKey { get; set; }

        public int MaxAgents { get; set; }
    }
}
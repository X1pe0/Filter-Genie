using System.ComponentModel;

namespace FilterGenie.Entities
{
    public class Agent
    {
        public int Id { get; set; }

        public string OrgKey { get; set; }

        public string AgentKey { get; set; }

        public string Hostname { get; set; }
        public string PersonalHash { get; set; }
    }
}
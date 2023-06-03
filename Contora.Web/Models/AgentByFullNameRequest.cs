using Contora.Core;

namespace Contora.Web.Models
{
    public class AgentByFullNameRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MidleName { get; set; }
    }
}

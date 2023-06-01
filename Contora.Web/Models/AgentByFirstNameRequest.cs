using Contora.Core;

namespace Contora.Web.Models
{
    public class AgentByFirstNameRequest
    {
        private string firstName;

        public string FirstName
        {
            get => firstName;
            set => firstName = string.IsNullOrEmpty(value) ? throw new ArgumentException("Имя не должно быть пустым.") :
                value.Length > 50 ? throw new ArgumentException("Имя должно быть менее 50 символов.") : value;
        }

        public async Task<List<Agent>> GetAgents(CancellationToken token)
        {
            return await Agent.Read_ByFirstName_async(this.FirstName, token);
        }
    }
}

using Contora.Core;

namespace Contora.Web.Models
{
    public class AgentByLastNameRequest
    {
        private string lastName;

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = string.IsNullOrEmpty(value) ? throw new ArgumentException("Фамилия не должна быть пустой.") :
                    value.Length > 50 ? throw new ArgumentException("Фамилия должна быть не более 50 символов.") : value;
            }
        }

        public async Task<List<Agent>> GetAgent(CancellationToken token)
        {
            return await Agent.Read_ByLastName_async(this.LastName, token);
        }
    }

}

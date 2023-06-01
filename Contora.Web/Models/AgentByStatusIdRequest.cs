using Contora.Core;

namespace Contora.Web.Models
{
    public class AgentByStatusIdRequest
    {
        private int statusId;

        public int StatusId
        {
            get => statusId;
            set
            {
                statusId = value < 1 ? throw new ArgumentException("ID статуса не может быть 0 или отрицательным.") : value;
            }
        }

        public async Task<List<Agent>> GetAgent(CancellationToken token)
        {
            return await Agent.Read_ByStatusId_async(this.StatusId, token);
        }
    }

}

using Contora.Core;

namespace Contora.Web.Models
{
    public class AgentByPositionIdRequest
    {
        private int positionId;

        public int PositionId
        {
            get => positionId;
            set
            {
                positionId = value < 1 ? throw new ArgumentException("ID должности не может быть 0 или отрицательным.") : value;
            }
        }

        public async Task<List<Agent>> GetAgent(CancellationToken token)
        {
            return await Agent.Read_ByPositionId_async(this.PositionId, token);
        }
    }

}

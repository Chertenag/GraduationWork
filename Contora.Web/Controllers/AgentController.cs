using Microsoft.AspNetCore.Mvc;
using Contora.Core;

namespace Contora.Web.Controllers
{
    [Route("Contora/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Agent>> Get(CancellationToken token)
        {
            return Agent.Read_All_async(token).Result;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Agent>> Get(int id, CancellationToken token)
        {
            var rez = Agent.Read_ById_async(id, token).Result;
            if (rez.Count == 0)
            {
                return base.NotFound("Target with this ID does not exist.");
            }
            return rez;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Agent>> Get([FromQuery] Models.AgentByFirstNameRequest value, CancellationToken token)
        {
            var rez = value.GetAgent(token).Result;
            if (rez.Count == 0)
            {
                return base.NotFound("Target with this ID does not exist.");
            }
            return rez;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Agent>> Get([FromQuery] Models.AgentByLastNameRequest value, CancellationToken token)
        {
            var rez = value.GetAgent(token).Result;
            if (rez.Count == 0)
            {
                return base.NotFound("Target with this ID does not exist.");
            }
            return rez;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Agent>> Get([FromQuery] Models.AgentByDepartmentIdRequest value, CancellationToken token)
        {
            var rez = value.GetAgent(token).Result;
            if (rez.Count == 0)
            {
                return base.NotFound("Target with this ID does not exist.");
            }
            return rez;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Agent value, CancellationToken token)
        {
            if (Agent.Read_ById_async(value.Id, token).Result.Count > 0)
            {
                return base.Conflict("Target with this ID already exists.");
            }
            Agent.Create_async(value, token).Wait();
            return base.Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Agent value, CancellationToken token)
        {
            if (Agent.Read_ById_async(value.Id, token).Result.Count == 0)
            {
                return base.NotFound("Target with this ID does not exist.");
            }
            value.Id = id;
            Agent.Update_async(value, token).Wait();
            return base.Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id, CancellationToken token)
        {
            if (Agent.Read_ById_async(id, token).Result.Count == 0)
            {
                return base.NotFound("Target with this ID does not exist.");
            }
            Agent.Delete_ById_async(id, CancellationToken.None).Wait();
            return base.Ok();
        }
    }
}

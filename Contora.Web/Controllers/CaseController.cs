using Microsoft.AspNetCore.Mvc;
using Contora.Core;
using Contora.Web.Models;

namespace Contora.Web.Controllers
{
    [Route("Contora/[controller]")]
    [ApiController]
    public class CaseController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Case>> Get(CancellationToken token)
        {
            return Case.Read_All_async(token).Result;
        }

        [HttpGet("{id:min(1)}")]
        public ActionResult<IEnumerable<Case>> Get(int id, CancellationToken token)
        {
            var rez = Case.Read_ById_async(id, token).Result;
            if (rez.Count == 0)
            {
                return base.NotFound("Case with this ID does not exist.");
            }
            return rez;

        }

        [HttpGet("ByDepId")]
        public ActionResult<IEnumerable<Case>> Get([FromQuery] CaseByBothAgentId value, CancellationToken token)
        {
            var rez = value.GetCases(token).Result;
            if (rez.Count == 0)
            {
                return base.NotFound("Case with this Department ID does not exist.");
            }
            return rez;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Case value, CancellationToken token)
        {
            if (Case.Read_ById_async(value.Id, token).Result.Count > 0)
            {
                return base.Conflict("Case with this ID already exists.");
            }
            Case.Create_async(value, token).Wait();
            return base.Ok();
        }

        [HttpPost("{id}/CloseCase")]
        public ActionResult Post(int id, DateTime closeDate, CancellationToken token)
        {
            Case.Update_CloseCase_async(id, closeDate, token).Wait(); 
            return base.Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Case value, CancellationToken token)
        {
            if (Case.Read_ById_async(value.Id, token).Result.Count == 0)
            {
                return base.NotFound("Case with this ID does not exist.");
            }
            value.Id = id;
            Case.Update_async(value, token).Wait();
            return base.Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id, CancellationToken token)
        {
            if (Case.Read_ById_async(id, token).Result.Count == 0)
            {
                return base.NotFound("Case with this ID does not exist.");
            }
            Case.Delete_ById_async(id, CancellationToken.None).Wait();
            return base.Ok();
        }
    }
}

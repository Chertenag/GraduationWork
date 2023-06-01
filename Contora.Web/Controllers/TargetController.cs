using Microsoft.AspNetCore.Mvc;
using Contora.Core;

namespace Contora.Web.Controllers
{
    [Route("Contora/[controller]")]
    [ApiController]
    public class TargetController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Target>> Get(CancellationToken token)
        {
            return Target.Read_All_async(token).Result;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Target>> Get(int id, CancellationToken token)
        {
            var rez = Target.Read_ById_async(id, token).Result;
            if (rez.Count == 0)
            {
                return base.NotFound("Target with this ID does not exist.");
            }
            return rez;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Target value, CancellationToken token)
        {
            if (Target.Read_ById_async(value.Id, token).Result.Count > 0) 
            {
                return base.Conflict("Target with this ID already exists.");
            }
            Target.Create_async(value, token).Wait();
            return base.Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Target value, CancellationToken token)
        {
            if (Target.Read_ById_async(value.Id, token).Result.Count == 0)
            {
                return base.NotFound("Target with this ID does not exist.");
            }
            value.Id = id;
            Target.Update_async(value, token).Wait();
            return base.Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id, CancellationToken token)
        {
            if (Target.Read_ById_async(id, token).Result.Count == 0)
            {
                return base.NotFound("Target with this ID does not exist.");
            }
            Target.Delete_ById_async(id, CancellationToken.None).Wait();
            return base.Ok();
        }
    }
}

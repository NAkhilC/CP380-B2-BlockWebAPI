using CP380_B2_BlockWebAPI.Models;
using CP380_B1_BlockList.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CP380_B2_BlockWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PendingPayloadsController : ControllerBase
    {
        public object DependencyManager { get; private set; }

        [HttpGet("/latest")]
        public string Getblocks()
        {
            BlockList blockList = new BlockList();
            var blocks = blockList.Chain;
            return JsonConvert.SerializeObject(blocks);
        }

        [HttpPost("/add")]
        public void AddNext(int index)
        {
            BlockList blist = new BlockList();
            var data = HttpContext.Request.ReadFromJsonAsync<Payload>();
#pragma warning disable CS8073 // The result of the expression is always the same since a value of this type is never equal to 'null'
            if (data != null && data.Result != null)
#pragma warning restore CS8073 // The result of the expression is always the same since a value of this type is never equal to 'null'
                blist.AddNext(data.Result, index);
        }
    }
}

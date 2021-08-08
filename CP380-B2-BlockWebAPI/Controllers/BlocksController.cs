using CP380_B1_BlockList.Models;
using CP380_B2_BlockWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CP380_B2_BlockWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BlocksController : ControllerBase
    {

        [HttpGet("/blocks")]
        public string GetAllBlocks()
        {
            BlockList blist = new BlockList();
            List<BlockSummary> bfinal = new List<BlockSummary>();
            for(int i = 0; i < blist.Chain.Count; i++)
            {
                BlockSummary summary = new BlockSummary();
                summary.Timedate = blist.Chain[i].TimeStamp;
                summary.PreviousHash = blist.Chain[i].PreviousHash;
                summary.Hash = blist.Chain[i].Hash;
                bfinal.Add(summary);
            }
            return JsonConvert.SerializeObject(bfinal);
        }

        [HttpGet("/blocks/{hash?}")]
        public string GetAllBlocks(int hash)
        {
            Block block = null;
            BlockList blist = new BlockList();
            if (hash < blist.Chain.Count)
                block = blist.Chain[hash];
            if (block == null)
                return JsonConvert.SerializeObject(NotFound());
            return JsonConvert.SerializeObject(block);
        }

        [HttpGet("/blocks/{hash?}/payloads")]
        public string GetAllPayloads(int Hash)
        {
            Block block = null;
            BlockList blockList = new BlockList();
            if (Hash < blockList.Chain.Count)
                block = blockList.Chain[Hash];
            return JsonConvert.SerializeObject(block.Data);
        }

    }
}

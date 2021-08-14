
using CP380_B1_BlockList.Models;
using CP380_B2_BlockWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CP380_B2_BlockWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BlocksController : ControllerBase
    {
        // TODO

        private BlockList blist;

        public BlocksController(BlockList blockList)
        {
            blist = blockList;
        }


        [HttpGet("/blocks")]
        public IActionResult Get()
        {
            return Ok(blist.Chain.Select(block => new BlockSummary()
            {
                Hash = block.Hash,
                PreviousHash = block.PreviousHash,
                TimeStamp = block.TimeStamp
            }));
        }

        
         [HttpGet("/blocks/{hash?}")]
        public IActionResult GetBlock(string hash)
        {
            var block = blist.Chain
                .Where(block => block.Hash.Equals(hash));

            if (block != null && block.Count() > 0)
            {
                return Ok(block
                    .Select(block => new BlockSummary()
                    {
                        Hash = block.Hash,
                        PreviousHash = block.PreviousHash,
                        TimeStamp = block.TimeStamp
                    }
                    )
                    .First());
            }

            return NotFound();
        }

             [HttpGet("/blocks/{hash?}/payloads")]
        public IActionResult GetBlockPayload(string hash)
        {
            var block = blist.Chain
                        .Where(block => block.Hash.Equals(hash));

            if (block != null && block.Count() > 0)
            {
                return Ok(block
                    .Select(block => block.Data
                    )
                    .First());
            }

            return NotFound();
        }
    }
}

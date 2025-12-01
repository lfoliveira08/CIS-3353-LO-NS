// Controllers/BlockchainController.cs
using Microsoft.AspNetCore.Mvc;
using SimpleBlockchainAPI.Models;
using SimpleBlockchainAPI.Services;

namespace SimpleBlockchainAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlockchainController : ControllerBase
    {
        private readonly BlockchainService _blockchainService;

        public BlockchainController(BlockchainService blockchainService)
        {
            _blockchainService = blockchainService;
        }

        // GET: /blockchain (Returns the entire chain)
        [HttpGet]
        public IEnumerable<Block> Get()
        {
            return _blockchainService.Chain;
        }

        // --- NEW ENDPOINT: GET: /blockchain/{hash} (Returns a block by its hash) ---
        [HttpGet("{hash}")]
        public ActionResult<Block> GetByHash(string hash)
        {
            // 1. Validate the input hash
            if (string.IsNullOrWhiteSpace(hash))
            {
                return BadRequest("The block hash cannot be empty.");
            }

            // 2. Search for the block in the service
            var block = _blockchainService.GetBlockByHash(hash);

            // 3. Handle the result
            if (block == null)
            {
                // Return 404 Not Found if the block isn't in the chain
                return NotFound($"Block with hash '{hash}' not found in the blockchain.");
            }

            // Return 200 OK with the block data
            return Ok(block);
        }

        // POST: /blockchain (Adds a new message/block)
        [HttpPost]
        public IActionResult Post([FromBody] string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return BadRequest("Message cannot be empty.");
            }
            _blockchainService.AddBlock(message);
            
            // Return a success response
            return Ok(new { 
                Status = "Success", 
                Message = $"Block added successfully with data: '{message}'",
                NewBlock = _blockchainService.GetLatestBlock()
            });
        }
    }
}
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

        // GET: /blockchain/{hash} (Returns a block by its hash) ---
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

        // GET: /blockchain/isvalid (Returns the validation status) ---
        [HttpGet("isvalid")]
        public IActionResult IsValid()
        {
            bool isValid = _blockchainService.IsChainValid();

            if (isValid)
            {
                return Ok(new {
                    Status = "Valid",
                    Message = "✅ The blockchain is intact and valid.",
                    IsValid = true
                });
            }
            else
            {
                return Ok(new { 
                    Status = "Invalid",
                    Message = "❌ The blockchain has been compromised or corrupted!",
                    IsValid = false
                });
            }
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

        // --- NEW DEBUG ENDPOINT: POST: /blockchain/tamper ---
        [HttpPost("tamper")]
        public IActionResult Tamper()
        {
            _blockchainService.TamperChain();
            
            return Ok(new { 
                Status = "Corrupted", 
                Message = "Block #1 data has been modified. The chain is now invalid."
            });
        }
    }
}
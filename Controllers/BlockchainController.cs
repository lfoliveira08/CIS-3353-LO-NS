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
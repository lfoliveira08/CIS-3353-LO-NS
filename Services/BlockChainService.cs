// Services/BlockChainService.cs
using SimpleBlockchainAPI.Models;

namespace SimpleBlockchainAPI.Services
{
    public class BlockchainService
    {
        public List<Block> Chain { get; private set; } = new List<Block>();
        
        public BlockchainService()
        {
            // Initialize the chain with the first block (Genesis Block)
            CreateGenesisBlock();
        }

        private void CreateGenesisBlock()
        {
            Block genesisBlock = new Block("Genesis Block", "0");
            Chain.Add(genesisBlock);
        }

        public Block GetLatestBlock()
        {
            return Chain.Last();
        }

        public void AddBlock(string data)
        {
            // 1. Get the hash of the last block
            string previousHash = GetLatestBlock().Hash;
            
            // 2. Create the new block
            Block newBlock = new Block(data, previousHash);
            
            // 3. Add it to the chain
            Chain.Add(newBlock);
        }
    }
}
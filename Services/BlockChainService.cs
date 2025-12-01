// Services/BlockChainService.cs
using SimpleBlockchainAPI.Models;

namespace SimpleBlockchainAPI.Services
{
    public class BlockchainService
    {
        public List<Block> Chain { get; private set; } = new List<Block>();

        private const int Difficulty = 3;
        
        public BlockchainService()
        {
            // Initialize the chain with the first block (Genesis Block)
            CreateGenesisBlock();
        }

        private void CreateGenesisBlock()
        {
            Block genesisBlock = new Block("Genesis Block", "0" , Difficulty);
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
            Block newBlock = new Block(data, previousHash, Difficulty);

            // 3. Add it to the chain
            Chain.Add(newBlock);
        }
        public Block? GetBlockByHash(string hash)
        {
            return Chain.FirstOrDefault(b => b.Hash.Equals(hash, StringComparison.OrdinalIgnoreCase));
        }
    }
}
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

        // --- NEW METHOD: Blockchain Validation ---
        public bool IsChainValid()
        {
            // The chain is valid by definition if it only contains the Genesis Block
            if (Chain.Count <= 1)
            {
                return true;
            }

            try
            {
                // Iterate over the chain, starting from the second block (index 1)
                for (int i = 1; i < Chain.Count; i++)
                {
                    Block currentBlock = Chain[i];
                    Block previousBlock = Chain[i - 1];

                    // 1. Check if the Current Block's Hash is correct (Proof of Work)
                    string recalculatedHash = currentBlock.CalculateHash();
                    if (currentBlock.Hash != recalculatedHash)
                    {
                        // You can add a Debug log here if you have a logging framework
                        return false; 
                    }

                    // 2. Check Chain Integrity
                    if (currentBlock.PreviousHash != previousBlock.Hash)
                    {
                        // You can add a Debug log here
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // CRITICAL: Log this error in your C# console if possible. 
                // A runtime error here can cause the endpoint to return a 500 status 
                // with an empty body, leading to the JS "undefined" error.
                Console.WriteLine($"[CRITICAL VALIDATION ERROR]: {ex.Message}");
                return false; // Assume invalid on internal error
            }

            // If the loop completes without finding any discrepancies, the chain is valid
            return true;
        }
    }
}
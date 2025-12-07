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

        // Blockchain Validation
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
                for (int i = 1; i <  Chain.Count; i++)
                {
                    Block currentBlock = Chain[i];
                    Block previousBlock = Chain[i - 1];

                    // 1. Check if the Current Block's Hash is correct (Proof of Work)
                    string recalculatedHash = currentBlock.CalculateHash();
                    if (currentBlock.Hash != recalculatedHash)
                    {
                        return false; 
                    }

                    // 2. Check Chain Integrity
                    if (currentBlock.PreviousHash != previousBlock.Hash)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CRITICAL VALIDATION ERROR]: {ex.Message}");
                return false;
            }

            // If the loop completes without finding any discrepancies, the chain is valid
            return true;
        }


        // --- NEW METHOD: Blockchain tampering ---
        public void TamperChain()
        {
            // We tamper with Block #1 (index 1), assuming a Genesis Block (index 0) exists.
            if (Chain.Count > 1)
            {
                Block blockToCorrupt = Chain[1];

                // 1. Corrupt the data
                Console.WriteLine($"[TAMPERING] Changing Data of Block #1 from '{blockToCorrupt.Data}'...");
                blockToCorrupt.Data = "DATA HAS BEEN HACKED!";
                
                // Because Data changed, the original blockToCorrupt.Hash is now going to be different than the recalculate hash.
                Console.WriteLine($"[TAMPERING] Block #1 successfully corrupted.");
            }
        }
    }
}
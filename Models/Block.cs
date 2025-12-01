//Models/Block.cs
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace SimpleBlockchainAPI.Models
{
    public class Block
    {

        // 1. Data
        public string Data { get; set; } = string.Empty;

        // 2. Metadata
        public DateTime Timestamp { get; set; } = DateTime.Now;

        // 3. Chain Integrity
        public string PreviousHash { get; set; } = string.Empty;

        // 4. Current Block's Hash
        public string Hash { get; private set; } = string.Empty;

        // 5. Salt/Nonce
        public int Nonce { get; private set; } = 0;
        
        private int Difficulty;

        public Block(string data, string previousHash, int difficulty)
        {
            Data = data;
            PreviousHash = previousHash;
            Difficulty = difficulty;
            MineBlock();
        }

        //Core Blockchain Logic: HASHING
        public string CalculateHash()
        {
            // Concatenate all data fields for hashing
            string rawData = $"{Timestamp.ToString("yyyyMMddHHmmssffff")}-{PreviousHash}-{Data}-{Nonce}";

            // Generate SHA-256 hash
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        
        //Salting logic (new addition)
        public void MineBlock()
        {
            // Create a target string (e.g., "000" for Difficulty=3)
            string target = new string('0', Difficulty);

            // Loop until the calculated hash meets the difficulty requirement
            // The Nonce automatically acts as the "salt" here.
            while (Hash == string.Empty || !Hash.StartsWith(target))
            {
                // 1. Increment the Nonce (Salt)
                Nonce++;

                // 2. Recalculate the Hash with the new Nonce
                Hash = CalculateHash();
            }
        }
        }
}

⛓️ SecureChain API

A lightweight Blockchain API developed for CIS-3353 demonstrating cryptographic integrity through Hashing and Salting.

📄 Project Overview

This repository contains a simple Blockchain API built using C# and ASP.NET Core. The primary goal of this project is to demonstrate the fundamental concepts of Data Integrity and Immutable Ledger Technology within a cybersecurity context.

In a typical centralized database, an administrator can alter records without detection. In this blockchain implementation, every record (block) is cryptographically linked to the previous one. This project differentiates itself from standard blockchain tutorials by placing a heavy emphasis on Cryptographic Salting to defend against specific attack vectors like rainbow table attacks.

Core Objectives

Integrity: Ensuring that once data is written to the chain, it cannot be altered without invalidating the entire history.

Non-Repudiation: Using unique signatures to verify the origin and content of data.

Defense in Depth: Implementing multiple layers of cryptographic security (SHA-256 + Random Salts).

🛡️ Key Security Features

1. SHA-256 Hashing

We utilize the SHA-256 (Secure Hash Algorithm 256-bit) via the System.Security.Cryptography namespace. This is a one-way function that transforms input data of any size into a fixed-size string of characters.

Avalanche Effect: A change in just one bit of the input data (e.g., changing a message from "Hello" to "hello") results in a completely different hash. This makes tampering immediately evident.

Collision Resistance: It is statistically impossible to find two different block inputs that generate the exact same hash signature.

2. Cryptographic Salting

To enhance security beyond standard hashing, we implement Salting.

Mechanism: A unique, random "salt" string is generated for every single block using a cryptographically secure random number generator.

Implementation: This salt is combined with the block's data before the hash is calculated (Hash = SHA256(Data + Salt)).

Defense: This specifically defends against Pre-computation Attacks (like Rainbow Tables). Without salting, an attacker could pre-calculate hashes for common inputs. With unique salts, every block requires a unique computation, making brute-force attacks computationally prohibitive.

3. Chain Dependencies

Each block contains the PreviousHash of the block preceding it. This creates a dependency chain:

Block A is hashed.

Block B includes Block A's hash in its own header.

If an attacker modifies Block A, Block A's hash changes.

Block B's reference to Block A is now incorrect, breaking the chain.

🛠️ Tech Stack

Language: C# (C-Sharp)

Framework: ASP.NET Core Web API

Runtime: .NET 10.0+

Cryptography: System.Security.Cryptography

Tools: Visual Studio 2022 / VS Code 

🚀 Installation & Setup

Prerequisites

.NET SDK (Version 10.0 or later)

Steps

Clone the repository

git clone [https://github.com/lfoliveira08/CIS-3353-LO-NS.git](https://github.com/lfoliveira08/CIS-3353-LO-NS.git)
cd CIS-3353-LO-NS


Restore Dependencies

dotnet restore


Run the API

dotnet run


The API is configured to run on http://localhost:5057. Ensure this port is free.

📡 API Endpoints

1. Get Full Chain

Retrieves the entire current state of the blockchain ledger.

URL: /blockchain

Method: GET

Response: A JSON array containing all blocks in chronological order.

2. Mine/Create Block

Adds new data to the blockchain. This triggers the hashing algorithm, generates a new random salt, and links the block to the previous entry.

URL: /blockchain

Method: POST

Request Body: A JSON string containing the message data.

"Secret message for the blockchain"


Response: Details of the newly created block, including its Salt and calculated Hash.

3. Find Block by Hash

Allows users to query the ledger for a specific block using its unique hash signature.

URL: /blockchain/{hash}

Method: GET

Example: /blockchain/a1b2c3d4...

Response:

200 OK: Returns the block object.

404 Not Found: If no block matches the provided hash.

🧩 Block Structure

Each block in our chain follows this JSON structure (serialized to camelCase). Note the inclusion of the salt field, which is critical for our security implementation.

{
  "timestamp": "2023-10-27T10:00:00Z",
  "data": "Secret message content...",
  "nounce": "8f9e2a...",   <-- Randomly generated per block
  "previousHash": "000abc...",
  "hash": "000123..."     <-- SHA-256(Index + Timestamp + Data + Salt + PreviousHash)
}


🔮 Future Improvements

While the current iteration focuses on core hashing and structure, future versions of this project may include:

Chain Validation Endpoint: A dedicated endpoint to iterate through the chain and programmatically verify integrity.

Proof of Work (PoW): Implementing a difficulty target (e.g., requiring hashes to start with 0000) to simulate mining difficulty.

P2P Networking: Broadcasting blocks to other nodes.

👥 Contributors

Luis de Oliveira and Nicholas Schreen - Project Lead

Luis de Oliveira and Nicholas Schreen - Developer

📜 License

This project is created for educational purposes for the CIS-3353 Cybersecurity course.

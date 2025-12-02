# ⛓️ SecureChain API

A lightweight Blockchain API developed for **CIS-3353** demonstrating cryptographic integrity through Hashing and Salting.

## 📄 Project Overview

This repository contains a simple Blockchain API built using **C#** and **ASP.NET Core**. The primary goal of this project is to demonstrate the fundamental concepts of Data Integrity and Immutable Ledger Technology within a cybersecurity context.

In a typical centralized database, an administrator can alter records without detection. In this blockchain implementation, every record (block) is cryptographically linked to the previous one. This project differentiates itself from standard blockchain tutorials by placing a heavy emphasis on **Cryptographic Salting** to defend against specific attack vectors like rainbow table attacks.

### Core Objectives

* **Integrity:** Ensuring that once data is written to the chain, it cannot be altered without invalidating the entire history.
* **Non-Repudiation:** Using unique signatures to verify the origin and content of data.
* **Defense in Depth:** Implementing multiple layers of cryptographic security (SHA-256 + Random Salts).

---

## 📈 Project Status

The foundational elements and basic visualization are complete. The project is currently focused on enhancing API usability and preparing the core model for Proof-of-Work (PoW) implementation.

| Milestone | Status | Description |
| :--- | :--- | :--- |
| **Milestone 1: Basic End-to-End Chain** | **100% Complete** | Core API, block model, and basic creation/linking logic are functional. |
| **Milestone 2: UI Visualization & Data Compatibility** | **100% Complete** | Initial frontend visualization (`index.html`) is set up, and backend serialization is finalized. |
| **Milestone 3: Mining Foundations & Block Lookup** | **75% Complete** | Core block model upgraded with `nonce`. Focus now is on API block lookup and frontend adaptation. |

---

## 🎯 Current Development Focus (Milestone 3)

The immediate development efforts are concentrated on completing the remaining 25% of Milestone 3, which focuses on delivering the Block Lookup feature and updating the visualization.

### Revised User Story 1: Block Lookup & UI Integration

**As an API User and Front-End Observer**, I want to **search for a specific block using its unique hash** via the API, **and see the result displayed on the front end**, **so that** I can quickly retrieve, verify, and visualize the block's complete details using the web interface.

| Related Issue (Actual Title) | Status | Priority |
| :--- | :--- | :--- |
| **API: Implement 'GetBlockByHash' Service and Endpoint** | Open | High |
| **Index.html Adaptation** | Open | High |

---

## 🛡️ Key Security Features

1.  **SHA-256 Hashing:** We utilize the SHA-256 (Secure Hash Algorithm 256-bit) via the `System.Security.Cryptography` namespace. This ensures the **Avalanche Effect** (a minor change invalidates the hash) and provides **Collision Resistance**.
2.  **Cryptographic Salting:** A unique, random "salt" string is generated for every single block. This salt is combined with the block's data before the hash is calculated (`Hash = SHA256(Data + Salt)`). This defends against **Pre-computation Attacks** (like Rainbow Tables) by making brute-force attempts computationally prohibitive.
3.  **Chain Dependencies:** Each block contains the `PreviousHash` of the block preceding it, creating a verifiable dependency chain that immediately breaks if any past block is modified.

---

## 👥 User Stories (Feature Focus)

These stories summarize the functionality that has been delivered and is currently under construction.

### User Story 2: Nonce Integration for Future Mining

**As a Core Developer**, I want the fundamental `Block` model to include a **`nonce` field**, **so that** the framework is ready to implement a Proof-of-Work (PoW) consensus algorithm in the next phase.

| Related Issue (Actual Title) | Status | Milestone |
| :--- | :--- | :--- |
| Feature: Add 'Nonce' Property to Block Model | Closed | M3: Mining Foundations... |
| Feature: Define 'Block Data Model | Closed | M1: Basic End-to-End Chain |
| Added Salting Logic | Closed | M3: Mining Foundations... |

### User Story 3: Visualizing the Complete Chain

**As a User**, I want the web interface to **display all blocks in a clean, scrollable list**, **so that** I can easily view the entire history of the chain from the genesis block onward.

| Related Issue (Actual Title) | Status | Milestone |
| :--- | :--- | :--- |
| Setup of Frontend Index.html | Closed | M2: UI Visualization... |
| Fixing Index.html file | Closed | M2: UI Visualization... |
| Index.html Adaptation | Open | M3: Mining Foundations... |

### User Story 4: Reliable Data Exchange

**As a Frontend Logic Developer**, I want the backend to **always serialize block data consistently** (e.g., to JSON), **so that** I can rely on a stable data structure when building the UI visualization components.

| Related Issue (Actual Title) | Status | Milestone |
| :--- | :--- | :--- |
| Bugfix: Correct JSON Serialization in Block.cs | Closed | M2: UI Visualization... |
| Implement Core Blockchain Services Logic (Chain, Block Creation, & Validation) | Closed | M1: Basic End-to-End Chain |
| Index.html Adaptation | Open | M3: Mining Foundations... |

### User Story 5: Functional End-to-End API

**As an API Consumer**, I want a functional API to **interact with the chain and retrieve block data**, **so that** I can test the core creation, linking, and storage mechanisms of the blockchain.

| Related Issue (Actual Title) | Status | Milestone |
| :--- | :--- | :--- |
| Implement Core Blockchain Services Logic (Chain, Block Creation, & Validation) | Closed | M1: Basic End-to-End Chain |
| API: Create Blockchain Controller and Stub Endpoints | Closed | M1: Basic End-to-End Chain |
| Configure and Initialize Web Server Startup (Program.cs) | Closed | M1: Basic End-to-End Chain |

---

## 🛠️ Tech Stack

* **Language:** C# (C-Sharp)
* **Framework:** ASP.NET Core Web API
* **Runtime:** .NET 10.0+
* **Cryptography:** `System.Security.Cryptography`
* **Tools:** Visual Studio 2022 / VS Code

## 🚀 Installation & Setup

### Prerequisites

* .NET SDK (Version 10.0 or later)

### Steps

1.  **Clone the repository**
    ```bash
    git clone [https://github.com/lfoliveira08/CIS-3353-LO-NS.git](https://github.com/lfoliveira08/CIS-3353-LO-NS.git)
    cd CIS-3353-LO-NS
    ```
2.  **Restore Dependencies**
    ```bash
    dotnet restore
    ```
3.  **Run the API**
    ```bash
    dotnet run
    ```
The API is configured to run on `http://localhost:5057`.

---

## 📡 API Endpoints

1.  **Get Full Chain**
    * **URL:** `/blockchain`
    * **Method:** `GET`
    * **Response:** A JSON array containing all blocks in chronological order.

2.  **Mine/Create Block**
    * **URL:** `/blockchain`
    * **Method:** `POST`
    * **Request Body:** A JSON string containing the message data (e.g., `"Secret message for the blockchain"`).
    * **Response:** Details of the newly created block, including its Salt and calculated Hash.

3.  **Find Block by Hash** *(Currently under development in Milestone 3)*
    * **URL:** `/blockchain/{hash}`
    * **Method:** `GET`
    * **Example:** `/blockchain/a1b2c3d4...`
    * **Response:** `200 OK` (Returns the block object) or `404 Not Found`.

---

## 🧩 Block Structure

Each block in our chain follows this JSON structure. Note the inclusion of the `salt` field, which is critical for our security implementation.

```json
{
"timestamp": "2023-10-27T10:00:00Z",
"data": "Secret message content...",
"nounce": "8f9e2a...",  // Randomly generated per block
"previousHash": "000abc...",
"hash": "000123..."      // SHA-256(Index + Timestamp + Data + Nounce + PreviousHash)
}

🔮 Future Improvements
Subsequent versions of this project are planned to include:Chain Validation Endpoint: A dedicated endpoint to iterate through the chain and programmatically verify integrity.Proof of Work (PoW): Implementing a difficulty target (e.g., requiring hashes to start with 0000) to simulate mining difficulty. The core model is already prepared with the nonce property.P2P Networking: Broadcasting blocks to other nodes for decentralized operation.

🧑‍💻 Contributors
Name Role
Luis de Oliveira Project Lead, Developer
Nicholas SchreenProject Lead, Developer

📜 License
This project is created for educational purposes for the CIS-3353 Cybersecurity course and is released under the MIT License.

# ⛓️ SecureChain API

A lightweight Blockchain API developed for **CIS-3353** demonstrating cryptographic integrity through Hashing, Salting, and Proof-of-Work.

## 📄 Project Overview

This repository contains a simple Blockchain API built using **C#** and **ASP.NET Core**. The primary goal of this project is to demonstrate the fundamental concepts of Data Integrity and Immutable Ledger Technology within a cybersecurity context.

In a typical centralized database, an administrator can alter records without detection. In this blockchain implementation, every record (block) is cryptographically linked to the previous one. This project differentiates itself from standard blockchain tutorials by placing a heavy emphasis on **Cryptographic Salting** and **Proof-of-Work** to defend against specific attack vectors.

### Core Objectives
* **Integrity:** Ensuring that once data is written to the chain, it cannot be altered without invalidating the entire history.
* **Non-Repudiation:** Using unique signatures to verify the origin and content of data.
* **Defense in Depth:** Implementing multiple layers of security (SHA-256 + Random Salts + Difficulty Targets).

---

## 📈 Project Status

The project is **complete**. All planned milestones, including the security validation and attack simulation modules, have been finalized.

| Milestone | Status | Description |
| :--- | :--- | :--- |
| **Milestone 1: Basic End-to-End Chain** | **100% Complete** | Core API, block model, and basic creation/linking logic. |
| **Milestone 2: UI Visualization** | **100% Complete** | Frontend visualization (`index.html`) and backend serialization. |
| **Milestone 3: Mining & Block Lookup** | **100% Complete** | Block lookup by Hash, Nonce integration, and Proof-of-Work logic. |
| **Milestone 4: Validation & Tamper Testing** | **100% Complete** | Chain validation logic and "Tamper" simulation features to prove security. |

---

## 🛡️ Key Security Features

1.  **SHA-256 Hashing:** We utilize the SHA-256 (Secure Hash Algorithm 256-bit) to ensure the **Avalanche Effect** (a minor change invalidates the hash).
2.  **Cryptographic Salting:** A unique, random "salt" string is generated for every single block (`Hash = SHA256(Data + Salt)`). This defends against **Pre-computation Attacks** (Rainbow Tables).
3.  **Proof-of-Work (PoW):** The system implements a difficulty target (hashes must start with specific characters, e.g., `0000`). This makes rewriting the chain computationally expensive, preventing spam and tempering.
4.  **Chain Dependencies:** Each block contains the `PreviousHash` of the block preceding it.
5.  **Tamper Detection:** The system includes logic to re-calculate hashes across the chain to verify validity (`isValid: true/false`), visually alerting the user if the ledger has been compromised.

 ---

## 🛠️ Tech Stack

* **Language:** C# (C-Sharp)
* **Framework:** ASP.NET Core Web API
* **Runtime:** .NET 10.0+
* **Cryptography:** `System.Security.Cryptography`
* **Frontend:** HTML5 / CSS3 / JavaScript

---

## 📡 API Endpoints

1.  **Get Full Chain** (`GET /blockchain`)
    * Returns all blocks in chronological order.
2.  **Mine/Create Block** (`POST /blockchain`)
    * Body: `{"data": "Message"}`. Performs PoW and adds the block.
3.  **Find Block by Hash** (`GET /blockchain/{hash}`)
    * Returns a specific block object or 404.
4.  **Validate Chain** (`GET /blockchain/isvalid`)
    * Returns `true` if integrity is intact, `false` otherwise.
5.  **Tamper Block** (`POST /blockchain/tamper`)
    * **Debug Tool:** Intentionally corrupts data at index[1] to test security triggers.

---

## 🧩 Block Structure



Each block in our chain follows this JSON structure.



```json

{

"timestamp": "2023-10-27T10:00:00Z",

"data": "Secret message content...",

"nounce": "8f9e2a...",  # Randomly generated per block

"previousHash": "000abc...",

"hash": "000123..."      # SHA-256(Timestamp + Data + Nounce + PreviousHash)

}

```

---

## 👥 User Stories (Completed)

The following stories represent the full development lifecycle of the SecureChain project.

### User Story 1: Functional End-to-End API
**As an API Consumer**, I want a functional API to **interact with the chain and retrieve block data**, **so that** I can test the core creation, linking, and storage mechanisms of the blockchain.
* **Status:** Closed (Milestone 1)

### User Story 2: Reliable Data Exchange
**As a Frontend Logic Developer**, I want the backend to **always serialize block data consistently** (e.g., to JSON), **so that** I can rely on a stable data structure when building the UI.
* **Status:** Closed (Milestone 2)

### User Story 3: Visualizing the Complete Chain
**As a User**, I want the web interface to **display all blocks in a clean, scrollable list**, **so that** I can easily view the entire history of the chain from the genesis block onward.
* **Status:** Closed (Milestone 2)

### User Story 4: Block Lookup & UI Integration
**As an API User**, I want to **search for a specific block using its unique hash** via the API, **and see the result displayed on the front end**, **so that** I can quickly retrieve specific transaction details.
* **Status:** Closed (Milestone 3)

### User Story 5: Proof of Work & Mining
**As a Core Developer**, I want to implement a **Nonce and Difficulty Target system**, **so that** new blocks require computational effort ("Mining") to be added, securing the network against spam and history rewriting.
* **Status:** Closed (Milestone 3)

### User Story 6: Chain Validation Logic
**As a User**, I want the system to **automatically check the integrity of the chain**, **so that** I am immediately warned (via a visual indicator) if any data has been altered.
* **Status:** Closed (Milestone 4)

### User Story 7: Simulate Block Data Tampering
**As a Tester**, I want to **intentionally corrupt data in a specific block** via a "Tamper" button, **so that** I can test the validation logic and prove that the system correctly flags the chain as invalid.
* **Status:** Closed (Milestone 4)

---
## 📸 Project Final Report Evidence

This section maps the user interface (Frontend) visualization to the underlying API (Backend) logic.

### 1. Blockchain State (Ledger View)
**Objective:** Display the current state of the blockchain.
| **Frontend (Blazor/HTML)** | **Backend (API JSON)** |
| :--- | :--- |
| https://github.com/user-attachments/assets/44854996-1b97-4fcb-aef6-a69b759c56c5| https://github.com/user-attachments/assets/6a9ec612-41eb-4d54-9a52-e53498220898 <br> https://github.com/user-attachments/assets/2f2b2b19-20aa-431f-aca0-a0362b07ebb3|
| *Visualized list of blocks.* | *GET `/blockchain` returning the full array.* |

### 2. Feature: Search Block by Hash (Success)
**Objective:** Retrieve a specific block using its unique SHA-256 signature.
| **Frontend Result** | **Backend Response** |
| :--- | :--- |
| https://github.com/user-attachments/assets/6de2b3c4-0a2b-4ac2-a6b8-7c372c17714b | https://github.com/user-attachments/assets/83d7acd8-f8ec-49b6-9ebc-b1a84d0e9538|
| *Block found and highlighted.* | *Returns specific block object.* |

### 3. Feature: Search Block by Hash (Not Found)
**Objective:** Handle queries for non-existent blocks.
| **Frontend Result** | **Backend Response** |
| :--- | :--- |
| https://github.com/user-attachments/assets/141eeb7d-1880-4d07-978f-ac1e34006c9d | https://github.com/user-attachments/assets/730c5944-9f1f-4b7c-998b-41089a53bfd9|
| *User-friendly error message.* | *API returns specific error string.* |

### 4. Security: Chain Validation (Success)
**Objective:** Cryptographically verify that `PreviousHash` links match the current `Hash` for all blocks.
| **Frontend Result** | **Backend Response** |
| :--- | :--- |
| https://github.com/user-attachments/assets/ee8255e6-d764-4636-a179-7c94762b6944 | https://github.com/user-attachments/assets/09d541e9-1d96-4e6c-894d-e649161cb21c|
| *System confirms integrity.* | *`"isValid": true`* |

### 5. Security: Tampering Detection (Avalanche Effect)
**Objective:** Demonstrate that altering data in a previous block breaks the chain.
| **Frontend Result** | **Backend Response** |
| :--- | :--- |
| https://github.com/user-attachments/assets/aa610952-7a09-43a3-97ec-dedbb74d5905 | https://github.com/user-attachments/assets/50ff07c3-3101-448b-b7dc-677ed693b532|
| *Red warning: Chain compromised.* | *`"isValid": false`* |

---

## 🔮 Possible Future Improvements

Subsequent versions of this project are planned to include:
* **P2P Networking:** Broadcasting blocks to other nodes for decentralized operation (Distributed Ledger).
* **Smart Contracts:** Basic implementation of logic execution within blocks.
* **Database Integration:** Moving from in-memory/JSON storage to a SQL or NoSQL database for persistence.

## 👥 Contributors

| Role | Name |
| :--- | :--- |
| **Lead, Developer** | Nicholas |
| **Developer** | Luis de Oliveira |

## 📖 Contributions

| Team contributor | Total story points contributed | Contribution % |
| :--- | :--- |  :--- |
| **Nicholas** | 32 | 51%
| **Luis de Oliveira** | 31 | 49% |

## 📜 License

This project is created for educational purposes for the **CIS-3353 Cybersecurity** course and is released under the MIT License.

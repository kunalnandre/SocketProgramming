# Socket Programming 

This repository contains a **TCP client-server application** implemented in **.NET**. The assignment demonstrates network communication, encryption, and handling multiple client requests.

---

## **Features**
1. **TCP Client-Server Communication**
   - The **server** listens for client requests and sends the current system time based on the client's input.
   - The **client** sends a request (e.g., `SetA-Two`) and displays the server's response.

2. **Encryption (AES)**
   - All messages are encrypted using **AES** (Advanced Encryption Standard).
   - The client encrypts the request before sending it.
   - The server decrypts the request, processes it, and encrypts the response before sending it back.

---

## **How to Run**

### **Start the Server**:
```bash
cd Server
dotnet run
```
The server will start listening on port **8888**.

### **Start the Client**:
```bash
cd Client
dotnet run
```
Enter a request (e.g., `SetA-Two`) in the client.
The server will respond with the current system time **n times** (where `n` is the value from the dataset).

---

## **Example**
**Input:**
```
Enter request (e.g., SetA-Two): SetA-Two
```
**Output:**
```
12-02-2025 14:47:31
12-02-2025 14:47:32
```

---

## **Dataset**
The server processes requests based on the following dataset:

```json
{
  "SetA": { "One": 1, "Two": 2 },
  "SetB": { "Three": 3, "Four": 4 },
  "SetC": { "Five": 5, "Six": 6 },
  "SetD": { "Seven": 7, "Eight": 8 },
  "Sett": { "Nine": 9, "Ten": 10 }
}
```
For example, if the client sends `SetA-Two`, the server will respond with the current time **2 times**.

---

## **Encryption Details**
- **Algorithm:** AES (Advanced Encryption Standard)
- **Key:** `0123456789ABCDEF0123456789ABCDEF` (32-byte key)
- **IV:** `ABCDEF0123456789` (16-byte initialization vector)

---

## **Repository Structure**
```
SocketProgramming/
├── Client/                # Client application
│   ├── Program.cs         # Client logic
│   ├── Crypto.cs          # Encryption/Decryption logic
│   ├── Client.csproj      # Project file
├── Server/                # Server application
│   ├── Program.cs         # Server logic
│   ├── Crypto.cs          # Encryption/Decryption logic
│   ├── Server.csproj      # Project file
├── README.md              # This file
```


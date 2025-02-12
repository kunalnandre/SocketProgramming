using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;

class Server
{
    static Dictionary<string, Dictionary<string, int>> data = new Dictionary<string, Dictionary<string, int>>()
    {
        { "SetA", new Dictionary<string, int> { { "One", 1 }, { "Two", 2 } } },
        { "SetB", new Dictionary<string, int> { { "Three", 3 }, { "Four", 4 } } },
        { "SetC", new Dictionary<string, int> { { "Five", 5 }, { "Six", 6 } } },
        { "SetD", new Dictionary<string, int> { { "Seven", 7 }, { "Eight", 8 } } },
        { "Sett", new Dictionary<string, int> { { "Nine", 9 }, { "Ten", 10 } } }
    };

    static void Main()
    {
        try
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 8888);
            listener.Start();
            Console.WriteLine("Server started...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Thread thread = new Thread(() => HandleClient(client));
                thread.Start();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void HandleClient(TcpClient client)
    {
        try
        {
            using (NetworkStream stream = client.GetStream())
            {
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // Decrypt the received message
                string decryptedMessage = Crypto.Decrypt(received);
                Console.WriteLine($"Received (Decrypted): {decryptedMessage}");

                // Process the decrypted string (e.g., "SetA-Two")
                string[] parts = decryptedMessage.Split('-');
                string response = "EMPTY";

                if (parts.Length == 2 && data.ContainsKey(parts[0]) && data[parts[0]].ContainsKey(parts[1]))
                {
                    int n = data[parts[0]][parts[1]];
                    for (int i = 0; i < n; i++)
                    {
                        response = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                        // Encrypt the response before sending
                        string encryptedResponse = Crypto.Encrypt(response);
                        byte[] data = Encoding.UTF8.GetBytes(encryptedResponse);
                        stream.Write(data, 0, data.Length);
                        Thread.Sleep(1000); // 1-second interval
                    }
                    return;
                }

                // Send "EMPTY" if invalid (encrypted)
                string encryptedEmpty = Crypto.Encrypt(response);
                byte[] emptyData = Encoding.UTF8.GetBytes(encryptedEmpty);
                stream.Write(emptyData, 0, emptyData.Length);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            client.Close();
        }
    }
}
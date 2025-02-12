using System;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        try
        {
            using (TcpClient client = new TcpClient("127.0.0.1", 8888))
            {
                Console.Write("Enter request (e.g., SetA-Two): ");  
                string? input = Console.ReadLine();

                // Ensure input is not null or empty
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty.");
                    return;
                }

                // Encrypt the message before sending
                string encryptedInput = Crypto.Encrypt(input);

                NetworkStream stream = client.GetStream();
                byte[] data = Encoding.UTF8.GetBytes(encryptedInput);
                stream.Write(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string encryptedResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    // Decrypt the response from the server
                    string decryptedResponse = Crypto.Decrypt(encryptedResponse);
                    Console.WriteLine($"Received (Decrypted): {decryptedResponse}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
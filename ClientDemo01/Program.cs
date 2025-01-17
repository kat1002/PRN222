using System.Net.Sockets;

namespace ClientDemo01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string server = "127.0.0.1";

            // Set the TcpListener on port 13000
            int port = 13000;
            ConnectToServer(server, port);
        }

        static void ConnectToServer(String server, int port)
        {
            string message, responseData;
            int bytes;

            try
            {
                // Create a TCP Client
                TcpClient tcpClient = new TcpClient(server, port);
                Console.Title = "Client Application Demo 01";

                NetworkStream stream = null;

                while (true)
                {
                    Console.Write("Input message <Press enter to exit>: ");
                    message = Console.ReadLine();
                    if (message == string.Empty) break;

                    // Translate the passed message into ASCII and store it as a byte array 
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                    // Get a client stream for reading and writing 
                    stream = tcpClient.GetStream();

                    // Send the message to the connected TcpServer
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine($"Sent: {message}");

                    // Receive the TcpServer response
                    // Use Buffer to store the response bytes

                    data = new Byte[256];

                    // Read the first batch of the TcpServer response bytes
                    bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine($"Received: {responseData}");
                }

                // Shutdown and end connection
                tcpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

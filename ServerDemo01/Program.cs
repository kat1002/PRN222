using System.Net;
using System.Net.Sockets;

namespace ServerDemo01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string host = "127.0.0.1";
            // Set the Listener to port 13000

            int port = 13000;
            ExecuteServer(host, port);
        }

        static void ProcessMessage(object param)
        {
            string data;
            int count;
            try
            {
                TcpClient tcpClient = param as TcpClient;

                //Buffer for reading data
                Byte[] bytes = new Byte[256];

                // Get a stream object for reading and writing
                NetworkStream stream = tcpClient.GetStream();

                // Loop to receive all the data sent by client
                while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes into a ASCII string
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, count);
                    Console.WriteLine($"Received: {data} at {DateTime.Now:t}");

                    //Process data sent by client
                    data = $"{data.ToUpper()}";
                    Byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    // Send back a response
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine($"Sent: {data}");
                }

                // Shutdown and end connection
                tcpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
                Console.WriteLine("Waiting Message...");
            }
        }

        static void ExecuteServer(string host, int port)
        {
            int Count = 0;
            TcpListener server = null;
            try
            {
                Console.Title = "Server Application Demo 01";
                IPAddress localAddress = IPAddress.Parse(host);
                server = new TcpListener(localAddress, port);

                // Start listening for client application
                server.Start();
                Console.WriteLine(new string('*', 50));
                Console.WriteLine("Waiting for a connection...");

                // Enter the listening loop
                while (true)
                {
                    // Perform a blocking call to accept requests
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine($"Number of client connected: {++Count}");
                    Console.WriteLine(new string('*', 50));

                    // Create a thread to receive and send message
                    Thread thread = new Thread(new ParameterizedThreadStart(ProcessMessage));
                    thread.Start(client);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
                Console.WriteLine("Waiting Message...");
            }
            finally
            {
                server.Stop();
                Console.WriteLine("Server stopped. Press any key to exit!");
            }
        }
    }
}

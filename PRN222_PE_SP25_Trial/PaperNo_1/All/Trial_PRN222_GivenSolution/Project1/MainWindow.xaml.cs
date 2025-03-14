using System.Net.Sockets;
using System.Text.Json.Nodes;
using System.Windows;

namespace Project1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Star inputStar;
        List<Star> listStars;
        string server = "127.0.0.1";
        int port = 5000;

        public MainWindow()
        {
            inputStar = new Star();
            listStars = new List<Star>();
            InitializeComponent();
        }

        private void AddToListBtn(object sender, RoutedEventArgs e)
        {
            inputStar = new Star(StarName.Text, DobPicker.SelectedDate, Description.Text, IsMaleCheckbox.IsChecked, Nationality.Text);
            listStars.Add(inputStar);

            StarList.Text = "";
            foreach (Star star in listStars)
            {
                StarList.Text += $"{star.ToString()}\n";
            }
        }

        private void SendToServerBtn(object sender, RoutedEventArgs e)
        {
            SendMessage(server, port, listStars);
        }

        private void SendMessage(String server, int port, List<Star> starList)
        {
            string message, responseData;
            int bytes;

            try
            {
                // Create a TCP Client
                TcpClient tcpClient = new TcpClient(server, port);
                NetworkStream stream = null;

                JsonArray jsonArray = new JsonArray();

                foreach (var star in starList)
                {
                    JsonObject jsonObject = new JsonObject();
                    jsonObject.Add("Name", star.Name);
                    jsonObject.Add("Dob", star.Dob);
                    jsonObject.Add("Description", star.Description);
                    jsonObject.Add("Male", star.Male);
                    jsonObject.Add("Nationality", star.Nationnality);
                    jsonArray.Add(jsonObject);
                }
                //StarList.Text = jsonArray.ToString();


                // Translate the passed message into ASCII and store it as a byte array 
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(jsonArray.ToString());

                // Get a client stream for reading and writing 
                stream = tcpClient.GetStream();

                // Send the message to the connected TcpServer
                stream.Write(data, 0, data.Length);
                //Console.WriteLine($"Sent: {message}");

                // Receive the TcpServer response
                // Use Buffer to store the response bytes

                data = new Byte[256];

                // Read the first batch of the TcpServer response bytes
                bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                //Console.WriteLine($"Received: {responseData}");

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
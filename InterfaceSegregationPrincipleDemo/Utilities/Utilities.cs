using InterfaceSegregationPrincipleDemo.Model;
using Newtonsoft.Json;

namespace InterfaceSegregationPrincipleDemo.Utilities
{
    class Utilities
    {
        static string ReadFile(string filename)
        {
            return File.ReadAllText(filename);
        }

        public static List<Video> ReadData(string fileId)
        {
            var filename = "Data/BookStore" + fileId + ".json";
            var cadJSON = ReadFile(filename);
            return JsonConvert.DeserializeObject<List<Video>>(cadJSON);
        }
    }
}

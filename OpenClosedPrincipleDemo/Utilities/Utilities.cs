using Newtonsoft.Json;
using OpenClosedPrincipleDemo.Model;

namespace OpenClosedPrincipleDemo.Utilities
{
    class Utilities
    {
        static string ReadFile(string filename)
        {
            return File.ReadAllText(filename);
        }

        public static List<Book> ReadData()
        {
            var cadJSON = ReadFile("Data/BookStore_01.json");
            return JsonConvert.DeserializeObject<List<Book>>(cadJSON);
        }

        public static List<Book> ReadDataExtra()
        {
            List<Book> books = ReadData();
            var cadJSON = ReadFile("Data/BookStore_02.json");
            books.AddRange(JsonConvert.DeserializeObject<List<Book>>(cadJSON));

            return books;
        }
    }
}

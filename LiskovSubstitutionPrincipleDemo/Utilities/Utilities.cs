using LiskovSubstitutionPrincipleDemo.Model;
using Newtonsoft.Json;

namespace LiskovSubstitutionPrincipleDemo.Utilities
{
    class Utilities
    {
        static string ReadFile(string filename)
        {
            return File.ReadAllText(filename);
        }

        public static List<Book> ReadData()
        {
            var cadJSON = ReadFile("Data/BookStore1.json");
            return JsonConvert.DeserializeObject<List<Book>>(cadJSON);
        }

        public static List<Book> ReadDataExtra(string extra)
        {
            List<Book> books = ReadData();
            var filename = "Data/BookStore2.json";
            var cadJSON = ReadFile(filename);
            books.AddRange(JsonConvert.DeserializeObject<List<Book>>(cadJSON));

            if (extra.ToLower() == "topic")
            {
                filename = "Data/BookStore3.json";
                cadJSON = ReadFile(filename);
                books.AddRange(JsonConvert.DeserializeObject<List<TopicBook>>(cadJSON));
            }

            return books;
        }
    }
}

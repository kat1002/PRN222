using InterfaceSegregationPrincipleDemo.Model;

namespace InterfaceSegregationPrincipleDemo
{
    internal class Program
    {
        static List<Video> bookList;
        static void PrintBooks(List<Video> books)
        {
            Console.WriteLine("List of Books");
            Console.WriteLine("-----------------------");
            foreach (Video book in books)
            {
                Console.WriteLine($"{book.Title.PadRight(39, ' ')} {book.Author.PadRight(20, ' ')} {book.Price} {book.Topic?.PadRight(12, ' ')} {book.Duration ?? ""}");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            string id = string.Empty;
            Console.Title = "Interface Segregation Principle Demo";
            do
            {
                Console.WriteLine("File no. to read: 1/2/3 - Enter(exit): ");
                id = Console.ReadLine();
                if ("123".Contains(id) && !String.IsNullOrEmpty(id))
                {
                    bookList = Utilities.Utilities.ReadData(id);
                    PrintBooks(bookList);
                }
            } while (!String.IsNullOrWhiteSpace(id));
        }
    }
}

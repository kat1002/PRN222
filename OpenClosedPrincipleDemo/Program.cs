using OpenClosedPrincipleDemo.Model;

namespace OpenClosedPrincipleDemo
{
    internal class Program
    {
        static List<Book> bookList;
        static void PrintBooks(List<Book> books)
        {
            Console.WriteLine("List of Books");
            Console.WriteLine("-----------------------");
            foreach (Book book in books)
            {
                Console.WriteLine($"{book.Title.PadRight(39, ' ')} {book.Author.PadRight(20, ' ')} {book.Price}");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Please, press 'yes' to read an extra file");
            Console.WriteLine("or any other key for a single file");

            var ans = Console.ReadLine();
            bookList = (ans.ToLower() != "yes") ? Utilities.Utilities.ReadData() : Utilities.Utilities.ReadDataExtra();
            PrintBooks(bookList);
        }
    }
}

namespace LiskovSubstitutionPrincipleDemo.Model
{
    public class Book : IBook
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
    }

    public class TopicBook : Book
    {
        public String Topic { get; set; }
    }
}

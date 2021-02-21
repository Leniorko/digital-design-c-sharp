using System;

namespace word_counter
{
    class Program
    {
        static void Main(string[] args)
        {
            Book book;
            string text;
            book = new Book();
            text = Console.ReadLine();


            Console.WriteLine(book.TestFunc(text));
        }
    }

    class Book
    {
        string Content;

        public string TestFunc(string content)
        {
            return ("Your value is " + content);
        }
    }

}

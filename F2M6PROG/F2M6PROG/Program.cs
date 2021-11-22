using System;

namespace F2M6PROG
{
    class Program
    {
        static void Main(string[] args)
        {
            Archive database = new Archive();
            Book book1 = new Book("Volume I");
            Book book2 = new Book("Volume II");
            database.AddBook(book1,book2);
            Console.WriteLine(database.GetLibrary(0).BookName);
        }
        
    }
}

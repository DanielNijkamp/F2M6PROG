using System;
using System.Collections.Generic;
using System.Text;

namespace F2M6PROG
{
    class Archive
    {
        private List<Book> Library = new List<Book>();
        public void AddBook(params Book[] bookArray)
        {
            foreach (Book book in bookArray)
            {
                Library.Add(book);
            }
        }
        public Book GetLibrary(int pos)
        {
            return Library[pos];
        }

    }
}

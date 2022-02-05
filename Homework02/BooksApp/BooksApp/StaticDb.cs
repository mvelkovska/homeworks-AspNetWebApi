using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApp.Models;

namespace BooksApp
{
    public static class StaticDb
    {
        public static List<Book> books = new List<Book>()
        {
            //three authors and 5 titles
            new Book(){ Author="Author1", Title="Title1" },
            new Book(){ Author="Author2", Title="Title2" },
            new Book(){ Author="Author3", Title="Title3" },
            new Book(){ Author="Author2", Title="Title4" },
            new Book(){ Author="Author3", Title="Title5" }

        }; 

    }
}

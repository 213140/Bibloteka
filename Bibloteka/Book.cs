using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibloteka
{
    class Book
    {
        public string book_name;
        public string book_author;
        public int page_quantity;
        public bool booked;

        public Book(string name)
        {
            book_name = name;
            book_author = "unknow";
            page_quantity = 1;
            booked = false;
        }
        public Book(string name, string author, int pages, bool reservation_status)
        {
            book_name = name;
            book_author = author;
            page_quantity = pages;
            booked = reservation_status;
        }

    }
}

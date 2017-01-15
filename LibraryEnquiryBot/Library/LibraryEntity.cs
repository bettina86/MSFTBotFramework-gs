using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class LibraryEntity
    {
        public List<Book> Books;
        public LibraryEntity()
        {
            Books = new List<Book>();
            Books.Add(new Book() { BookName = "Harry Potter 1", BookAuthor = "JK Rowling", BookISBN = 123451, BookPublishYear = 2001, InStock = true });
            Books.Add(new Book() { BookName = "Harry Potter 2", BookAuthor = "JK Rowling", BookISBN = 123452, BookPublishYear = 2002, InStock = false });
            Books.Add(new Book() { BookName = "Harry Potter 3", BookAuthor = "JK Rowling", BookISBN = 123453, BookPublishYear = 2003, InStock = false });
            Books.Add(new Book() { BookName = "Harry Potter 4", BookAuthor = "JK Rowling", BookISBN = 123454, BookPublishYear = 2004, InStock = true });
            Books.Add(new Book() { BookName = "Harry Potter 5", BookAuthor = "JK Rowling", BookISBN = 123455, BookPublishYear = 2005, InStock = false });
            Books.Add(new Book() { BookName = "Harry Potter 6", BookAuthor = "JK Rowling", BookISBN = 123456, BookPublishYear = 2006, InStock = true });
            Books.Add(new Book() { BookName = "Percy Jackson 1", BookAuthor = "Rick Riordan", BookISBN = 234561, BookPublishYear = 2003, InStock = false });
            Books.Add(new Book() { BookName = "Percy Jackson 2", BookAuthor = "Rick Riordan", BookISBN = 234562, BookPublishYear = 2004, InStock = true });
            Books.Add(new Book() { BookName = "Percy Jackson 3", BookAuthor = "Rick Riordan", BookISBN = 234563, BookPublishYear = 2005, InStock = false });
            Books.Add(new Book() { BookName = "Percy Jackson 4", BookAuthor = "Rick Riordan", BookISBN = 234564, BookPublishYear = 2006, InStock = true });
            Books.Add(new Book() { BookName = "Percy Jackson 5", BookAuthor = "Rick Riordan", BookISBN = 234565, BookPublishYear = 2007, InStock = false });
            Books.Add(new Book() { BookName = "Percy Jackson 6", BookAuthor = "Rick Riordan", BookISBN = 234566, BookPublishYear = 2008, InStock = true });
        }
        public int LibraryId { get {
                return 123;
            }
            set { }
        }
        public string LibraryName { get { return "MICLibrary"; } set { } }
        public string LibrarianName { get { return "Boss"; } set { } }
       
        public int GetTotalBooksInLibrary()
        {
            return Books.Count;
        }
        public bool GetResultInStock(int BookISBN)
        {
            bool bookStatus = (bool)Books.Where(x => x.BookISBN == BookISBN).Select(x => x.InStock).ToList()[0];
            return bookStatus;
        }
        public string GetBookAuthorName(string bookName)
        {
            List<string> bookAuthorList = Books.Where(x => x.BookName.ToLower().Contains(bookName.ToLower())).Select(x => x.BookAuthor).ToList<string>();
            if (bookAuthorList.Count > 0)
                return bookAuthorList[0].ToString();
            else
                return "";
        }
        public int GetBookPublishYear(int BookISBN)
        {
            int bookPublishYear = Books.Where(x => x.BookISBN == BookISBN).Select(x => x.BookPublishYear).ToList()[0];
            return bookPublishYear;
        }
    }
    public class Book {
        public string BookName { get; set; }
        public Int32 BookISBN { get; set; }
        public string BookAuthor { get; set; }
        public int BookPublishYear { get; set; }
        public bool InStock { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class BookRepository : IAuthorRepository<Book>
    {
        IList<Book> books;
        public BookRepository()
        {
            books = new List<Book>()
            {
               new Book{id=1,title="c programming" ,descreption=" without description",author=new Author{id=1,fullName="Asmaa Ali"},imgUrl="tools1.PNG" },
               new Book{id=3,title="java programming" ,descreption=" without description",author=new Author{id=3,fullName="Eman Abdo"},imgUrl="tools2.PNG" }
            };
        }

        public IList<Book> list()
        {
            return books;
        }

        public void add(Book book)
        {
            book.id = books.Max(b => b.id) + 1;
            books.Add(book);
        }

        public void delete(int id)
        {
            Book book = this.findOne(id);
            books.Remove(book);
        }

        public Book findOne(int id)
        {
            return books.SingleOrDefault(b => b.id == id);
        }

        public IList<Book> getAll()
        {
            return books;
        }


        public void update(int id, Book newBook)
        {
            Book book = this.findOne(id);
            book.title = newBook.title;
            book.descreption = newBook.descreption;
            book.author = newBook.author;
            book.imgUrl = newBook.imgUrl;
        }
    }

}

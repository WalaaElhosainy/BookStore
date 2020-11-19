using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class BookDbRepository : IAuthorRepository<Book>
    {
        BookstoreDbContext db;
        public BookDbRepository(BookstoreDbContext _db)
        {
            db = _db;
        } 

        public IList<Book> list()
        {
            return db.Books.Include(b => b.author).ToList();
        }

        public void add(Book book)
        {
           
            db.Books.Add(book);
            db.SaveChanges();
        }

        public void delete(int id)
        {
            Book book = this.findOne(id);
           db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book findOne(int id)
        {
            return db.Books.Include(b=>b.author).SingleOrDefault(b => b.id == id);
        }

        public IList<Book> getAll()
        {
            return db.Books.ToList();
        }


        public void update(int id, Book newBook)
        {
            db.Update(newBook);
            db.SaveChanges();
        }
    }

}

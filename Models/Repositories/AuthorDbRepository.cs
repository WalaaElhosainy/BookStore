using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class AuthorDbRepository : IAuthorRepository<Author>
    {
        BookstoreDbContext db;
        public AuthorDbRepository(BookstoreDbContext _db)
        {
            db = _db;
        }
        public void add(Author author)
        {
           
            db.Authors.Add(author);
            db.SaveChanges();
        }

      
        public void delete(int id)
        {
            Author author = this.findOne(id);
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public Author findOne(int id)
        {
            return db.Authors.SingleOrDefault(author => author.id == id);

        }

        public IList<Author> getAll()
        {
            return db.Authors.ToList();
        }

        public IList<Author> list()
        {
            return db.Authors.ToList();
        }

        public void update(int id, Author newAuthor)
        {
            db.Update(newAuthor);
            db.SaveChanges();
        }
    }
}


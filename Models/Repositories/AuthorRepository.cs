using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class AuthorRepository : IAuthorRepository<Author>
    {
           public IList<Author> authors = new List<Author>()
        {
            new Author{id=1 , fullName="Walaa Hussein"},
            new Author{id=2 , fullName="Eman Abdo"},
            new Author{id=3 , fullName="Asmaa Ali"}
        };
        public void add(Author author)
        {
            author.id = authors.Max(b => b.id) + 1;
            authors.Add(author);
        }

        public void delete(int id)
        {
            Author author= this.findOne(id);
            authors.Remove(author);
        }

       public Author findOne(int id)
        {
            return authors.SingleOrDefault(author => author.id == id);
            
        }

        public IList<Author>getAll()
        {
            return authors;
        }

        public IList<Author>list()
        {
            return authors;
        }

        public void update(int id, Author newAuthor)
        {
            Author author = this.findOne(id);
            author.fullName = newAuthor.fullName;
        }
    }
}

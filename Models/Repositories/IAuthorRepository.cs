using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
   public interface IAuthorRepository<Entity>
    {
        IList<Entity> list();
        IList<Entity> getAll();
        Entity findOne(int id);
        void add(Entity entity);
        void update(int id, Entity entity);
        void delete(int id);
    }
}


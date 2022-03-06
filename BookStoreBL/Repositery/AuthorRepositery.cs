using BookStoreDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBL.Repositery
{
   public class AuthorRepositery : IBookStoreRepositery<Author>
    {
        private readonly BookStoreContext DB;

        public AuthorRepositery(BookStoreContext DB)
        {
            this.DB = DB;
        }
        public void Add(Author entity)
        {
            DB.Authors.Add(entity);
            DB.SaveChanges();

        }

        public void Delete(int id)
        {
            var entity = Find(id);
            DB.Authors.Remove(entity);
            DB.SaveChanges();
        }

        public Author Find(int id)
        {
            return DB.Authors.FirstOrDefault(b => b.Id == id);
        }

        public IList<Author> List()
        {
            return DB.Authors.ToList();
        }


       
        public List<Author> Search(Expression<Func<Author, bool>> filter = null)
        {
            return DB.Authors.Where<Author>(filter).ToList();
        }

        public void Update(Author entity)
        {
            DB.Update(entity);
            DB.SaveChanges();
        }
    }
}

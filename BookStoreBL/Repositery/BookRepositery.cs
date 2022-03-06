using BookStoreDAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBL.Repositery
{
    public class BookRepositery : IBookStoreRepositery<Book>
    {
        private readonly BookStoreContext DB;

        public BookRepositery(BookStoreContext DB)
        {
            this.DB = DB;
        }
        public void Add(Book entity)
        {
            DB.Books.Add(entity);
            DB.SaveChanges();
            
        }

        public void Delete(int id)
        {
            var entity = Find(id);
            DB.Books.Remove(entity);
            DB.SaveChanges();
        }

        public Book Find(int id)
        {
            return DB.Books.Include(a => a.Author).FirstOrDefault(b => b.Id == id);
        }

        public IList<Book> List()
        {
            return DB.Books.Include(a => a.Author).ToList();
        }

        public List<Book> Search(Expression<Func<Book, bool>> filter = null)
        {
            return DB.Books.Include(a => a.Author).Where<Book>(filter).ToList();

        }

        public void Update( Book entity)
        {
            DB.Update(entity);
            DB.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBL.Repositery
{
  public  interface IBookStoreRepositery<TEntity>
    {
        IList<TEntity> List();
        TEntity Find(int id);
        void Add(TEntity entity);
        void Update( TEntity entity);
        void Delete(int id);
        List<TEntity> Search(Expression<Func<TEntity, bool>> filter = null );
    }
}

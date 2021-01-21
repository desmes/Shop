using Shop.Core.Models;
using System.Linq;

namespace Shop.Core.Logic
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Delete(int id);
        T FindById(int id);
        void Insert(T t);
        void SaveChanges();
        void Update(T t);
    }
}
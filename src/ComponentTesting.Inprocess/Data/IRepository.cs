using System.Linq;

namespace ComponentTesting.Inprocess.Data
{
    public interface IRepository<T>
    {
        void Save(T student); 
        IQueryable<T> All(); 
        T Get(long id); 
        void Delete(T item); 
        void Update(T item);
    }
}
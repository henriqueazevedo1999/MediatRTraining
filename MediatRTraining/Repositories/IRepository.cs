namespace Project.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Get(int id);
    Task Add(T item);
    Task Edit(T item);
    Task<bool> Delete(int id);    
}

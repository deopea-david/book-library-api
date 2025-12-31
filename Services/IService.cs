namespace BookLibraryAPI.Services;

public interface IService<T>
  where T : class
{
  public Task<IEnumerable<T>> GetMany();
  public Task<T?> GetById(int id);
  public Task<T> Create(T entity);
  public Task<T> Update<U>(U updates, T original)
    where U : class;
  public Task<int> Delete(T entity);
}

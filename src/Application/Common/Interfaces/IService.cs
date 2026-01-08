namespace BookLibraryAPI.Application.Common.Interfaces;

public interface IService<T>
  where T : class
{
  public const int DefaultPage = 1;
  public const int DefaultPageSize = 25;

  public (int page, int size) ParsePageAndSize(int? page, int? size);

  public Task<IEnumerable<T>> GetMany(int? page = DefaultPage, int? size = DefaultPageSize);
  public Task<T?> GetById(int id);
  public Task<T> Create(T entity);
  public Task<T> Update<U>(U updates, T original)
    where U : class;
  public Task<int> Delete(T entity);
}

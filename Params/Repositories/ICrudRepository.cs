namespace Wond.Params.Repositories;

public interface ICrudRepository<T> {
   

    public IQueryable<T> GetAll();
    public Task<T?> GetByIdAsync(int id);
    public int GetLastestId();
    public Task CreateAsync(T en);
    public void Update(T en);
    public void DeleteById(int id);
    public Task SaveChangesAsync();

  
}

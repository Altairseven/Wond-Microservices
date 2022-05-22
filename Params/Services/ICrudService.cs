namespace Wond.Params.Services;

public interface ICrudService<T> {
    
    public List<T> GetList();
    public Task<T> GetByIdAsync(int id);
    public Task<T> CreateAsync(T en);
    public Task<T> UpdateASync(T en);
    public Task DeleteByIdAsync(int id);

}

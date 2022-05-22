
using Wond.Params.Data;
using Wond.Params.Models;


namespace Wond.Params.Repositories;

public class CategoriesRepository : ICrudRepository<Category> {

    private readonly ParamsDbContext _db;

    public CategoriesRepository(ParamsDbContext db) {
        _db = db;
    }

    public IQueryable<Category> GetAll() {
        return _db.Categories;
    }

    public async Task<Category?> GetByIdAsync(int id) {
        var en = await _db.Categories.FindAsync(id);
        return en;
    }

    public int GetLastestId() {
        var en = _db.Categories.Max(x => x.Id);
        return en;
    }

    public async Task CreateAsync(Category en) {
      await _db.Categories.AddAsync(en);
      ;
    }

    public void Update(Category en) {
        var obj = _db.Categories.First(x => x.Id == en.Id);
    
        obj.Name = en.Name;
        
    }

    public void DeleteById(int id) {
        var obj = _db.Categories.First(x=> x.Id == id);
        _db.Categories.Remove(obj);
        
    }

    public Task SaveChangesAsync() {
        return _db.SaveChangesAsync();
    }

    
}

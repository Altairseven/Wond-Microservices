
using Wond.Params.Data;
using Wond.Params.Models;


namespace Wond.Params.Repositories;

public class SizesRepository : ICrudRepository<Size> {

    private readonly ParamsDbContext _db;

    public SizesRepository(ParamsDbContext db) {
        _db = db;
    }

    public IQueryable<Size> GetAll() {
        return _db.Sizes;
    }

    public async Task<Size?> GetByIdAsync(int id) {
        var en = await _db.Sizes.FindAsync(id);
        return en;
    }

    public int GetLastestId() {
        var en = _db.Sizes.Max(x => x.Id);
        return en;
    }

    public async Task CreateAsync(Size en) {
      await _db.Sizes.AddAsync(en);
      ;
    }

    public void Update(Size en) {
        var obj = _db.Sizes.First(x => x.Id == en.Id);
    
        obj.Name = en.Name;
        
    }

    public void DeleteById(int id) {
        var obj = _db.Sizes.First(x=> x.Id == id);
        _db.Sizes.Remove(obj);
        
    }

    public Task SaveChangesAsync() {
        return _db.SaveChangesAsync();
    }

    
}

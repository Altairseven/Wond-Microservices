
using Wond.Params.Data;
using Wond.Params.Models;


namespace Wond.Params.Repositories;

public class ColorsRepository : ICrudRepository<Color> {

    private readonly ParamsDbContext _db;

    public ColorsRepository(ParamsDbContext db) {
        _db = db;
    }

    public IQueryable<Color> GetAll() {
        return _db.Colors;
    }

    public async Task<Color?> GetByIdAsync(int id) {
        var en = await _db.Colors.FindAsync(id);
        return en;
    }

    public int GetLastestId() {
        var en = _db.Colors.Max(x => x.Id);
        return en;
    }

    public async Task CreateAsync(Color en) {
      await _db.Colors.AddAsync(en);
      ;
    }

    public void Update(Color en) {
        var obj = _db.Colors.First(x => x.Id == en.Id);
    
        obj.Name = en.Name;
        
    }

    public void DeleteById(int id) {
        var obj = _db.Colors.First(x=> x.Id == id);
        _db.Colors.Remove(obj);
        
    }

    public Task SaveChangesAsync() {
        return _db.SaveChangesAsync();
    }

    
}

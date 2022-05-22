
using Wond.Params.Data;
using Wond.Params.Models;


namespace Wond.Params.Repositories;

public class MaterialsRepository : ICrudRepository<Material> {

    private readonly ParamsDbContext _db;

    public MaterialsRepository(ParamsDbContext db) {
        _db = db;
    }

    public IQueryable<Material> GetAll() {
        return _db.Materials;
    }

    public async Task<Material?> GetByIdAsync(int id) {
        var en = await _db.Materials.FindAsync(id);
        return en;
    }

    public int GetLastestId() {
        var en = _db.Materials.Max(x => x.Id);
        return en;
    }

    public async Task CreateAsync(Material en) {
      await _db.Materials.AddAsync(en);
      ;
    }

    public void Update(Material en) {
        var obj = _db.Materials.First(x => x.Id == en.Id);
    
        obj.Name = en.Name;
        
    }

    public void DeleteById(int id) {
        var obj = _db.Materials.First(x=> x.Id == id);
        _db.Materials.Remove(obj);
        
    }

    public Task SaveChangesAsync() {
        return _db.SaveChangesAsync();
    }

    
}

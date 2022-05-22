using AutoMapper;
using Wond.Params.Dtos;
using Wond.Params.Models;
using Wond.Params.Repositories;

namespace Wond.Params.Services;

public class MaterialsService : ICrudService<MaterialDto> {

    public readonly ICrudRepository<Material> _repo;
    public readonly IMapper _mapper;

    public MaterialsService(ICrudRepository<Material> repo, IMapper mapper) {
        _repo = repo;
        _mapper = mapper;
    }

    public List<MaterialDto> GetList() {
        var list = _repo.GetAll().ToList();
        return _mapper.Map<List<MaterialDto>>(list);
    }

    public async Task<MaterialDto> GetByIdAsync(int id) {
        var en = await _repo.GetByIdAsync(id);
        return _mapper.Map<MaterialDto>(en);
    }

    public async Task<MaterialDto> CreateAsync(MaterialDto dto) { 
        
        await _repo.CreateAsync(_mapper.Map<Material>(dto));

        await _repo.SaveChangesAsync();    

        dto.Id = _repo.GetLastestId();

        return dto;
    }

    public async Task<MaterialDto> UpdateASync(MaterialDto dto) {
        _repo.Update(_mapper.Map<Material>(dto));
        await _repo.SaveChangesAsync();
        return await GetByIdAsync(dto.Id);
    }

    public async Task DeleteByIdAsync(int id) {
        _repo.DeleteById(id);
        await _repo.SaveChangesAsync();
    }
}

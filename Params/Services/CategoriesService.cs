using AutoMapper;
using Wond.Params.Dtos;
using Wond.Params.Models;
using Wond.Params.Repositories;

namespace Wond.Params.Services;

public class CategoriesService : ICrudService<CategoryDto> {

    public readonly ICrudRepository<Category> _repo;
    public readonly IMapper _mapper;

    public CategoriesService(ICrudRepository<Category> repo, IMapper mapper) {
        _repo = repo;
        _mapper = mapper;
    }

    public List<CategoryDto> GetList() {
        var list = _repo.GetAll().ToList();
        return _mapper.Map<List<CategoryDto>>(list);
    }

    public async Task<CategoryDto> GetByIdAsync(int id) {
        var en = await _repo.GetByIdAsync(id);
        return _mapper.Map<CategoryDto>(en);
    }

    public async Task<CategoryDto> CreateAsync(CategoryDto dto) { 
        
        await _repo.CreateAsync(_mapper.Map<Category>(dto));

        await _repo.SaveChangesAsync();    

        dto.Id = _repo.GetLastestId();

        return dto;
    }

    public async Task<CategoryDto> UpdateASync(CategoryDto dto) {
        _repo.Update(_mapper.Map<Category>(dto));
        await _repo.SaveChangesAsync();
        return await GetByIdAsync(dto.Id);
    }

    public async Task DeleteByIdAsync(int id) {
        _repo.DeleteById(id);
        await _repo.SaveChangesAsync();
    }
}

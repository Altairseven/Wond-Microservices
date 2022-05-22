using AutoMapper;
using Wond.Params.Dtos;
using Wond.Params.Models;
using Wond.Params.Repositories;

namespace Wond.Params.Services;

public class SizesService : ICrudService<SizeDto> {

    public readonly ICrudRepository<Size> _repo;
    public readonly IMapper _mapper;

    public SizesService(ICrudRepository<Size> repo, IMapper mapper) {
        _repo = repo;
        _mapper = mapper;
    }

    public List<SizeDto> GetList() {
        var list = _repo.GetAll().ToList();
        return _mapper.Map<List<SizeDto>>(list);
    }

    public async Task<SizeDto> GetByIdAsync(int id) {
        var en = await _repo.GetByIdAsync(id);
        return _mapper.Map<SizeDto>(en);
    }

    public async Task<SizeDto> CreateAsync(SizeDto dto) { 
        
        await _repo.CreateAsync(_mapper.Map<Size>(dto));

        await _repo.SaveChangesAsync();    

        dto.Id = _repo.GetLastestId();

        return dto;
    }

    public async Task<SizeDto> UpdateASync(SizeDto dto) {
        _repo.Update(_mapper.Map<Size>(dto));
        await _repo.SaveChangesAsync();
        return await GetByIdAsync(dto.Id);
    }

    public async Task DeleteByIdAsync(int id) {
        _repo.DeleteById(id);
        await _repo.SaveChangesAsync();
    }
}

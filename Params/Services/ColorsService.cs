using AutoMapper;
using Wond.Params.Dtos;
using Wond.Params.Models;
using Wond.Params.Repositories;

namespace Wond.Params.Services;

public class ColorsService : ICrudService<ColorDto> {

    public readonly ICrudRepository<Color> _repo;
    public readonly IMapper _mapper;

    public ColorsService(ICrudRepository<Color> repo, IMapper mapper) {
        _repo = repo;
        _mapper = mapper;
    }

    public List<ColorDto> GetList() {
        var list = _repo.GetAll().ToList();
        return _mapper.Map<List<ColorDto>>(list);
    }

    public async Task<ColorDto> GetByIdAsync(int id) {
        var en = await _repo.GetByIdAsync(id);
        return _mapper.Map<ColorDto>(en);
    }

    public async Task<ColorDto> CreateAsync(ColorDto dto) { 
        
        await _repo.CreateAsync(_mapper.Map<Color>(dto));

        await _repo.SaveChangesAsync();    

        dto.Id = _repo.GetLastestId();

        return dto;
    }

    public async Task<ColorDto> UpdateASync(ColorDto dto) {
        _repo.Update(_mapper.Map<Color>(dto));
        await _repo.SaveChangesAsync();
        return await GetByIdAsync(dto.Id);
    }

    public async Task DeleteByIdAsync(int id) {
        _repo.DeleteById(id);
        await _repo.SaveChangesAsync();
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wond.Params.Dtos;
using Wond.Params.Services;
using Wond.Shared.Dtos;

namespace Wond.Params.Controllers;

public class SizeController : BaseApiController {

    private readonly ICrudService<SizeDto> _service;

    public SizeController(ICrudService<SizeDto> service) {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetList() {
        var list = _service.GetList();
        return Ok(list);
    }

    [HttpGet("{id}", Name = "GetSizeById")]
    [ActionName("GetSizeById")]
    public async Task<IActionResult> GetSizeById(int id) {
        try {
            var en = await _service.GetByIdAsync(id);
            if (en == null)
                return NotFound("Object does not exists");
            return Ok(en);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(SizeDto dto) {
        try {
            var en = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetSizeById),new { en.Id }, en);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(SizeDto dto) {
        try {
            var en = await _service.UpdateASync(dto);
            return Ok(en);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id) {
        try {
            await _service.DeleteByIdAsync(id);
            return Ok();
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }
}

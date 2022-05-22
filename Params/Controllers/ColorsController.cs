using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wond.Params.Dtos;
using Wond.Params.Services;
using Wond.Shared.Dtos;

namespace Wond.Params.Controllers;

public class ColorsController : BaseApiController {

    private readonly ICrudService<ColorDto> _service;
    private readonly ILogger<DefaultController> _logger;


    public ColorsController(ICrudService<ColorDto> service, ILogger<DefaultController> logger) {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetList() {
        var list = _service.GetList();
        return Ok(list);
    }

    [HttpGet("{id}", Name = "GetColorById")]
    [ActionName("GetColorById")]
    public async Task<IActionResult> GetColorById(int id) {
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
    
    public async Task<IActionResult> Create(ColorDto dto) {
        try {
            var en = await _service.CreateAsync(dto);

            _logger.LogInformation("ColorAdded", en.ToJson());

            return CreatedAtAction(nameof(GetColorById),new { en.Id }, en);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(ColorDto dto) {
        try {
            var en = await _service.UpdateASync(dto);
            _logger.LogInformation("ColorEdited", en.ToJson());
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

            _logger.LogInformation("ColorRemoved", id);
            return Ok();
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }
}

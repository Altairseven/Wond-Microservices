using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Wond.Params.Dtos;

public class ColorDto {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;



    public string ToJson() {
        return JsonSerializer.Serialize(this);
    }
}

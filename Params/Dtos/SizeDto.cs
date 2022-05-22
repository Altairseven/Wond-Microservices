using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wond.Params.Dtos;

public class SizeDto {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

}

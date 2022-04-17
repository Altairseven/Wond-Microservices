using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wond.Shared.Dtos;

public class ProductColor {
    public int Id { get; set; }
    public string? Nombre { get; set; }

    public ProductColor() {

    }

    public ProductColor(int Id, string? Nombre) {
        this.Id = Id;
        this.Nombre = Nombre;   
    }

}

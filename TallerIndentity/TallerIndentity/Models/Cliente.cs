using System;
using System.Collections.Generic;

namespace TallerIndentity.Models;

public partial class Cliente
{
    public int IdClientes { get; set; }

    public int Cedula { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}

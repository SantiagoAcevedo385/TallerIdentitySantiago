using System;
using System.Collections.Generic;

namespace TallerIndentity.Models;

public partial class Producto
{
    public int IdProductos { get; set; }

    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public double Precio { get; set; }

    public int Cantidad { get; set; }

    public virtual ICollection<DetallesVenta> DetallesVenta { get; set; } = new List<DetallesVenta>();
}

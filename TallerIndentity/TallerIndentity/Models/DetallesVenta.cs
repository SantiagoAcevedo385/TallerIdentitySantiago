using System;
using System.Collections.Generic;

namespace TallerIndentity.Models;

public partial class DetallesVenta
{
    public int DetalleVentaId { get; set; }

    public int VentaId { get; set; }

    public int ProductoId { get; set; }

    public int Cantidad { get; set; }

    public decimal Valor { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual Venta Venta { get; set; } = null!;
}

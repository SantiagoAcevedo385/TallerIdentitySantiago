using System;
using System.Collections.Generic;

namespace TallerIndentity.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public DateTime FechaVenta { get; set; }

    public int ClienteId { get; set; }

    public decimal Total { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<DetallesVenta> DetallesVenta { get; set; } = new List<DetallesVenta>();
}

using System;
using System.Collections.Generic;

namespace Warehouse_Management_System.Models;

public partial class PurchaseOrder
{
    public int PurchaseOrderId { get; set; }

    public int? SupplierId { get; set; }

    public int? UserId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

    public virtual Supplier? Supplier { get; set; }

    public virtual User? User { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JoePizzaApp.Areas.Identity.Data;

[Index("OrderId", Name = "IX_OrderDetails_OrderId")]
[Index("PizzaId", Name = "IX_OrderDetails_PizzaId")]
public partial class OrderDetail
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int PizzaId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TotalPrice { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderDetails")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("PizzaId")]
    [InverseProperty("OrderDetails")]
    public virtual Pizza Pizza { get; set; } = null!;
}

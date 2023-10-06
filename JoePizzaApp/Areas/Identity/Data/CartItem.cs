using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JoePizzaApp.Areas.Identity.Data;

[Index("CartId", Name = "IX_CartItems_CartId")]
[Index("PizzaId", Name = "IX_CartItems_PizzaId")]
public partial class CartItem
{
    [Key]
    public int CartItemId { get; set; }

    public int PizzaId { get; set; }

    public int Quantity { get; set; }

    public int CartId { get; set; }

    [ForeignKey("CartId")]
    [InverseProperty("CartItems")]
    public virtual Cart Cart { get; set; } = null!;

    [ForeignKey("PizzaId")]
    [InverseProperty("CartItems")]
    public virtual Pizza Pizza { get; set; } = null!;
}

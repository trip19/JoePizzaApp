using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JoePizzaApp.Areas.Identity.Data;

[Index("UserId", Name = "IX_Carts_UserId")]
public partial class Cart
{
    [Key]
    public int CartId { get; set; }

    public string UserId { get; set; } = null!;

    [InverseProperty("Cart")]
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    [ForeignKey("UserId")]
    [InverseProperty("Carts")]
    public virtual AspNetUser User { get; set; } = null!;
}

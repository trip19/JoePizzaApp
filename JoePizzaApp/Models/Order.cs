using JoePizzaApp.Models;
using System;
using System.Collections.Generic;

namespace JoePizzaApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

        // Navigation property to link orders to their details
        public List<OrderDetail> OrderDetails { get; set; }
    }
}

// OrderDetail.cs
namespace JoePizzaApp.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PizzaId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

        // Navigation properties to link order details to their order and pizza
        public Order Order { get; set; }
        public Pizza Pizza { get; set; }
    }
}
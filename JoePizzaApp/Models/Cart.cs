using Microsoft.AspNetCore.Identity;

namespace JoePizzaApp.Models
{
    public class Cart
    {

        public int CartId { get; set; }
        public string UserId { get; set; }

        public IdentityUser User { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

    }

    public class CartItem
    {
        public int CartItemId { get; set; }

        public int PizzaId { get; set; } // Changed from LaptopId

        public int Quantity { get; set; }

        // Navigation property to link a cart item to a pizza
        public Pizza Pizza { get; set; }

        // Reference to the cart that contains this item
        public int CartId { get; set; }

        // Navigation property to link a cart item to its parent cart
        public Cart Cart { get; set; }
    }

    /*public class Order
    {
        public int Id { get; set; }
        public List<CartItem> Items { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
    }*/
}
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Dish
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
    }

    public class OrderItem
    {
        public Dish SelectesDish { get; set; }

        public int Quantity { get; set; }
    }


    public class GuestSession
    {
        public string Id { get; set; }
        public string Type { get; set; }

        public string Name { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public bool IsSubmitted { get; set; }
    


    public decimal GetTotal()
        {
            decimal total = 0;

            foreach (var item in Items)
            {
                if (item.SelectesDish != null)
                {
                    total += item.SelectesDish.Price * item.Quantity;
                }
            }
            return total;
        }
    }

    public class ConsolidatedItem
    {
        public string DishId { get; set; }

        public int Quantity { get; set; }
    }




}

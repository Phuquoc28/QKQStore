using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QKQStore.Models
{
    public class Mathangmua
    {
        private QKQStoreEntities db = new QKQStoreEntities();
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice()
        {
            return Quantity * Price;
        }


        public Mathangmua(int ProductId)
        {
            this.ProductId = ProductId;

            var item  = db.Products.Single(s => s.Id == this.ProductId);

            this.Title = item.Title;
            this.Image = item.Image;
            this.Discount = item.Discount ?? 0;
            this.Quantity = 1;
            this.Price = decimal.Parse(item.Price.ToString());

        }
    }

}
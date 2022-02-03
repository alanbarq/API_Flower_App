using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiFlowerTwo.Controllers
{
    public class ProductsFavs
    {
        public string Code_product { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int ID_product { get; set; }
        public int ID { get; set; }
        public int Quantity { get; set; }

        public int User { get; set; }


        public ProductsFavs(string code_product, string name, double price, string description, int id_product, int id, int quantity, int user)
        {
            this.Code_product = code_product;
            this.Name = name;
            this.Price = price;
            this.Description = description;
            this.ID_product= id_product;
            this.ID = id;
            this.Quantity = quantity;
            this.User = user;
        }
    }
}
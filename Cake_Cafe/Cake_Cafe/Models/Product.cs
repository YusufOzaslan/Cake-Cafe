using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cake_Cafe.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Photo { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")] public Category Category { get; set; }
    }
}

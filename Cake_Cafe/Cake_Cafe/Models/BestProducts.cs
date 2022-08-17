using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cake_Cafe.Models
{
    public class BestProducts
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")] public Product Product { get; set; }
    }
}

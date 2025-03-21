using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.Objects.Product
{
    public class Product
    {
        [Key] 
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public int StockAmount { get; set; }
    }
}

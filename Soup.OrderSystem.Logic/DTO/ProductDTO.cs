using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.Logic.DTO
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        
    }
}

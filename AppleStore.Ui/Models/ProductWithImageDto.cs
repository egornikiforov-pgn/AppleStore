using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AppleStore.Ui.Models
{
    public class ProductWithImageDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
    }
}
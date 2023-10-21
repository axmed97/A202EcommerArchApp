using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Picture : IEntity
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public int ProductId { get; set; }
        public List<Product> Product { get; set; }
    }
}

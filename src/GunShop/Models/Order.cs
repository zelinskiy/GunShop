using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateTime { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}

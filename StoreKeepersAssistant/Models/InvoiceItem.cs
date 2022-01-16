using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Item Item { get; set; }
        public double Qty { get; set; } = 0.0;
    }
}

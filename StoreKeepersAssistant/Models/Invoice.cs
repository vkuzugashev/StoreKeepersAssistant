using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime InvoiceTime { get; set; } = DateTime.Now;
        public string InvoceNumber { get; set; }
        public virtual Storage FromStorage { get; set; }
        public virtual Storage ToStorage { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }

    }
}

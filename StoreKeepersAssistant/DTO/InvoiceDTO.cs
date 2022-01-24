using System;
using System.Collections.Generic;

namespace StoreKeepersAssistant.ViewModels
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public DateTime? InvoiceTime { get; set; }
        public string InvoiceNumber { get; set; }
        public StorageDTO FromStorage { get; set; }
        public StorageDTO ToStorage { get; set; }
        //public List<InvoiceItemViewModel> Items { get; set; }
    }
}

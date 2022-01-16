using System;
using System.Collections.Generic;

namespace StoreKeepersAssistant.ViewModels
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public DateTime? InvoiceTime { get; set; }
        public string InvoiceNumber { get; set; }
        public StorageViewModel FromStorage { get; set; }
        public StorageViewModel ToStorage { get; set; }
        //public List<InvoiceItemViewModel> Items { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using StoreKeepersAssistant.Repositories;

namespace StoreKeepersAssistant.Services
{
    public class InvoiceItemService : IInvoiceItemService
    {
        IInvoiceItemRepository rep;
        public InvoiceItemService(IInvoiceItemRepository repository) => rep = repository;

        public async Task<List<ViewModels.InvoiceItemDTO>> GetByInvoiceIdAsync(int invoiceId)
        {
            return await rep.GetByInvoiceIdAsync(invoiceId);
        }

        public async Task<ViewModels.InvoiceItemDTO> AddAsync(ViewModels.InvoiceItemDTO invoiceItem)
        {
            return await rep.AddAsync(invoiceItem);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await rep.DeleteAsync(id);
        }

        public async Task<ViewModels.InvoiceItemDTO> UpdateAsync(ViewModels.InvoiceItemDTO invoiceItem)
        {           
            return await rep.UpdateAsync(invoiceItem);
        }


    }
}

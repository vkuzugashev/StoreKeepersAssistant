using StoreKeepersAssistant.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Services
{
    public interface IInvoiceItemService
    {
        Task<List<InvoiceItemDTO>> GetByInvoiceIdAsync(int invoiceId);
        Task<InvoiceItemDTO> AddAsync(InvoiceItemDTO invoiceItem);
        Task<int> DeleteAsync(int id);
        Task<InvoiceItemDTO> UpdateAsync(InvoiceItemDTO invoiceItem);
    }
}
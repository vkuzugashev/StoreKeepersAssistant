using StoreKeepersAssistant.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Repositories
{
    public interface IInvoiceItemRepository
    {
        Task<InvoiceItemDTO> AddAsync(InvoiceItemDTO invoiceItemViewModel);
        Task<int> DeleteAsync(int id);
        Task<InvoiceItemDTO> UpdateAsync(InvoiceItemDTO invoiceItemViewModel);
        Task<List<InvoiceItemDTO>> GetByInvoiceIdAsync(int invoiceId);
    }
}
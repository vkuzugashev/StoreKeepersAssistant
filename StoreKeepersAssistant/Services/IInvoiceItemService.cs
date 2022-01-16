using StoreKeepersAssistant.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Services
{
    public interface IInvoiceItemService
    {
        Task<List<InvoiceItemViewModel>> GetByInvoiceIdAsync(int invoiceId);
        Task<InvoiceItemViewModel> AddAsync(InvoiceItemViewModel invoiceItem);
        Task<int> DeleteAsync(int id);
        Task<InvoiceItemViewModel> UpdateAsync(InvoiceItemViewModel invoiceItem);
    }
}
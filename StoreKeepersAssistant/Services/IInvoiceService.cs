using StoreKeepersAssistant.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceViewModel> GetByIdAsync(int id);
        Task<InvoiceViewModel> AddAsync(InvoiceViewModel invoiceViewModel);
        Task<int> DeleteAsync(int id);
        Task<InvoiceViewModel> UpdateAsync(InvoiceViewModel invoiceViewModel);
    }
}
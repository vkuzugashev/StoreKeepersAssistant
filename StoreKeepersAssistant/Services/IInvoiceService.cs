using StoreKeepersAssistant.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceDTO> GetByIdAsync(int id);
        Task<InvoiceDTO> AddAsync(InvoiceDTO invoiceViewModel);
        Task<int> DeleteAsync(int id);
        Task<InvoiceDTO> UpdateAsync(InvoiceDTO invoiceViewModel);
    }
}
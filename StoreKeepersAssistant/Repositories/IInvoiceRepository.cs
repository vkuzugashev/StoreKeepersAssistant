using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Repositories
{
    public interface IInvoiceRepository
    {
        Task<InvoiceDTO> AddAsync(InvoiceDTO invoiceViewModel);
        Task<int> DeleteAsync(int id);
        Task<List<InvoiceDTO>> GetAllAsync();
        Task<InvoiceDTO> GetByIdAsync(int id);
        Task<InvoiceDTO> UpdateAsync(InvoiceDTO invoiceViewModel);
    }
}
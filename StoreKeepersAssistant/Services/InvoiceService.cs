using StoreKeepersAssistant.Repositories;
using StoreKeepersAssistant.ViewModels;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Services
{
    public class InvoiceService : IInvoiceService
    {
        IInvoiceRepository invoiceRepository;
        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
        }

        public async Task<InvoiceDTO> GetByIdAsync(int id)
        {
            return await invoiceRepository.GetByIdAsync(id);
        }

        public async Task<InvoiceDTO> AddAsync(InvoiceDTO invoiceViewModel)
        {
            return await invoiceRepository.AddAsync(invoiceViewModel);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await invoiceRepository.DeleteAsync(id);
        }

        public async Task<InvoiceDTO> UpdateAsync(InvoiceDTO invoiceViewModel)
        {
            return await invoiceRepository.UpdateAsync(invoiceViewModel);
        }

    }
}

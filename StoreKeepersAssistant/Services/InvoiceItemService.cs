using Microsoft.EntityFrameworkCore;
using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace StoreKeepersAssistant.Services
{
    public class InvoiceItemService : IInvoiceItemService
    {
        StorageContext db;
        public InvoiceItemService(StorageContext context)
        {
            db = context;
        }

        public async Task<List<InvoiceItemViewModel>> GetByInvoiceIdAsync(int invoiceId)
        {
            var query = from o in db.InvoiceItems where o.Invoice.Id == invoiceId
                        select new InvoiceItemViewModel { Id = o.Id, InvoiceId = o.Invoice.Id, ItemId = o.Item.Id };

            return await query.ToListAsync();
        }

        public async Task<InvoiceItemViewModel> AddAsync(InvoiceItemViewModel invoiceItem)
        {
            var invoice = db.Invoices.Find(invoiceItem.InvoiceId);
            var item = db.Items.Find(invoiceItem.ItemId);

            var _invoiceItem = new InvoiceItem { Invoice = invoice, Item = item, Qty = invoiceItem.Qty };

            await db.InvoiceItems.AddAsync(_invoiceItem);
            await db.SaveChangesAsync();

            invoiceItem.Id = _invoiceItem.Id;

            return invoiceItem;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var invoiceItem = new InvoiceItem { Id = id };
            db.Entry(invoiceItem).State = EntityState.Deleted;
            return await db.SaveChangesAsync();
        }

        public async Task<InvoiceItemViewModel> UpdateAsync(InvoiceItemViewModel invoiceItem)
        {
            var _invoiceItem = db.InvoiceItems.Find(invoiceItem.Id);
            if (_invoiceItem != null)
            {
                _invoiceItem.Qty = invoiceItem.Qty;
                await db.SaveChangesAsync();
            }
            return invoiceItem;
        }


    }
}

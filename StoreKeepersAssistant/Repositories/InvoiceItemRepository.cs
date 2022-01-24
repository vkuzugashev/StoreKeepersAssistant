using Microsoft.EntityFrameworkCore;
using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Repositories
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        StorageContext db;
        ICustomDbContextFactory<StorageContext> customDbContextFactory;
        public InvoiceItemRepository(StorageContext context, ICustomDbContextFactory<StorageContext> customDbContextFactory)
        {
            db = context;
            this.customDbContextFactory = customDbContextFactory;
        }

        public async Task<ViewModels.InvoiceItemDTO> AddAsync(ViewModels.InvoiceItemDTO invoiceItemViewModel)
        {
            using (var db1 = customDbContextFactory.CreateDbContext(db.Database.GetConnectionString()) )
            {
                var taskInvoice = db.Invoices.FirstOrDefaultAsync(x => x.Id == invoiceItemViewModel.InvoiceId);
                var taskItem = db1.Items.FirstOrDefaultAsync(x => x.Id == invoiceItemViewModel.ItemId);
                
                await Task.WhenAll(taskInvoice, taskItem);
                
                var invoice = taskInvoice.Result;
                var item = taskItem.Result;

                db.AttachRange(item);

                var _invoiceItem = new Models.InvoiceItem { Invoice = invoice, Item = item, Qty = invoiceItemViewModel.Qty };

                await db.InvoiceItems.AddAsync(_invoiceItem);
                await db.SaveChangesAsync();
                invoiceItemViewModel.Id = _invoiceItem.Id;
            }
            return invoiceItemViewModel;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var invoiceItem = new InvoiceItem { Id = id };
            db.Entry(invoiceItem).State = EntityState.Deleted;
            return await db.SaveChangesAsync();
        }

        public async Task<ViewModels.InvoiceItemDTO> UpdateAsync(ViewModels.InvoiceItemDTO invoiceItemViewModel)
        {
            var id = invoiceItemViewModel.Id;
            var invoiceId = invoiceItemViewModel.InvoiceId;
            var itemId = invoiceItemViewModel.ItemId ?? "";
            
            using (var db1 = customDbContextFactory.CreateDbContext(db.Database.GetConnectionString()))
            using (var db2 = customDbContextFactory.CreateDbContext(db.Database.GetConnectionString()))
            {
                var taskInvoiceItem = db.InvoiceItems.FirstOrDefaultAsync(x => x.Id == id);
                var taskInvoice = db1.Invoices.FirstOrDefaultAsync(x => x.Id == invoiceId);
                var taskItem = db2.Items.FirstOrDefaultAsync(x => x.Id == itemId);

                await Task.WhenAll(taskInvoiceItem, taskInvoice, taskItem);

                var invoiceItem = taskInvoiceItem.Result;
                var invoice = taskInvoice.Result;
                var item = taskItem.Result;

                db.AddRange(invoice, item);

                if (invoiceItem != null)
                {

                    invoiceItem.Invoice = invoice;
                    invoiceItem.Item = item;
                    invoiceItem.Qty = invoiceItemViewModel.Qty;

                    await db.SaveChangesAsync();

                    return invoiceItemViewModel;
                }
                else
                    return null;
            }
        }

        public async Task<List<ViewModels.InvoiceItemDTO>> GetByInvoiceIdAsync(int invoiceId)
        {
            var query = from o in db.InvoiceItems
                        where o.Invoice.Id == invoiceId
                        select new ViewModels.InvoiceItemDTO { Id = o.Id, InvoiceId = o.Invoice.Id, ItemId = o.Item.Id };

            return await query.ToListAsync();
        }
    }
}
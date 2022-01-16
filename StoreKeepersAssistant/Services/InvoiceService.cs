using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Services
{
    public class InvoiceService : IInvoiceService
    {
        StorageContext db;
        public InvoiceService(StorageContext context)
        {
            db = context;
        }

        public async Task<InvoiceViewModel> GetByIdAsync(int id)
        {
            var query = from o in db.Invoices where o.Id == id
                        select new InvoiceViewModel {
                            Id = o.Id,
                            InvoiceTime = o.InvoiceTime,
                            InvoiceNumber = o.InvoceNumber,
                            FromStorage = new StorageViewModel { Id = o.FromStorage.Id, Name = o.FromStorage.Name },
                            ToStorage = new StorageViewModel { Id = o.ToStorage.Id, Name = o.ToStorage.Name }
                        };
            return await query.FirstOrDefaultAsync();
        }

        public async Task<InvoiceViewModel> AddAsync(InvoiceViewModel invoiceViewModel)
        {
            var fromStorage = await db.Storages.FindAsync(invoiceViewModel.FromStorage?.Id);
            var toStorage = await db.Storages.FindAsync(invoiceViewModel.ToStorage?.Id);

            var invoice = new Invoice { 
                InvoceNumber = invoiceViewModel.InvoiceNumber, 
                InvoiceTime = invoiceViewModel.InvoiceTime??DateTime.Now, 
                FromStorage = fromStorage, 
                ToStorage = toStorage 
            };
            
            await db.Invoices.AddAsync(invoice);
            await db.SaveChangesAsync();
            
            invoiceViewModel.Id = invoice.Id;

            return invoiceViewModel;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var invoice = new Invoice { Id = id };
            db.Entry(invoice).State = EntityState.Deleted;
            return await db.SaveChangesAsync();
        }

        public async Task<InvoiceViewModel> UpdateAsync(InvoiceViewModel invoiceViewModel)
        {
            var invoice = db.Invoices.Find(invoiceViewModel.Id);
            if (invoice != null)
            {
                var fromStorage = await db.Storages.FindAsync(invoiceViewModel.FromStorage.Id);
                var toStorage = await db.Storages.FindAsync(invoiceViewModel.ToStorage.Id);

                invoice.InvoceNumber = invoiceViewModel.InvoiceNumber;
                invoice.InvoiceTime = invoiceViewModel.InvoiceTime??DateTime.Now;
                invoice.FromStorage = fromStorage;
                invoice.ToStorage = toStorage;

                await db.SaveChangesAsync();
            }
            return invoiceViewModel;
        }

    }
}

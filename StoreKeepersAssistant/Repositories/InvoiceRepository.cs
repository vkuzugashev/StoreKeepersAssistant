using Microsoft.EntityFrameworkCore;
using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        StorageContext db;
        ICustomDbContextFactory<StorageContext> customDbContextFactory;
        public InvoiceRepository(StorageContext context, ICustomDbContextFactory<StorageContext> customDbContextFactory)
        {
            db = context;
            this.customDbContextFactory = customDbContextFactory;
        }
        public async Task<InvoiceDTO> GetByIdAsync(int id)
        {
            var query = from o in db.Invoices
                        where o.Id == id
                        select new InvoiceDTO
                        {
                            Id = o.Id,
                            InvoiceTime = o.InvoiceTime,
                            InvoiceNumber = o.InvoceNumber,
                            FromStorage = new StorageDTO { Id = o.FromStorage.Id, Name = o.FromStorage.Name },
                            ToStorage = new StorageDTO { Id = o.ToStorage.Id, Name = o.ToStorage.Name }
                        };
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<InvoiceDTO>> GetAllAsync()
        {
            var query = from o in db.Invoices
                        select new InvoiceDTO
                        {
                            Id = o.Id,
                            InvoiceTime = o.InvoiceTime,
                            InvoiceNumber = o.InvoceNumber,
                            FromStorage = new StorageDTO { Id = o.FromStorage.Id, Name = o.FromStorage.Name },
                            ToStorage = new StorageDTO { Id = o.ToStorage.Id, Name = o.ToStorage.Name }
                        };
            return await query.ToListAsync();
        }
        public async Task<InvoiceDTO> AddAsync(InvoiceDTO  invoiceViewModel)
        {
            var fromStorageId = invoiceViewModel.FromStorage?.Id ?? "";
            var toStorageId = invoiceViewModel.ToStorage?.Id ?? "";
            
            using (var db1 = customDbContextFactory.CreateDbContext(db.Database.GetConnectionString()))
            {
                var taskFromStorage = db.Storages.FirstOrDefaultAsync(o => o.Id == fromStorageId);
                var taskToStorage = db1.Storages.FirstOrDefaultAsync(o => o.Id == toStorageId);

                await Task.WhenAll(taskFromStorage, taskToStorage);

                var fromStorage = taskFromStorage.Result;
                var toStorage = taskToStorage.Result;
                
                db.AttachRange(toStorage);
                
                var invoice = new Invoice
                {
                    InvoceNumber = invoiceViewModel.InvoiceNumber,
                    InvoiceTime = invoiceViewModel.InvoiceTime ?? DateTime.Now,
                    FromStorage = fromStorage,
                    ToStorage = toStorage
                };

                db.Add(invoice);
                await db.SaveChangesAsync();

                invoiceViewModel.Id = invoice.Id;
            }
            return invoiceViewModel;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var invoice = new Invoice { Id = id };
            db.Entry(invoice).State = EntityState.Deleted;
            return await db.SaveChangesAsync();
        }

        public async Task<InvoiceDTO> UpdateAsync(InvoiceDTO invoiceViewModel)
        {
            var invoiceId = invoiceViewModel.Id;
            var fromStorageId = invoiceViewModel.FromStorage?.Id ?? "";
            var toStorageId = invoiceViewModel.ToStorage?.Id ?? "";

            using (var db1 = customDbContextFactory.CreateDbContext(db.Database.GetConnectionString()))
            using (var db2 = customDbContextFactory.CreateDbContext(db.Database.GetConnectionString()))
            {
                var taskInvoice = db.Invoices.FirstOrDefaultAsync(x => x.Id == invoiceId);
                var taskFromStorage = db1.Storages.FirstOrDefaultAsync(x => x.Id == fromStorageId);
                var taskToStorage = db2.Storages.FirstOrDefaultAsync(x => x.Id == toStorageId);

                await Task.WhenAll(taskInvoice, taskFromStorage, taskToStorage);

                var invoice = taskInvoice.Result;
                var fromStorage = taskFromStorage.Result;
                var toStorage = taskToStorage.Result;

                db.AttachRange(fromStorage, toStorage);

                if (invoice != null)
                {

                    invoice.InvoceNumber = invoiceViewModel.InvoiceNumber;
                    invoice.InvoiceTime = invoiceViewModel.InvoiceTime ?? DateTime.Now;
                    invoice.FromStorage = fromStorage;
                    invoice.ToStorage = toStorage;

                    await db.SaveChangesAsync();

                    invoiceViewModel.InvoiceNumber = invoice.InvoceNumber;
                    invoiceViewModel.InvoiceTime = invoice.InvoiceTime;

                    return invoiceViewModel;
                }
                else
                    return null;
            }
        }


    }
}

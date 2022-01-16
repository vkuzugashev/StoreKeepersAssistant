using Microsoft.EntityFrameworkCore;
using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Services
{
    public class InventoryService : IInventoryService
    {
        StorageContext db;
        public InventoryService(StorageContext context)
        {
            db = context;
        }
        public async Task<List<InvoiceViewModel>> GetAllMoviesAsync()
        {
            var query = from o in db.Invoices
                        select new InvoiceViewModel
                        {
                            Id = o.Id,
                            InvoiceTime = o.InvoiceTime,
                            InvoiceNumber = o.InvoceNumber,
                            FromStorage = new StorageViewModel { Id = o.FromStorage.Id, Name = o.FromStorage.Name },
                            ToStorage = new StorageViewModel { Id = o.ToStorage.Id, Name = o.ToStorage.Name }
                        };

            return await query.ToListAsync();
        }


        public List<InventoryViewModel> GetRemainsOnDateAsync(string storageId, DateTime searchTime)
        {
            return null;
        }
    }
}

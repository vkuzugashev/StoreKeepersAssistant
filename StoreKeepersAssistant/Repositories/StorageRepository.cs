using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StoreKeepersAssistant.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        StorageContext db;
        public StorageRepository(StorageContext context) => db = context;

        public async Task<List<StorageDTO>> GetAllAsync()
        {
            var query = from o in db.Storages
                        select new StorageDTO { Id = o.Id, Name = o.Name };

            return await query.ToListAsync();
        }

        public async Task<StorageDTO> AddAsync(StorageDTO storageViewModel)
        {
            var storage = new Storage
            {
                Id = storageViewModel.Id,
                Name = storageViewModel.Name
            };

            db.Add(storage);
            await db.SaveChangesAsync();
            
            storageViewModel.Id = storage.Id;
            
            return storageViewModel;
        }

    }
}

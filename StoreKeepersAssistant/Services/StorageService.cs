using Microsoft.EntityFrameworkCore;
using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Services
{
    public class StorageService : IStorageService
    {
        StorageContext db;
        public StorageService(StorageContext context)
        {
            db = context;
        }
        public async Task<List<StorageViewModel>> GetAllAsync()
        {
            var query = from o in db.Storages
                        select new StorageViewModel { Id = o.Id, Name = o.Name };
            return await query.ToListAsync();
        }
    }
}

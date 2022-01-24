using Microsoft.EntityFrameworkCore;
using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Repositories
{
    public class ItemRepository : IItemRepository
    {
        StorageContext db;
        public ItemRepository(StorageContext context) => db = context;

        public async Task<List<ItemDTO>> GetAllAsync()
        {
            var query = from o in db.Items
                        select new ItemDTO { Id = o.Id, Name = o.Name };

            return await query.ToListAsync();
        }

    }
}

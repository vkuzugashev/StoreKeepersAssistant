using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StoreKeepersAssistant.Services
{
    public class ItemService : IItemService
    {

        StorageContext db;

        public ItemService(StorageContext context)
        {
            db = context;
        }
        public async Task<List<ItemViewModel>> GetAllAsync()
        {
            var query = from o in db.Items select new ItemViewModel { Id = o.Id, Name = o.Name };
            return await query.ToListAsync();
        }
    }
}

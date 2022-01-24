using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreKeepersAssistant.Repositories;

namespace StoreKeepersAssistant.Services
{
    public class ItemService : IItemService
    {

        IItemRepository rep;

        public ItemService(IItemRepository repository) => rep = repository;

        public async Task<IEnumerable<ItemDTO>> GetAllAsync()
        {
            return await rep.GetAllAsync();
        }
    }
}

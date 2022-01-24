using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Repositories
{
    public interface IItemRepository
    {
        Task<List<ItemDTO>> GetAllAsync();
    }
}
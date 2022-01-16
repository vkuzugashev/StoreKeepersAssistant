using StoreKeepersAssistant.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Services
{
    public interface IItemService
    {
        Task<List<ItemViewModel>> GetAllAsync();
    }
}
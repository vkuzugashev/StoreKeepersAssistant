using StoreKeepersAssistant.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Services
{
    public interface IInventoryService
    {
        List<InventoryViewModel> GetRemainsOnDateAsync(string storageId, DateTime searchTime);
        Task<List<InvoiceViewModel>> GetAllMoviesAsync();
    }
}
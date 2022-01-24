using StoreKeepersAssistant.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Services
{
    public interface IInventoryService
    {
        List<InventoryDTO> GetRemainsOnDateAsync(string storageId, DateTime searchTime);
        Task<List<InvoiceDTO>> GetAllMoviesAsync();
    }
}
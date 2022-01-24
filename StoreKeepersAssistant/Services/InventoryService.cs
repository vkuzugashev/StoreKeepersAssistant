using Microsoft.EntityFrameworkCore;
using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.Repositories;
using StoreKeepersAssistant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Services
{
    public class InventoryService : IInventoryService
    {
        IInvoiceRepository rep;
        public InventoryService(IInvoiceRepository repository) => rep = repository;

        public async Task<List<InvoiceDTO>> GetAllMoviesAsync()
        {
            return await rep.GetAllAsync();
        }
        public List<InventoryDTO> GetRemainsOnDateAsync(string storageId, DateTime searchTime)
        {
            return null;
        }
    }
}

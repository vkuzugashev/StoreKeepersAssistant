using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Repositories
{
    public interface IStorageRepository
    {
        Task<StorageDTO> AddAsync(StorageDTO storageViewModel);
        Task<List<StorageDTO>> GetAllAsync();
    }
}
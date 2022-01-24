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
    public class StorageService : IStorageService
    {
        IStorageRepository rep;
        public StorageService(IStorageRepository repository) => rep = repository;
        public async Task<IEnumerable<StorageDTO>> GetAllAsync() => await rep.GetAllAsync();

    }

}

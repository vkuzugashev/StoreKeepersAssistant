using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.Services;
using StoreKeepersAssistant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreKeepersAssistant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageController : ControllerBase
    {
        IStorageService _service;

        private readonly ILogger<StorageController> _logger;

        public StorageController(ILogger<StorageController> logger, IStorageService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<StorageDTO>> Get()
        {
            return await _service.GetAllAsync();
        }


    }
}

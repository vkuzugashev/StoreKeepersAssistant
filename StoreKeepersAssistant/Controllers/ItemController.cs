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
    public class ItemController : ControllerBase
    {
        IItemService _service;

        private readonly ILogger<ItemController> _logger;

        public ItemController(ILogger<ItemController> logger, IItemService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemViewModel>> Get()
        {
            return await _service.GetAllAsync();
        }


    }
}

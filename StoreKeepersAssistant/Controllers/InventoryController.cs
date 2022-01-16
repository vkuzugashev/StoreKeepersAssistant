using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class InventoryController : ControllerBase
    {
        IInventoryService _service;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger, IInventoryService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<InvoiceViewModel>> GetAllMovies()
        {
            return await _service.GetAllMoviesAsync();
        }


    }
}

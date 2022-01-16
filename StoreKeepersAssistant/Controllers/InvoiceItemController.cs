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
    public class InvoiceItemController : ControllerBase
    {
        IInvoiceItemService _service;

        private readonly ILogger<InvoiceItemController> _logger;

        public InvoiceItemController(ILogger<InvoiceItemController> logger, IInvoiceItemService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<InvoiceItemViewModel>> Get(int id)
        {
            return await _service.GetByInvoiceIdAsync(id);
        }

        [HttpGet]
        [Route("[action]")]
        public InvoiceItemViewModel Create()
        {
            return new InvoiceItemViewModel();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] InvoiceItemViewModel invoiceItem)
        {
            if (ModelState.IsValid)
                return new ObjectResult(await _service.AddAsync(invoiceItem));
            else
                return BadRequest();
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid && id != 0)
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            else
                return NotFound();
        }

    }
}

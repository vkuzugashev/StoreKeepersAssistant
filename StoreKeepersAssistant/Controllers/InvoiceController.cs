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
    public class InvoiceController : ControllerBase
    {
        IInvoiceService _service;

        private readonly ILogger<InvoiceController> _logger;

        public InvoiceController(ILogger<InvoiceController> logger, IInvoiceService invoceService)
        {
            _logger = logger;
            _service = invoceService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var invoice = await _service.GetByIdAsync(id);
            if (invoice != null)
                return Ok(invoice);
            else
                return NotFound();            
        }

        [HttpGet]
        [Route("[action]")]
        public InvoiceViewModel Create()
        {
            return new InvoiceViewModel();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] InvoiceViewModel invoice)
        {
            if (ModelState.IsValid)
                return Ok(await _service.AddAsync(invoice));
            else
                return BadRequest();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] InvoiceViewModel invoice)
        {
            if (ModelState.IsValid)
                return Ok(await _service.UpdateAsync(invoice));
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

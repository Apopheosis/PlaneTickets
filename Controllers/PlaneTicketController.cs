using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Json.More;
using Json.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using Models;
using Npgsql;
using Tickets.Filters;
using Microsoft.Extensions.Logging;
using MoviesApp.Middleware;
using Tickets.Services;

namespace Tickets.Controllers
{
    [ApiController]
    [Route("v1/")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class ItemApiController : ControllerBase
    {
        private readonly ItemContext _context;
        private readonly IService _service;
        private readonly ILogger<PlaneTicketController> _logger;
        public ItemApiController(ItemContext context, IService service, ILogger<PlaneTicketController> logger)
        {
            _context = context;
            _service = service;
            _logger = logger;
        }

        [HttpPost("process/sale")]
        public async Task<ActionResult> Sale(PlaneTicket ticket)
        {
            JsonSchemaValidation ValResult = new JsonSchemaValidation();
            if (System.Text.ASCIIEncoding.Unicode.GetByteCount(JsonConvert.SerializeObject(ticket)) >= 2000)
            {
                return StatusCode(413);
            }
            if (ValResult.JsonSchema(JsonConvert.SerializeObject(ticket), System.IO.File.ReadAllText("RefundJsonSchema.json")) ==false)
            {
                return StatusCode(400);
            }
            try
            {
                await _service.SaleTransaction(ticket);
            }
            catch (DbUpdateException exception)
            {
                return Conflict();
            }
            return Ok();
        }
        [HttpPost("process/refund")]
        public async Task<ActionResult> Refund(RefundedTicket Rticket)
        {
            if (System.Text.ASCIIEncoding.Unicode.GetByteCount(JsonConvert.SerializeObject(Rticket)) >= 2000)
            {
                return StatusCode(413);
            }
            JsonSchemaValidation ValResult = new JsonSchemaValidation();
            if (ValResult.JsonSchema(JsonConvert.SerializeObject(Rticket), System.IO.File.ReadAllText("RefundJsonSchema.json"))== false)
            {
                return StatusCode(400);
            }
            try
            {
                await _service.RefundTransaction(Rticket);
            }
            catch (DbUpdateException exception)
            {
                return Conflict();
            }
            return Ok();
        }
        
        
    }
}
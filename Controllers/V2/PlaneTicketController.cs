using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Json.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using Models;
using Tickets.Services;
using JsonValidator = Newtonsoft.Json.Schema.JsonValidator;
using JsonValidatorContext = Newtonsoft.Json.Schema.JsonValidatorContext;

namespace Tickets.Controllers
{
    [ApiController]
    [Route("/v{version:apiVersion}/")]
    [ApiVersion("2.0")]
    public class PlaneTicketController : ControllerBase
    {
        private readonly ItemContext _context;
        private readonly IService _service;
        public PlaneTicketController(ItemContext context, IService service)
        {
            _context = context;
            _service = service;
        }

        [HttpPost("process/sale")]
        [RequestSizeLimit(2000)]
        public async Task<ActionResult> Sale(PlaneTicket ticket)
        {
            string stringJson = JsonConvert.SerializeObject(ticket);
            string data = System.IO.File.ReadAllText("SaleJsonSchema.json");
            var model = JObject.Parse(data);
            var schema = JSchema.Parse(stringJson);
            bool val = model.IsValid(schema);
            if (val ==false)//JsonSchema validating.
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
        [RequestSizeLimit(2000)]
        public async Task<ActionResult> Refund(RefundedTicket Rticket)
        {
            
            string stringJson = JsonConvert.SerializeObject(Rticket);
            string data = System.IO.File.ReadAllText("RefundJsonSchema.json");
            var model = JObject.Parse(data);
            var schema = JSchema.Parse(stringJson);
            bool val = model.IsValid(schema);
            if (val== false)//JsonSchema validating.
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
            Console.WriteLine("Ok");
            return Ok();
        }
        
        
    }
}
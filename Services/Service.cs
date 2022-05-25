using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models;
using Tickets.Services;

namespace Tickets.Services
{
    public class Service: IService

    {
        private ItemContext _context;
        private readonly IMapper _mapper;

        public Service(ItemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> SaleTransaction(PlaneTicket ticket)
        {
            if (ticket.passenger.ticket_number == "")
            {
                throw new DbUpdateException();
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                int serialNumber = 1;
                
                foreach (var route in ticket.routes)
                {
                    var itemWithoutRoutes = _mapper.Map<PlaneTicket, Item>(ticket);
                    var itemWithRoutes = _mapper.Map<Models.Route, Item>(route, itemWithoutRoutes);
                    itemWithRoutes.SerialNumber = serialNumber;
                    _context.Add(itemWithRoutes);
                    serialNumber += 1;
                }

                await _context.SaveChangesAsync();
                transaction.Commit();
                return true;
            }
        }

        public async Task<bool> RefundTransaction(RefundedTicket ticketRim)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var items = _context.Items.Where(s => s.TicketNumber == ticketRim.TicketNumber);
                
                if (!items.Any())
                {
                    throw new DbUpdateException();
                }
                
                foreach (var i in items)
                {
                    if (i.OperationType == "refund")
                    {
                        throw new DbUpdateException();
                    }
                    i.OperationType = ticketRim.OperationType;
                    i.OperationTime = ticketRim.OperationTime;
                    i.OperationPlace = ticketRim.OperationPlace;
                }

                await _context.SaveChangesAsync();
                
                transaction.Commit();
                return true;
            }
        }
    }
}
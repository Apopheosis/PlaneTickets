using System.Threading.Tasks;
using Models;

namespace Tickets.Services
{
    public interface IService
        {
            Task<bool> SaleTransaction(PlaneTicket ticket);
            Task<bool> RefundTransaction(RefundedTicket Rticket);
        }
}
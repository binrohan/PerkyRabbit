using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Entities;
using IRepository;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DataContext _context;
        public TicketRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Ticket>> GetTicketsAsync()
        {
            return await _context
                        .Tickets
                        .ToListAsync();
        }
    }
}
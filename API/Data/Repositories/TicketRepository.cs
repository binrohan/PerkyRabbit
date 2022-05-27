using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Entities;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
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
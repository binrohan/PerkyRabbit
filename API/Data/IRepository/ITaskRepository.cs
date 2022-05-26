using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Entities;

namespace IRepository
{
    public interface ITicketRepository
    {
        Task<IReadOnlyList<Ticket>> GetTicketsAsync();
    }
}
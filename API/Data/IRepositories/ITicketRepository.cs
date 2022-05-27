using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Entities;

namespace Data.IRepositories
{
    public interface ITicketRepository
    {
        Task<IReadOnlyList<Ticket>> GetTicketsAsync();
    }
}
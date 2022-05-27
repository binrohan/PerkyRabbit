using System;
using System.Threading.Tasks;
using Data;
using Dtos;
using Entities;
using Helpers;
using Data.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        public TicketsController(ITicketRepository repo, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var ticketsFromRepo = await _repo.GetTicketsAsync();

            return Ok(new ApiResponse(200, ticketsFromRepo));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(int id)
        {
            var ticket = await _unitOfWork.Repository<Ticket>().GetByIdAsync(id);

            if(ticket is null) return NotFound(new ApiResponse(404, "Ticket Not Found"));

            return Ok(new ApiResponse(200, ticket));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(TicketToCreateDto ticketToCreate)
        {
            // Mapper can be used here
            var ticket = new Ticket
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Description = ticketToCreate.Description,
                Price = ticketToCreate.Price,
                Title = ticketToCreate.Title
            };

            _unitOfWork.Repository<Ticket>().Add(ticket);

            var result = await _unitOfWork.Complete();

            if(result <= 0) return BadRequest(new ApiResponse(400, "Failed To Create Ticket"));

            return Ok(new ApiResponse(201, ticket));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket([FromRoute]int id, [FromBody]TicketToUpdateDto ticketToUpdate)
        {
            var ticketFromRepo = await _unitOfWork.Repository<Ticket>().GetByIdAsync(id);

            if(ticketFromRepo is null) return NotFound(new ApiResponse(404, "Ticket Not Found"));

            ticketFromRepo.Description = ticketToUpdate.Description;
            ticketFromRepo.Title = ticketToUpdate.Title;
            ticketFromRepo.Price = ticketToUpdate.Price;
            ticketFromRepo.UpdatedAt = DateTime.Now;

            var result = await _unitOfWork.Complete();

            if(result <= 0) return BadRequest(new ApiResponse(400, "Failed To Update Ticket"));

            return Ok(new ApiResponse(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticketFromRepo = await _unitOfWork.Repository<Ticket>().GetByIdAsync(id);

            if(ticketFromRepo is null) return NotFound(new ApiResponse(404, "Ticket Not Found"));

            _unitOfWork.Repository<Ticket>().Delete(ticketFromRepo);

            var result = await _unitOfWork.Complete();

            if(result <= 0) return BadRequest(new ApiResponse(400, "Failed To Delete Ticket"));

            return Ok(new ApiResponse(200, "Succeeded"));
        }
    }
}

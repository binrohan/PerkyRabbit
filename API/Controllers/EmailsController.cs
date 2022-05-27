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
    public class EmailsController : ControllerBase
    {
        private readonly IEmailRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public EmailsController(IEmailRepository repo, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmails()
        {
            var EmailsFromRepo = await _repo.GetEmailsAsync();

            return Ok(new ApiResponse(200, EmailsFromRepo));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmail(int id)
        {
            var mail = await _unitOfWork.Repository<Email>().GetByIdAsync(id);

            if(mail is null) return NotFound(new ApiResponse(404, "Email Not Found"));

            return Ok(new ApiResponse(200, mail));
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmail(EmailToCreateDto EmailToCreate)
        {
            // Mapper can be used here
            var mail = new Email
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Description = EmailToCreate.Description,
                Price = EmailToCreate.Price,
                Title = EmailToCreate.Title
            };

            _unitOfWork.Repository<Email>().Add(mail);

            var result = await _unitOfWork.Complete();

            if(result <= 0) return BadRequest(new ApiResponse(400, "Failed To Create Email"));

            return Ok(new ApiResponse(201, mail));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmail([FromRoute]int id, [FromBody]EmailToUpdateDto EmailToUpdate)
        {
            var mailFromRepo = await _unitOfWork.Repository<Email>().GetByIdAsync(id);

            if(mailFromRepo is null) return NotFound(new ApiResponse(404, "Email Not Found"));

            mailFromRepo.Description = EmailToUpdate.Description;
            mailFromRepo.Title = EmailToUpdate.Title;
            mailFromRepo.Price = EmailToUpdate.Price;
            mailFromRepo.UpdatedAt = DateTime.Now;

            var result = await _unitOfWork.Complete();

            if(result <= 0) return BadRequest(new ApiResponse(400, "Failed To Update Email"));

            return Ok(new ApiResponse(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmail(int id)
        {
            var mailFromRepo = await _unitOfWork.Repository<Email>().GetByIdAsync(id);

            if(mailFromRepo is null) return NotFound(new ApiResponse(404, "Email Not Found"));

            _unitOfWork.Repository<Email>().Delete(mailFromRepo);

            var result = await _unitOfWork.Complete();

            if(result <= 0) return BadRequest(new ApiResponse(400, "Failed To Delete Email"));

            return Ok(new ApiResponse(200, "Succeeded"));
        }
    }
}

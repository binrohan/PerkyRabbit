using System;
using System.Threading.Tasks;
using Data;
using Dtos;
using Entities;
using Helpers;
using Data.IRepositories;
using Data.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _service;

        public EmailsController(IUnitOfWork unitOfWork, IMailService service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MailToSendDto mailDto)
        {
            await _service.SendAsync(mailDto);

            var mail = new Mail()
            {
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                To = mailDto.To,
                Subject = mailDto.Subject,
                Body = mailDto.Body,
                CC = mailDto.CC,
                BCC = mailDto.BCC
            };

            _unitOfWork.Repository<Mail>().Add(mail);

             var result = await _unitOfWork.Complete();

            if(result <= 0) return BadRequest(new ApiResponse(400, "Failed To Save Mail"));
            
            return Ok(new ApiResponse(201, mail, "Mail Sent"));
        }

        [HttpGet]
        public async Task<IActionResult> GetMails(bool isDeleted = false)
        {
            var mailsFromRepo = await _unitOfWork.Repository<Mail>()
                                                 .ListAsync(m => m.IsDeleted == isDeleted);

            return Ok(new ApiResponse(200, ticketsFromRepo));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> DeleteMail(int id)
        {
            var mail = await _unitOfWork.Repository<Mail>().GetByIdAsync(id);

            if(mail is null) return NotFound(new ApiResponse(404, "Mail Not Found"));

            mail.IsDeleted = true;

            var result = await _unitOfWork.Complete();

            if(result <= 0) return BadRequest(new ApiResponse(400, "Failed To Save Mail"));
            
            return Ok(new ApiResponse(205, "Mail Deleted"));
        }
    }
}

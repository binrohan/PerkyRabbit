using System;
using System.Threading.Tasks;
using API.Controllers;
using Data;
using Data.IRepositories;
using Dtos;
using Entities;
using FluentAssertions;
using Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using static Data.IRepositories.IGenericRepository;

namespace Tests
{
    public class TicketsControllerTests
    {
        private readonly Mock<IUnitOfWork> unitOfWorkStub = new();
        private readonly Mock<ITicketRepository> repositoryStub = new();
        private readonly Mock<IGenericRepository<Ticket>> genericRepository = new();
        private readonly Random rand = new();

        [Fact]
        public async Task CreateTicketAsync_WithTicketToCreate_ReturnsCreatedTickets()
        {
            // Arrange
            var itemToCreate = new TicketToCreateDto(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), rand.Next(1000));
        
            genericRepository.Setup(repo => repo.Add(It.IsAny<Ticket>()));

            var controller = new TicketsController(repositoryStub.Object, unitOfWorkStub.Object);

            // Act
            var res = await controller.CreateTicket(itemToCreate);
            var actionResult = res as OkObjectResult;
            var payload = actionResult.Value as ApiResponse;
            // Assert
            var createdItem = payload.Data as Ticket;
            itemToCreate.Should().BeEquivalentTo(createdItem, options => options.ComparingByMembers<Ticket>().ExcludingMissingMembers()
            );

            createdItem.Id.Should().NotBe(0);
            createdItem.CreatedAt.Should().BeCloseTo(DateTime.Now, 1000);
        }

        [Fact]
        public async Task GetTicketAsync_WithUnexistingTicket_ReturnsNotFound()
        {
            // Arrange
            genericRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Ticket)null);

            var controller = new TicketsController(repositoryStub.Object, unitOfWorkStub.Object);

            // Act
            var result = await controller.GetTicket(0);
            var okResult = result as OkObjectResult;

            // Assert
            okResult.Value.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetTicketAsync_WithExistingTicket_ReturnsExpectedTicket()
        {
            // Arrange
            Ticket expectedItem = CreateRandomItem();

            genericRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(expectedItem);

            var controller = new TicketsController(repositoryStub.Object, unitOfWorkStub.Object);

            // Act
            var result = await controller.GetTicket(rand.Next());
            var okResult = result as OkObjectResult;

            // Assert
            okResult.Value.Should().BeEquivalentTo(expectedItem);
        }

        [Fact]
        public async Task GetTicketsAsync_WithExistingTickets_ReturnsExpectedTickets()
        {
            // Arrange
            var expectedItems = new[] { CreateRandomItem(), CreateRandomItem(), CreateRandomItem() };
            var expectedResponse = new ApiResponse(200, expectedItems);

            repositoryStub.Setup(repo => repo.GetTicketsAsync())
                .ReturnsAsync(expectedItems);

            var controller = new TicketsController(repositoryStub.Object, unitOfWorkStub.Object);

            // Act
            var result = await controller.GetTickets();
            var okResult = result as OkObjectResult;

            // Assert
            okResult.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task UpdateTicketsAsync_WithUnexistingTicket_ReturnsNotFound()
        {
            // Arrange
            var controller = new TicketsController(repositoryStub.Object, unitOfWorkStub.Object);

            // Act
            var result = await controller.UpdateTicket(rand.Next(), new TicketToUpdateDto(rand.Next(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), rand.Next()));

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateTicketAsync_WithExistingTicket_ReturnsNoContent()
        {
            // Arrange
            var existingItem = CreateRandomItem();
            var itemToUpdate = new TicketToUpdateDto(rand.Next(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), existingItem.Price + 3);
            var itemId = existingItem.Id;
            var expectedResponse = new ApiResponse(204);

            var controller = new TicketsController(repositoryStub.Object, unitOfWorkStub.Object);

            // Act
            var res = await controller.UpdateTicket(itemId, itemToUpdate);
            var actionResult = res as OkObjectResult;

            // Assert
            actionResult.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task DeleteTicketAsync_WithUnexistingTicket_ReturnsNotFound()
        {
            // Arrange
            var expectedResponse = new ApiResponse(404, "Ticket Not Found");
            var ticket = CreateRandomItem();

            genericRepository.Setup(repo => repo.Delete(It.IsAny<Ticket>()));

            var controller = new TicketsController(repositoryStub.Object, unitOfWorkStub.Object);

            // Act
            var res = await controller.DeleteTicket(rand.Next());
            var actionResult = res as OkObjectResult;

            // Assert
            actionResult.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task DeleteTicketAsync_WithExistingTicket_ReturnsNoContent()
        {
            // Arrange
            var expectedResponse = new ApiResponse(200, "Succeeded");
            var ticket = CreateRandomItem();

            genericRepository.Setup(repo => repo.Delete(It.IsAny<Ticket>()));

            var controller = new TicketsController(repositoryStub.Object, unitOfWorkStub.Object);

            // Act
            var res = await controller.DeleteTicket(rand.Next());
            var actionResult = res as OkObjectResult;

            // Assert
            actionResult.Value.Should().BeEquivalentTo(expectedResponse);
        }

        private Ticket CreateRandomItem()
        {
            return new()
            {
                Id = rand.Next(),
                Title = Guid.NewGuid().ToString(),
                Price = rand.Next(1000),
                CreatedAt = DateTime.Now,
                Description = Guid.NewGuid().ToString(),
                UpdatedAt = DateTime.Now
            };
        }
    }
}

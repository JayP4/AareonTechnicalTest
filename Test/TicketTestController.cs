using AareonTechnicalTest.Controller;
using AareonTechnicalTest.Models;
using AareonTechnicalTest.Mapper;
using AareonTechnicalTest.Services;
using AareonTechnicalTest.ViewModels;
using ASPNetCoreWebAPiDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using AareonTechnicalTest;
using Microsoft.EntityFrameworkCore;

namespace Test
{
    [Collection("Non-Parallel Collection")]
    public class TicketTestController 
    {
        private readonly TicketController _controller;
        private readonly ITicketService _service;
        private readonly ApplicationContext appContext;
        public TicketTestController()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>().UseSqlite();
            appContext = new ApplicationContext(optionsBuilder.Options, true);
            appContext.Database.EnsureDeleted();
            appContext.Database.EnsureCreated();

            _service = new TicketService(appContext);
            _controller = new TicketController(_service);
        }
        [Fact]
        public void CreateTicket_ReturnSucess()
        {
            //Arrange
            Ticket Ticket = new Ticket();
            Ticket.Content = "Ticket For System Maintanance";
            Ticket.PersonId = 1;
            
            // Act
            var result = _controller.SaveTickets(Ticket);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseModel = Assert.IsType<ResponseModel>(actionResult.Value);
            Assert.True(responseModel.IsSuccess);
            Ticket = appContext.Tickets.FirstOrDefault();
            Assert.Equal("Ticket For System Maintanance", Ticket.Content);
        }
        [Fact]
        public void AllTickets_ReturnsAllItems()
        {
            //Arrange
            MockDataUtility.CreatePersonAndTicket(appContext);
            
            // Act
            IActionResult response =  _controller.GetAllTickets();
            
            // Assert
            var res = response as OkObjectResult;
            var Tickets = res.Value as List<Ticket>;
            Assert.Single(Tickets);
        }
        [Fact]
        public void GetTicketByID_ReturnSucess()
        {
            //Arrange
            MockDataUtility.CreatePersonAndTicket(appContext);

            // Act
            IActionResult response = _controller.GetTicketsById(1);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(response);
            var Ticket = Assert.IsType<Ticket>(actionResult.Value);
            
            Assert.Equal(1, Ticket.Id);
        }
        [Fact]
        public void UpdateTicket_ReturnSucess()
        {
            //Arrange
            MockDataUtility.CreatePersonAndTicket(appContext);
           
            var Ticket = _service.GetTicketDetailsById(1);
            Ticket.Content = "Ticket Content Updated.";

            var TicketUpdate = TicketMapper.GetUpdateTicket(Ticket);
            // Act
            var result = _controller.UpdateTickets(TicketUpdate);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseModel = Assert.IsType<ResponseModel>(actionResult.Value);
            Assert.True(responseModel.IsSuccess);
            Ticket = appContext.Tickets.Where(x => x.Id == 1).FirstOrDefault(); 
            Assert.Equal("Ticket Content Updated.", Ticket.Content);
        }
        [Fact]
        public void DeleteTicket_ReturnSucess()
        {
            //Arrange
            MockDataUtility.CreatePersonAndTicket(appContext);
            var Ticket = _service.GetTicketDetailsById(1);

            // Act
            var result = _controller.DeleteTicket(Ticket.Id);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseModel = Assert.IsType<ResponseModel>(actionResult.Value);
            Assert.True(responseModel.IsSuccess);
            Ticket = appContext.Tickets.Where(x => x.Id == 1).FirstOrDefault();
            Assert.Null(Ticket); 
        }
       
    }

}


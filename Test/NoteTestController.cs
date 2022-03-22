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
    public class NoteTestController
    {
        private readonly NoteController _controller;
        private readonly INoteService _service;
        private ApplicationContext appContext;
        public NoteTestController()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>().UseSqlite();
            appContext = new ApplicationContext(optionsBuilder.Options, true);
            appContext.Database.EnsureDeleted();
            appContext.Database.EnsureCreated();

            _service = new NoteService(appContext);
            _controller = new NoteController(_service);
        }
        [Fact]
        public void GetAllNotes_ReturnsAllItems()
        {
            //Arrange
            MockDataUtility.CreatePersonAndTicket(appContext);
            MockDataUtility.CreateNotes(appContext);
            
            // Act
            IActionResult response =  _controller.GetAllNotes();
            
            // Assert
            var res = response as OkObjectResult;
            var Notes = res.Value as List<Note>;
            Assert.Equal(2, Notes.Count);
        }
        [Fact]
        public void GetNoteByID_ReturnSucess()
        {
            //Arrange
            MockDataUtility.CreatePersonAndTicket(appContext);
            MockDataUtility.CreateNotes(appContext);

            // Act
            IActionResult response = _controller.GetNotesById(1);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(response);
            var note = Assert.IsType<Note>(actionResult.Value);
            
            Assert.Equal(1, note.Id);
        }
        [Fact]
        public void CreateNote_ReturnSucess()
        {
            //Arrange
            MockDataUtility.CreatePersonAndTicket(appContext);
            Note note = new Note();
            note.TicketId = 1;
            note.Content = "Unit Test Note.";
            note.PersonId = 1;
            
            // Act
            var result = _controller.SaveNotes(note);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseModel = Assert.IsType<ResponseModel>(actionResult.Value);
            Assert.True(responseModel.IsSuccess);
            note = appContext.Notes.FirstOrDefault();
            Assert.Equal("Unit Test Note.", note.Content);
        }
        [Fact]
        public void UpdateNote_ReturnSucess()
        {
            //Arrange
            MockDataUtility.CreatePersonAndTicket(appContext);
            MockDataUtility.CreateNotes(appContext);
            var note = _service.GetNoteDetailsById(1);
            note.Content = "Updated Unit Test Note.";

            var noteUpdate = NoteMapper.GetUpdateNote(note);
            // Act
            var result = _controller.UpdateNotes(noteUpdate);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseModel = Assert.IsType<ResponseModel>(actionResult.Value);
            Assert.True(responseModel.IsSuccess);
            note = appContext.Notes.Where(x => x.Id == 1).FirstOrDefault(); 
            Assert.Equal("Updated Unit Test Note.", note.Content);
        }
        [Fact]
        public void DeleteNote_ReturnSucess()
        {
            //Arrange
            MockDataUtility.CreatePersonAndTicket(appContext);
            MockDataUtility.CreateNotes(appContext);
            var note = _service.GetNoteDetailsById(1);

            // Act
            var result = _controller.DeleteNote(note.Id, true);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseModel = Assert.IsType<ResponseModel>(actionResult.Value);
            Assert.True(responseModel.IsSuccess);
            note = appContext.Notes.Where(x => x.Id == 1).FirstOrDefault();
            Assert.Null(note); 
        }
        [Fact]
        public void DeleteNote_UserNotAdmin_ShouldNotAllow()
        {
            //Arrange
            MockDataUtility.CreatePersonAndTicket(appContext);
            MockDataUtility.CreateNotes(appContext);
            var note = _service.GetNoteDetailsById(1);

            // Act
            var result = _controller.DeleteNote(note.Id, false);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseModel = Assert.IsType<ResponseModel>(actionResult.Value);
            Assert.False(responseModel.IsSuccess);
            
        }
    }
}


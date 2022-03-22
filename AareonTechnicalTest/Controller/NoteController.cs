using AareonTechnicalTest.Models;
using AareonTechnicalTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AareonTechnicalTest.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        INoteService _NoteService;
        public NoteController(INoteService service)
        {
            _NoteService = service;
        }

        /// <summary>
        /// get all Notes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllNotes()
        {
            try
            {
                var Notes = _NoteService.GetNoteList();
                if (Notes == null)
                    return NotFound();
                return Ok(Notes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// get Note details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/id")]
        public IActionResult GetNotesById(int id)
        {
            try
            {
                var Notes = _NoteService.GetNoteDetailsById(id);
                if (Notes == null)
                    return NotFound();
                return Ok(Notes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// update Note
        /// </summary>
        /// <param name="NoteModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult SaveNotes(Note NoteModel)
        {
            try
            {
                var model = _NoteService.SaveNote(NoteModel);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// save Note
        /// </summary>
        /// <param name="NoteModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateNotes(NoteUpdate NoteModel)
        {
            try
            {
                var model = _NoteService.UpdateNote(NoteModel);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// delete Note  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteNote(int id, bool IsAdmin)
        {
            try
            {
                var model = _NoteService.DeleteNote(id, IsAdmin);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

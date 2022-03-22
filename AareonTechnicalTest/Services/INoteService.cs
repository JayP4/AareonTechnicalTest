using AareonTechnicalTest.Models;
using AareonTechnicalTest.ViewModels;
using System.Collections.Generic;

namespace AareonTechnicalTest.Services
{
    public interface INoteService
    {
        /// <summary>
        /// get list of all Note
        /// </summary>
        /// <returns></returns>
        List<Note> GetNoteList();

        /// <summary>
        /// get Note details by Note id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Note GetNoteDetailsById(int Id);

        /// <summary>
        ///  add Note
        /// </summary>
        /// <param name="NoteModel"></param>
        /// <returns></returns>
        ResponseModel SaveNote(Note NoteModel);

        /// <summary>
        ///  edit Note
        /// </summary>
        /// <param name="NoteModel"></param>
        /// <returns></returns>
        ResponseModel UpdateNote(NoteUpdate NoteModel);

        /// <summary>
        /// delete Note
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ResponseModel DeleteNote(int Id, bool IsAdmin);
    }
}


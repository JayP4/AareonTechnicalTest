using AareonTechnicalTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AareonTechnicalTest.Services;
using AareonTechnicalTest.ViewModels;
using AareonTechnicalTest;

namespace ASPNetCoreWebAPiDemo.Services
{
    public class NoteService : INoteService
    {
        private readonly ApplicationContext _context;
        public NoteService(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// get list of all Note
        /// </summary>
        /// <returns></returns>
        public List<Note> GetNoteList()
        {
            return _context.Set<Note>().ToList();
        }

        /// <summary>
        /// get Note details by Note id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Note GetNoteDetailsById(int Id)
        {
            return _context.Find<Note>(Id);
        }

        /// <summary>
        ///  add edit Note
        /// </summary>
        /// <param name="NoteModel"></param>
        /// <returns></returns>
        public ResponseModel SaveNote(Note NoteModel)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                _context.Add<Note>(NoteModel);
                model.Messsage = "Note Inserted Successfully";

                _context.SaveChanges();
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        /// <summary>
        ///  add edit Note
        /// </summary>
        /// <param name="NoteModel"></param>
        /// <returns></returns>
        public ResponseModel UpdateNote(NoteUpdate NoteModel)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Note _temp = GetNoteDetailsById(NoteModel.Id);

                _temp.Content = NoteModel.Content;
                _temp.PersonId = NoteModel.PersonId;
                _temp.TicketId = NoteModel.TicketId;
                _context.Update<Note>(_temp);
                model.Messsage = "Note Updated Successfully";

                _context.SaveChanges();
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        /// <summary>
        /// delete Note
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseModel DeleteNote(int Id, bool IsAdmin)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                if (IsAdmin)
                {
                    Note _temp = GetNoteDetailsById(Id);
                    if (_temp != null)
                    {
                        _context.Remove<Note>(_temp);
                        _context.SaveChanges();
                        model.IsSuccess = true;
                        model.Messsage = "Note Deleted Successfully";
                    }
                    else
                    {
                        model.IsSuccess = false;
                        model.Messsage = "Note Not Found";
                    }
                }
                else
                {
                    model.IsSuccess = false;
                    model.Messsage = "Note Can Not Be Deleted.";
                }
            }

            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

    }
}

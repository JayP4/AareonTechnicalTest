using AareonTechnicalTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AareonTechnicalTest.Services;
using AareonTechnicalTest.ViewModels;
using AareonTechnicalTest;
using Microsoft.EntityFrameworkCore;

namespace ASPNetCoreWebAPiDemo.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationContext _context;
        public TicketService(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// get list of all Ticket
        /// </summary>
        /// <returns></returns>
        public List<Ticket> GetTicketList()
        {
            return _context.Tickets.Include(t => t.Notes).ToList();
        }

        /// <summary>
        /// get Ticket details by Ticket id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Ticket GetTicketDetailsById(int Id)
        {
            return _context.Tickets.Include(t => t.Notes).Where(x => x.Id == Id).FirstOrDefault();
        }

        /// <summary>
        ///  add Ticket
        /// </summary>
        /// <param name="TicketModel"></param>
        /// <returns></returns>
        public ResponseModel SaveTicket(Ticket TicketModel)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                _context.Add<Ticket>(TicketModel);
                model.Messsage = "Ticket Inserted Successfully";

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
        ///  edit Ticket
        /// </summary>
        /// <param name="TicketModel"></param>
        /// <returns></returns>
        public ResponseModel UpdateTicket(TicketUpdate TicketModel)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Ticket _temp = GetTicketDetailsById(TicketModel.Id);

                _temp.Content = TicketModel.Content;
                _temp.PersonId = TicketModel.PersonId;
                _context.Update<Ticket>(_temp);
                model.Messsage = "Ticket Updated Successfully";

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
        /// delete Ticket
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResponseModel DeleteTicket(int Id)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Ticket _temp = GetTicketDetailsById(Id);
                if (_temp != null)
                {
                    _context.Remove<Ticket>(_temp);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.Messsage = "Ticket Deleted Successfully";
                }
                else
                {
                    model.IsSuccess = false;
                    model.Messsage = "Ticket Not Found";
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

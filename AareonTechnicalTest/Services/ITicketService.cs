using AareonTechnicalTest.Models;
using AareonTechnicalTest.ViewModels;
using System.Collections.Generic;

namespace AareonTechnicalTest.Services
{
    public interface ITicketService
    {
        /// <summary>
        /// get list of all Ticket
        /// </summary>
        /// <returns></returns>
        List<Ticket> GetTicketList();

        /// <summary>
        /// get Ticket details by Ticket id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Ticket GetTicketDetailsById(int Id);

        /// <summary>
        ///  add Ticket
        /// </summary>
        /// <param name="TicketModel"></param>
        /// <returns></returns>
        ResponseModel SaveTicket(Ticket TicketModel);

        /// <summary>
        ///  add Ticket
        /// </summary>
        /// <param name="TicketModel"></param>
        /// <returns></returns>
        ResponseModel UpdateTicket(TicketUpdate TicketModel);

        /// <summary>
        /// delete Ticket
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ResponseModel DeleteTicket(int Id);
    }
}


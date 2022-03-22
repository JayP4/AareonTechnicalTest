using AareonTechnicalTest.Models;
using AareonTechnicalTest.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AareonTechnicalTest.Controller
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class TicketController : ControllerBase
    {
        ITicketService _TicketService;
        public TicketController(ITicketService service)
        {
            _TicketService = service;
        }

        /// <summary>
        /// get all Tickets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllTickets()
        {
            try
            {
                var Tickets = _TicketService.GetTicketList();
                if (Tickets == null)
                    return NotFound();
                return Ok(Tickets);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// get Ticket details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/id")]
        public IActionResult GetTicketsById(int id)
        {
            try
            {
                var Tickets = _TicketService.GetTicketDetailsById(id);
                if (Tickets == null)
                    return NotFound();
                return Ok(Tickets);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// save Ticket
        /// </summary>
        /// <param name="TicketModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult SaveTickets(Ticket TicketModel)
        {
            try
            {
                var model = _TicketService.SaveTicket(TicketModel);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// update Ticket
        /// </summary>
        /// <param name="TicketModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateTickets(TicketUpdate TicketModel)
        {
            try
            {
                var model = _TicketService.UpdateTicket(TicketModel);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// delete Ticket  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteTicket(int id)
        {
            try
            {
                var model = _TicketService.DeleteTicket(id);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

using BookCatalogue.Manager;
using BookCatalogue.MessageQueue;
using BookCatalogue.Models;
using BookCatalogue.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookManager _bookManager;
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger, IBookManager bookManager)
        {
            _logger = logger;
            _bookManager = bookManager;
        }

        /// <summary>
        /// Get books based on the search criteria
        /// </summary>
        /// <returns>List of books</returns>
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult> Get(string searchCriteria)
        {
            var books = await _bookManager.Get(searchCriteria);
            return Ok(books);
        }

        /// <summary>
        /// Add new book(s) to the database.
        /// </summary>
        /// <param name="newBooks"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Add(Book newBook)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState[ModelState.FirstOrDefault().Key].Errors[0].ErrorMessage);
            var isBooksAdded = await _bookManager.Add(newBook);
            return Ok(isBooksAdded);
        }

        /// <summary>
        /// Update the details of existing book(s).
        /// </summary>
        /// <param name="modifiedBooks"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult> Update(Book modifiedBook)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState[ModelState.FirstOrDefault().Key].Errors[0].ErrorMessage);
            bool isBooksUpdated = await _bookManager.Update(modifiedBook);
            return Ok(isBooksUpdated);
        }

        /// <summary>
        ///  Delete the book(s) based on the ID
        /// </summary>
        /// <param name="selectedBookIDs"></param>
        /// <returns></returns>        
        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> Delete(List<int> selectedBookIDs)
        {
            if (selectedBookIDs.Count() == 0)
                return BadRequest("Please provide book id");
            bool isBooksDeleted = await _bookManager.Delete(selectedBookIDs);
            return Ok(isBooksDeleted);
        }
    }
}

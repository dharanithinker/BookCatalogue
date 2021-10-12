using BookCatalogue.MessageQueue;
using BookCatalogue.Models;
using BookCatalogue.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCatalogue.Manager
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookCatalogueMessageSender _bookCatalogueMessageSender;
        public BookManager(IBookRepository bookRepository, IBookCatalogueMessageSender bookCatalogueMessageSender)
        {
            _bookRepository = bookRepository;
            _bookCatalogueMessageSender = bookCatalogueMessageSender;
        }

        public async Task<List<Book>> Get(string searchCriteria)
        {
            return await _bookRepository.Get(searchCriteria);
        }

        public async Task<bool> Add(Book newBook)
        {
            bool isBooksAdded = await _bookRepository.Add(newBook);
            if (isBooksAdded)
            {
                _bookCatalogueMessageSender.SendNewBookNotification(newBook);
            }
            return isBooksAdded;
        }

        public async Task<bool> Update(Book modifiedBook)
        {
            bool isBooksUpdated = await _bookRepository.Update(modifiedBook);
            if (isBooksUpdated)
            {
                _bookCatalogueMessageSender.UpdateBookNotification(modifiedBook);
            }
            return isBooksUpdated;
        }

        public async Task<bool> Delete(List<int> selectedBookIDs)
        {
            bool isBooksDeleted = await _bookRepository.Delete(selectedBookIDs);
            if (isBooksDeleted)
            {
                _bookCatalogueMessageSender.DeleteBookNotification();
            }
            return isBooksDeleted;
        }
    }
}

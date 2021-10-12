using BookCatalogue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCatalogue.MessageQueue
{
    public interface IBookCatalogueMessageSender
    {
        void SendNewBookNotification(Book newBook);
        void UpdateBookNotification(Book updatedBook);
        void DeleteBookNotification();
    }
}

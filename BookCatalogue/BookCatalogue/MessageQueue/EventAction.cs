using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCatalogue.MessageQueue
{
    public class EventAction
    {
        public const string BOOK_ADDED = "Added";
        public const string BOOK_UPDATED = "Updated";
        public const string BOOK_DELETED = "Deleted";
    }
}

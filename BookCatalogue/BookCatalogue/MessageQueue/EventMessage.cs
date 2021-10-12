using BookCatalogue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCatalogue.MessageQueue
{
    public class EventMessage
    {
        public string Type { get; set; }
        public Book Data { get; set; }
        public string Message { get; set; }
    }
}

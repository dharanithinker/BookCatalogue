using BookCatalogue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCatalogue.Manager
{
    public interface IBookManager
    {
        Task<List<Book>> Get(string searchCriteria);
        Task<bool> Add(Book newBook);
        Task<bool> Update(Book modifiedBook);
        Task<bool> Delete(List<int> selectedBookIDs);
    }
}

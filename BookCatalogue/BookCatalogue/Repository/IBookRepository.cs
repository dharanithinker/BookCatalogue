using BookCatalogue.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCatalogue.Repository
{
    public interface IBookRepository
    {
        Task<List<Book>> Get(string searchCriteria);
        Task<bool> Add(Book newBook);
        Task<bool> Update(Book modifiedBook);
        Task<bool> Delete(List<int> selectedBookIDs, bool isSoftDelete = true);
    }
}

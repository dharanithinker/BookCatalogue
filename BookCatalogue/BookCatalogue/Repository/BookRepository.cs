using BookCatalogue.DBContexts;
using BookCatalogue.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCatalogue.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _dbContext;

        public BookRepository(BookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Book>> Get(string searchCriteria)
        {
            List<Book> books = new List<Book>();
            // Retrieve only active books            
            if (String.IsNullOrEmpty(searchCriteria) || String.IsNullOrWhiteSpace(searchCriteria))
            {
                books = await _dbContext.Books.Where(x => x.IsActive).ToListAsync();
            }
            else
            {
                books = await _dbContext.Books.Where(x => x.IsActive &&
                            (x.Title.Contains(searchCriteria) || x.Authors.Contains(searchCriteria) || x.ISBN.Contains(searchCriteria))
                        ).ToListAsync();
            }
            return books;
        }

        public async Task<bool> Add(Book newBook)
        {
            newBook.IsActive = true;
            newBook.CreatedBy = GetLoggedUserID();
            newBook.CreatedDate = DateTime.Now;
            _dbContext.Add(newBook);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Book modifiedBook)
        {
            // Updating Book information
            var existingBook = await _dbContext.Books.FindAsync(modifiedBook.ID);
            if (existingBook != null && existingBook.ID != 0)
            {
                existingBook.Title = modifiedBook.Title;
                existingBook.Authors = modifiedBook.Authors;
                existingBook.ISBN = modifiedBook.ISBN;
                existingBook.PublicationDate = modifiedBook.PublicationDate;
                existingBook.IsActive = true;
                existingBook.ModifiedBy = GetLoggedUserID();
                existingBook.ModifiedDate = DateTime.Now;
                _dbContext.Books.Update(existingBook);
                _dbContext.Entry(existingBook).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(List<int> selectedBookIDs, bool isSoftDelete = true)
        {
            foreach (var currentBookID in selectedBookIDs)
            {
                var currentBook = await _dbContext.Books.FindAsync(currentBookID);
                if (currentBook != null && currentBook.ID != 0)
                {
                    // Using soft delete option the data will remain in database and will not be visible in UI
                    if (isSoftDelete)
                    {
                        currentBook.IsActive = false;
                        currentBook.ModifiedBy = GetLoggedUserID();
                        currentBook.ModifiedDate = DateTime.Now;
                        _dbContext.Books.Update(currentBook);
                        _dbContext.Entry(currentBook).State = EntityState.Modified;
                    }
                    // To delete the data from database set 'isSoftDelete' flag as false.
                    else
                    {
                        _dbContext.Books.Remove(currentBook);
                    }
                }
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }

        // This method will be moved into UserController while implement the Authorization.
        public int GetLoggedUserID()
        {
            return 7;
        }
    }
}

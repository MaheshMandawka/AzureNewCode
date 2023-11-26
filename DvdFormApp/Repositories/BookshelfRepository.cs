using DvdFormApp.DataTransferObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace DvdFormApp.Repositories
{
    public class BookshelfRepository : IBookshelfRepository
    {
        private MediaContext _mediaContext;
        private ILogger _logger;

        public BookshelfRepository(MediaContext mediaContext, ILoggerFactory logger)
        {
            _mediaContext = mediaContext;
            _logger = logger.CreateLogger(nameof(BookshelfRepository));
        }

        public IQueryable<Bookshelf> GetBookshelves()
        {
            return _mediaContext.Bookshelves.AsQueryable();
        }

        public Bookshelf CreateBookshelf(BookshelfDto bookshelfDto)
        {
            try
            {
                var result = _mediaContext.Bookshelves.Add(new Bookshelf
                {
                    Name = bookshelfDto.Title,
                });
                _mediaContext.SaveChanges();
                return result.Entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return null;
            }
        }

        public Bookshelf UpdateBookshelf(BookshelfDto bookshelfDto)
        {
            try
            {
                var bookshelfToUpdate = _mediaContext.Bookshelves.FirstOrDefault(x => x.Id == bookshelfDto.Id);

                if (bookshelfToUpdate == null)
                {
                    // Failed to find any elements
                    return null;
                }

                bookshelfToUpdate.Name = bookshelfDto.Title;
                var result = _mediaContext.Update(bookshelfToUpdate);
                _mediaContext.SaveChanges();
                return result.Entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return null;
            }
        }
    }
}

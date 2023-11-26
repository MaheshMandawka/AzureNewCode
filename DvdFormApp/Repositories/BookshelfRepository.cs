using DvdFormApp.DataTransferObjects;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace DvdFormApp.Repositories
{
    public class BookshelfRepository : IBookshelfRepository
    {
        private MediaContext _mediaContext;
        private ILoggerFactory _logger;

        public BookshelfRepository(MediaContext mediaContext, ILoggerFactory logger)
        {
            _mediaContext = mediaContext;
            _logger = logger;
        }

        public IQueryable<Bookshelf> GetBookshelves()
        {
            return _mediaContext.Bookshelves.AsQueryable();
        }

        public Bookshelf CreateBookshelf(BookshelfDto bookshelfDto)
        {
            return null;
        }
    }
}

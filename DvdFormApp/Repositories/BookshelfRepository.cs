using DvdFormApp.DataTransferObjects;
using System.Linq;

namespace DvdFormApp.Repositories
{
    public class BookshelfRepository : IBookshelfRepository
    {
        private MediaContext _mediaContext;

        public BookshelfRepository(MediaContext mediaContext)
        {
            _mediaContext = mediaContext;
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

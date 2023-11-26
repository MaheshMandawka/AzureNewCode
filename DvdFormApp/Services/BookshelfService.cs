using DvdFormApp.DataTransferObjects;
using DvdFormApp.Repositories;
using System.Linq;

namespace DvdFormApp.Services
{
    public class BookshelfService : IBookshelfService
    {
        private IBookshelfRepository _bookshelfRepository;

        public BookshelfService(IBookshelfRepository bookshelfRepository)
        {
            _bookshelfRepository = bookshelfRepository;
        }

        public IQueryable<Bookshelf> GetBookshelves()
        {
            return _bookshelfRepository.GetBookshelves();
        }

        public Bookshelf CreateBookshelf(BookshelfDto bookshelfDto)
        {
            return _bookshelfRepository.CreateBookshelf(bookshelfDto);
        }
    }
}

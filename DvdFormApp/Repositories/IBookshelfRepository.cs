using DvdFormApp.DataTransferObjects;
using System.Linq;

namespace DvdFormApp.Repositories
{
    public interface IBookshelfRepository
    {
        IQueryable<Bookshelf> GetBookshelves();
        Bookshelf CreateBookshelf(BookshelfDto bookshelfDto);
        Bookshelf UpdateBookshelf(BookshelfDto bookshelfDto);
        bool DeleteBookshelf(int id);
    }
}

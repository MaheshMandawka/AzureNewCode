using DvdFormApp.DataTransferObjects;
using System.Linq;

namespace DvdFormApp.Services
{
    public interface IBookshelfService
    {
        IQueryable<Bookshelf> GetBookshelves();
        Bookshelf CreateBookshelf(BookshelfDto bookshelfDto);
    }
}

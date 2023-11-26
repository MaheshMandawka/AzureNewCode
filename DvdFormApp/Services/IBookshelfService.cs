using System.Linq;

namespace DvdFormApp.Services
{
    public interface IBookshelfService
    {
        IQueryable<Bookshelf> GetBookshelves();
    }
}

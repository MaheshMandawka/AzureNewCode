using System.Linq;

namespace DvdFormApp.Repositories
{
    public interface IBookshelfRepository
    {
        IQueryable<Bookshelf> GetBookshelves();
    }
}

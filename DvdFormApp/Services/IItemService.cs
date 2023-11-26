using System.Linq;

namespace DvdFormApp.Services
{
    public interface IItemService
    {
        IQueryable<Item> GetItems();
    }
}

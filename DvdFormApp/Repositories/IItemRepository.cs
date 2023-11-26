using DvdFormApp.DataTransferObjects;
using System.Linq;

namespace DvdFormApp.Repositories
{
    public interface IItemRepository
    {
        IQueryable<Item> GetItems();
        Item CreateLibraryItem(ItemDto itemDto);
        Item CreateBookshelfItem(ItemDto itemDto);
        Item AssignItemToBookshelf(ItemAssignmentDto itemDto);
        bool DeleteItem(int id);
    }
}

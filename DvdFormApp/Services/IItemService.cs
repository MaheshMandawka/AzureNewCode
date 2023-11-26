using DvdFormApp.DataTransferObjects;
using System.Linq;

namespace DvdFormApp.Services
{
    public interface IItemService
    {
        IQueryable<Item> GetItems();
        IQueryable<Item> GetItemsByKeyword(string keyword);
        Item CreateLibraryItem(ItemDto itemDto);
        Item CreateBookshelfItem(ItemDto itemDto);
        Item AssignItemToBookshelf(ItemAssignmentDto itemDto);
        bool DeleteItem(int id);
        IQueryable<Item> GetItemsByBookshelfId(int bookshelfId);
        Item UpdateItem(ItemDto itemDto);
    }
}

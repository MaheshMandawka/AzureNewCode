using DvdFormApp.DataTransferObjects;
using System.Linq;

namespace DvdFormApp.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private MediaContext _mediaContext;

        public ItemRepository(MediaContext mediaContext)
        {
            _mediaContext = mediaContext;
        }

        public IQueryable<Item> GetItems()
        {
            return _mediaContext.Items.AsQueryable();
        }

        public Item CreateLibraryItem(ItemDto itemDto)
        {
            return null;
        }

        public Item CreateBookshelfItem(ItemDto itemDto)
        {
            return null;
        }
    }
}

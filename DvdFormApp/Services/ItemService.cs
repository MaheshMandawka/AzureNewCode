using DvdFormApp.Repositories;
using System.Linq;

namespace DvdFormApp.Services
{
    public class ItemService : IItemService
    {
        private IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public IQueryable<Item> GetItems()
        {
            return _itemRepository.GetItems();
        }
    }
}

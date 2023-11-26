using DvdFormApp.DataTransferObjects;
using DvdFormApp.Repositories;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace DvdFormApp.Services
{
    public class ItemService : IItemService
    {
        private IItemRepository _itemRepository;
        private ILogger _logger;

        public ItemService(IItemRepository itemRepository, ILoggerFactory logger)
        {
            _itemRepository = itemRepository;
            _logger = logger.CreateLogger(nameof(ItemService));
        }

        public IQueryable<Item> GetItems()
        {
            return _itemRepository.GetItems();
        }

        public IQueryable<Item> GetItemsByKeyword(string keyword)
        {
            return _itemRepository.GetItems().Where(x => !string.IsNullOrWhiteSpace(x.Description) && x.Description.Contains(keyword));
        }

        public Item CreateLibraryItem(ItemDto itemDto)
        {
            return _itemRepository.CreateLibraryItem(itemDto);
        }

        public Item CreateBookshelfItem(ItemDto itemDto)
        {
            return _itemRepository.CreateBookshelfItem(itemDto);
        }

        public Item AssignItemToBookshelf(ItemAssignmentDto itemDto)
        {
            return _itemRepository.AssignItemToBookshelf(itemDto);
        }
        public bool DeleteItem(int id)
        {
            return _itemRepository.DeleteItem(id);
        }
        public IQueryable<Item> GetItemsByBookshelfId(int bookshelfId)
        {
            return _itemRepository.GetItems().Where(x => x.BookshelfId == bookshelfId);
        }

        public Item UpdateItem(ItemDto itemDto)
        {
            return _itemRepository.UpdateItem(itemDto);
        }
    }
}

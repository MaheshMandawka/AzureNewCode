using DvdFormApp.DataTransferObjects;
using System;
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
            try
            {
                var result = _mediaContext.Items.Add(new Item
                {
                    Name = itemDto.Title,
                    Description = itemDto.Description,
                    Type = itemDto.Type,
                    Date = DateTime.Parse(itemDto.Date),
                    CreatedAt = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow,
                });
                _mediaContext.SaveChanges();
                return result.Entity;
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e);
                return null;
            }
        }

        public Item CreateBookshelfItem(ItemDto itemDto)
        {
            try
            {
                var result = _mediaContext.Items.Add(new Item
                {
                    Name = itemDto.Title,
                    Description = itemDto.Description,
                    Type = itemDto.Type,
                    Date = DateTime.Parse(itemDto.Date),
                    CreatedAt = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow,
                    BookshelfId = itemDto.BookshelfId,
                });
                _mediaContext.SaveChanges();
                return result.Entity;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return null;
            }
        }
    }
}

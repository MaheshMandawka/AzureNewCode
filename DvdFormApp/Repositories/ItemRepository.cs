using DvdFormApp.DataTransferObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace DvdFormApp.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private MediaContext _mediaContext;
        private ILogger _logger;

        public ItemRepository(MediaContext mediaContext, ILoggerFactory logger)
        {
            _mediaContext = mediaContext;
            _logger = logger.CreateLogger(nameof(ItemRepository));
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
                _logger.LogError(e, e.Message);
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
                _logger.LogError(e, e.Message);
                return null;
            }
        }
        public Item AssignItemToBookshelf(ItemAssignmentDto itemDto)
        {
            try
            {
                var itemToUpdate = _mediaContext.Items.FirstOrDefault(x => x.Id == itemDto.ItemId);

                if (itemToUpdate == null)
                {
                    return null;
                }
                
                itemToUpdate.BookshelfId = itemDto.BookshelfId;
                var result = _mediaContext.Items.Update(itemToUpdate);
                _mediaContext.SaveChanges();
                return result.Entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return null;
            }
        }

        public bool DeleteItem(int id)
        {
            try
            {
                var itemToDelete = _mediaContext.Items.FirstOrDefault(x => x.Id == id);

                if (itemToDelete == null)
                {
                    // Object not found
                    return false;
                }

                _mediaContext.Items.Remove(itemToDelete);
                _mediaContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return false;
            }
        }

        public Item UpdateItem(ItemDto itemDto)
        {
            try
            {
                var itemToUpdate = _mediaContext.Items.FirstOrDefault(x => x.Id == itemDto.Id);

                if (itemToUpdate == null)
                {
                    // Failed to find any elements
                    return null;
                }

                itemToUpdate.Name = itemDto.Title;
                itemToUpdate.Description = itemDto.Description;
                itemToUpdate.Type = itemDto.Type;
                itemToUpdate.Date = itemDto.Date;
                var result = _mediaContext.Update(itemToUpdate);
                _mediaContext.SaveChanges();
                return result.Entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return null;
            }
        }
    }
}

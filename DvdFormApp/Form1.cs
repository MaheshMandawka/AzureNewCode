using DvdFormApp.Constants;
using DvdFormApp.DataTransferObjects;
using DvdFormApp.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DvdFormApp
{
    public partial class Form1 : Form
    {
        private IBookshelfService _bookshelfService;
        private IItemService _itemService;
        private ILogger _logger;

        public Form1(IBookshelfService bookshelfService, IItemService itemService, ILoggerFactory logger)
        {
            InitializeComponent();

            // Initialize Services
            _bookshelfService = bookshelfService;
            _itemService = itemService;

            // Initialize Logging
            _logger = logger.CreateLogger(nameof(Form1));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetupBookshelfLookup();
        }

        private void SetupBookshelfLookup()
        {
            var bookshelves = _bookshelfService.GetBookshelves().ToArray();

            activeBookshelf1Lookup.Items.AddRange(bookshelves);
            activeBookshelf2Lookup.Items.AddRange(bookshelves);
        }

        #region Transfer Item Controls
        private void btnTransferOneToTwo_Click(object sender, EventArgs e)
        {
            var selectedItem = activeBookshelf1.SelectedItem;
            var currentBookshelf = activeBookshelf1Lookup.SelectedItem;
            var selectedBookshelf = activeBookshelf2Lookup.SelectedItem;

            if (selectedItem == null || (selectedItem as Item) == null)
            {
                return;
            }

            if (selectedBookshelf == null || (selectedBookshelf as Bookshelf) == null)
            {
                return;
            }

            // If we have the same bookshelf selected on both we do nothing
            if ((currentBookshelf as Bookshelf).Id == (selectedBookshelf as Bookshelf).Id)
            {
                return;
            }

            var result = _itemService.AssignItemToBookshelf(new ItemAssignmentDto
            {
                ItemId = (selectedItem as Item).Id,
                BookshelfId = (selectedBookshelf as Bookshelf).Id,
            });

            if (result != null)
            {
                activeBookshelf1.Items.Remove(selectedItem);
                activeBookshelf2.Items.Add(selectedItem);
            }
            else
            {
                // Failure: notify user
            }
        }

        private void btnTransferTwoToOne_Click(object sender, EventArgs e)
        {
            var selectedItem = activeBookshelf2.SelectedItem;
            var currentBookshelf = activeBookshelf2Lookup.SelectedItem;
            var selectedBookshelf = activeBookshelf1Lookup.SelectedItem;

            if (selectedItem == null || (selectedItem as Item) == null)
            {
                return;
            }

            if (selectedBookshelf == null || (selectedBookshelf as Bookshelf) == null)
            {
                return;
            }

            // If we have the same bookshelf selected on both we do nothing
            if ((currentBookshelf as Bookshelf).Id == (selectedBookshelf as Bookshelf).Id)
            {
                return;
            }

            var result = _itemService.AssignItemToBookshelf(new ItemAssignmentDto
            {
                ItemId = (selectedItem as Item).Id,
                BookshelfId = (selectedBookshelf as Bookshelf).Id,
            });

            if (result != null)
            {
                activeBookshelf2.Items.Remove(selectedItem);
                activeBookshelf1.Items.Add(selectedItem);
            }
            else
            {
                // Failure: notify user
            }
        }
        #endregion

        #region Bookshelf 1 Controls
        private void btnRemoveItemBookshelf1_Click(object sender, EventArgs e)
        {
            var selectedItem = activeBookshelf1.SelectedItem;
            DeleteItemBasedOnSelectedItem(selectedItem);
        }

        private void btnEditSelectedBookshelf1_Click(object sender, EventArgs e)
        {
            var selectedItem = activeBookshelf1Lookup.SelectedItem;

            if (selectedItem == null || (selectedItem as Bookshelf) == null)
            {
                return;
            }

            editBookshelfDataHidden.Text = GenerateTemporaryBookshelfStorageKey((selectedItem as Bookshelf).Id, 1);
            editBookshelfTitleValue.Text = (selectedItem as Bookshelf).Name;
        }

        private void btnDeleteSelectedBookshelf1_Click(object sender, EventArgs e)
        {
            // TODO
        }
        #endregion

        #region Bookshelf 2 Controls
        private void btnRemoveItemBookshelf2_Click(object sender, EventArgs e)
        {
            var selectedItem = activeBookshelf2.SelectedItem;
            DeleteItemBasedOnSelectedItem(selectedItem);
        }

        private void btnEditSelectedBookshelf2_Click(object sender, EventArgs e)
        {
            var selectedItem = activeBookshelf2Lookup.SelectedItem;

            if (selectedItem == null || (selectedItem as Bookshelf) == null)
            {
                return;
            }

            editBookshelfDataHidden.Text = GenerateTemporaryBookshelfStorageKey((selectedItem as Bookshelf).Id, 2);
            editBookshelfTitleValue.Text = (selectedItem as Bookshelf).Name;
        }

        private void btnDeleteSelectedBookshelf2_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void DeleteItemBasedOnSelectedItem(object selectedItem)
        {
            if (selectedItem == null || (selectedItem as Item) == null)
            {
                return;
            }

            var result = _itemService.DeleteItem((selectedItem as Item).Id);

            if (result)
            {
                // Success: Remove from all possible areas
                itemLookup.Items.Remove(selectedItem);
                activeBookshelf1.Items.Remove(selectedItem);
                activeBookshelf2.Items.Remove(selectedItem);
                return;
            }
            else
            {
                // Failure: Notify user
                return;
            }
        }

        private string GenerateTemporaryBookshelfStorageKey(int bookshelfId, int activeBookshelfNumber)
        {
            return bookshelfId + "_" + activeBookshelfNumber;
        }

        private (string, string) DecodeTemporaryBookshelfStorageKey(string encodedString)
        {
            var split = encodedString.Split('_');

            if (split.Length != 2)
            {
                return (null, null);
            }

            return (split[0], split[1]);
        }
        #endregion

        #region Item Management
        private void btnAddLibraryItem_Click(object sender, EventArgs e)
        {
            var itemDto = getItemDtoFromAddItem(false);

            if (!validateItemDto(itemDto, false))
            {
                return;
            }

            var result = _itemService.CreateLibraryItem(itemDto);

            if (result != null)
            {
                // Success
                clearAddItemForm();
            }
            else
            {
                // Failure
            }
        }

        private void btnAddBookshelfItem_Click(object sender, EventArgs e)
        {
            var itemDto = getItemDtoFromAddItem(true);

            if (!validateItemDto(itemDto, true))
            {
                return;
            }

            var result = _itemService.CreateBookshelfItem(itemDto);

            if (result != null)
            {
                // Success
                clearAddItemForm();
            }
            else
            {
                // Failure
            }
        }

        private ItemDto getItemDtoFromAddItem(bool shouldAddBookshelfId)
        {
            return new ItemDto
            {
                Title = addItemTitleValue.Text,
                Description = addItemDescriptionValue.Text,
                Type = addItemTypeValue.Text,
                Date = addItemDateValue.Text,
                BookshelfId = shouldAddBookshelfId ? (activeBookshelf1.SelectedItem as Item)?.Id : (int?)null
            };
        }

        private void clearAddItemForm()
        {
            addItemTitleValue.Clear();
            addItemDescriptionValue.Clear();
            addItemTypeValue.ResetText();
            addItemDateValue.ResetText();
        }

        private void btnSaveItem_Click(object sender, EventArgs e)
        {
            var itemDto = getItemDtoFromEditItem(true);

            if (!validateItemDto(itemDto, true))
            {
                return;
            }

            // Perform Save Item
            // TODO
        }

        private void btnCancelItem_Click(object sender, EventArgs e)
        {
            clearEditItemForm();
        }

        private ItemDto getItemDtoFromEditItem(bool shouldAddBookshelfId)
        {
            return new ItemDto
            {
                Title = editItemTitleValue.Text,
                Description = editItemDescriptionValue.Text,
                Type = editItemTypeValue.Text,
                Date = editItemDateValue.Text,
                BookshelfId = shouldAddBookshelfId ? (activeBookshelf1.SelectedItem as Item)?.Id : (int?)null
            };
        }

        private void clearEditItemForm()
        {
            editItemTitleValue.Clear();
            editItemDescriptionValue.Clear();
            editItemTypeValue.ResetText();
            editItemDateValue.ResetText();
            editItemDataHidden.Clear();
        }

        private bool validateItemDto(ItemDto itemDto, bool shouldAddBookshelfId)
        {
            if (itemDto.Title == null ||
                itemDto.Description == null ||
                itemDto.Type == null ||
                !DataConstants.ItemConstants.ItemTypes.Contains(itemDto.Type) ||
                itemDto.Date == null ||
                (shouldAddBookshelfId && itemDto.BookshelfId == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Bookshelf Management
        private void btnAddBookshelf_Click(object sender, EventArgs e)
        {
            var title = addBookshelfTitleValue.Text;

            if (!string.IsNullOrWhiteSpace(title))
            {
                var result = _bookshelfService.CreateBookshelf(new BookshelfDto
                {
                    Title = title,
                });

                // On Success Clear values and Update Bookshelf Lookup
                if (result != null)
                {
                    addBookshelfTitleValue.Clear();

                    activeBookshelf1Lookup.Items.Add(result);
                    activeBookshelf2Lookup.Items.Add(result);
                }
            }
            else
            {
                return;
            }
        }

        private void btnSaveBookshelf_Click(object sender, EventArgs e)
        {
            // Need to have hidden value for which bookshelf was input
        }

        private void btnCancelBookshelf_Click(object sender, EventArgs e)
        {
            editBookshelfTitleValue.Clear();
            editBookshelfDataHidden.Clear();
        }
        #endregion

        #region Keyword Lookup
        private void btnKeywordLookup_Click(object sender, EventArgs e)
        {
            itemLookup.Items.Clear();

            var matchingItems = _itemService.GetItemsByKeyword(keywordLookupValue.Text);
            foreach(var matchingItem in matchingItems)
            {
                itemLookup.Items.Add(matchingItem);
            }
        }

        private void btnAddBookshelf1_Click(object sender, EventArgs e)
        {
            AssignItemToBookshelfFromLookup(1);
        }

        private void btnAddBookshelf2_Click(object sender, EventArgs e)
        {
            AssignItemToBookshelfFromLookup(2);
        }

        private void AssignItemToBookshelfFromLookup(int activeBookshelfNumber)
        {
            var selectedItem = itemLookup.SelectedItem;
            object selectedBookshelf = null;

            // Select Active Bookshelf 1 or 2
            if (activeBookshelfNumber == 1)
            {
                selectedBookshelf = activeBookshelf1Lookup.SelectedItem;
            }
            else if (activeBookshelfNumber == 2)
            {
                selectedBookshelf = activeBookshelf1Lookup.SelectedItem;
            }

            // Validation
            if (selectedItem == null || (selectedItem as Item) == null)
            {
                return;
            }

            if (selectedBookshelf == null || (selectedBookshelf as Bookshelf) == null)
            {
                return;
            }

            // Do nothing when trying to add to existing
            var originalBookshelfId = (selectedItem as Item).BookshelfId;
            var updatedBookshelfId = (selectedBookshelf as Bookshelf).Id;
            if (originalBookshelfId == updatedBookshelfId)
            {
                return;
            }

            // Assignment
            var result = _itemService.AssignItemToBookshelf(new ItemAssignmentDto
            {
                ItemId = (selectedItem as Item).Id,
                BookshelfId = updatedBookshelfId,
            });

            if (result == null)
            {
                // Notify of failure to assign item
                return;
            }

            // Update UI
            if (activeBookshelfNumber == 1)
            {
                activeBookshelf1.Items.Add(selectedItem);
            }
            else if (activeBookshelfNumber == 2)
            {
                activeBookshelf2.Items.Add(selectedItem);
            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            var selectedItem = itemLookup.SelectedItem;
            DeleteItemBasedOnSelectedItem(selectedItem);
        }
        #endregion

        #region Active Bookshelf Lookup
        private void activeBookshelf1Lookup_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectIndexChangeOnActiveBookshelfLookup(1);
        }

        private void activeBookshelf2Lookup_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectIndexChangeOnActiveBookshelfLookup(2);
        }

        private void SelectIndexChangeOnActiveBookshelfLookup(int activeBookshelfNumber)
        {
            object selectedItem = null;
            if (activeBookshelfNumber == 1)
            {
                selectedItem = activeBookshelf1Lookup.SelectedItem;
            }
            else if (activeBookshelfNumber == 2)
            {
                selectedItem = activeBookshelf2Lookup.SelectedItem;
            }

            if (selectedItem == null || (selectedItem as Bookshelf) == null)
            {
                return;
            }

            var itemsToAdd = _itemService.GetItemsByBookshelfId((selectedItem as Bookshelf).Id).ToArray();
            if (activeBookshelfNumber == 1)
            {
                activeBookshelf1.Items.Clear();
                activeBookshelf1.Items.AddRange(itemsToAdd);
                
            }
            else if (activeBookshelfNumber == 2)
            {
                activeBookshelf2.Items.Clear();
                activeBookshelf2.Items.AddRange(itemsToAdd);
            }
        }
        #endregion

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            var selectedItem = itemLookup.SelectedItem;

            if (selectedItem == null || (selectedItem as Item) == null)
            {
                return;
            }

            var item = selectedItem as Item;

            bool shouldUpdate1 = false;
            bool shouldUpdate2 = false;
            if (item.BookshelfId != null)
            {
                var bookshelf1 = activeBookshelf1Lookup.SelectedItem;
                var bookshelf2 = activeBookshelf2Lookup.SelectedItem;

                if (bookshelf1 != null && (bookshelf1 as Bookshelf) != null && (bookshelf1 as Bookshelf).Id == item.BookshelfId)
                {
                    shouldUpdate1 = true;
                }
                if (bookshelf2 != null && (bookshelf2 as Bookshelf) != null && (bookshelf2 as Bookshelf).Id == item.BookshelfId)
                {
                    shouldUpdate2 = true;
                }
            }

            editItemDataHidden.Text = GenerateTemporaryItemStorageKey(item.Id, shouldUpdate1, shouldUpdate2);
            editItemTitleValue.Text = item.Name;
            editItemDescriptionValue.Text = item.Description;
            editItemTypeValue.Text = item.Type;
            editItemDateValue.Value = item.Date ?? DateTime.Now;
        }

        private string GenerateTemporaryItemStorageKey(int itemId, bool shouldUpdate1, bool shouldUpdate2)
        {
            return itemId + "_" + (shouldUpdate1 ? "1" : "0") + "_" + (shouldUpdate2 ? "1" : "0");
        }

        private (string, string, string) DecodeTemporaryItemStorageKey(string encodedString)
        {
            var split = encodedString.Split('_');

            if (split.Length != 3)
            {
                return (null, null, null);
            }

            return (split[0], split[1], split[2]);
        }
    }
}

using DvdFormApp.DataTransferObjects;
using DvdFormApp.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DvdFormApp
{
    public partial class Form1 : Form
    {
        private IBookshelfService _bookshelfService;
        private IItemService _itemService;

        public Form1(IBookshelfService bookshelfService, IItemService itemService)
        {
            InitializeComponent();

            // Initialize Services
            _bookshelfService = bookshelfService;
            _itemService = itemService;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        #region Transfer Item Controls
        private void btnTransferOneToTwo_Click(object sender, EventArgs e)
        {

        }

        private void btnTransferTwoToOne_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Bookshelf 1 Controls
        private void btnRemoveItemBookshelf1_Click(object sender, EventArgs e)
        {

        }

        private void btnEditSelectedBookshelf1_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteSelectedBookshelf1_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Bookshelf 2 Controls
        private void btnRemoveItemBookshelf2_Click(object sender, EventArgs e)
        {

        }

        private void btnEditSelectedBookshelf2_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteSelectedBookshelf2_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Item Management
        private void btnAddLibraryItem_Click(object sender, EventArgs e)
        {
            var itemDto = getItemDtoFromAddItem(false);
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
            // Perform Save Item
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
        }
        #endregion

        #region Bookshelf Management
        private void btnAddBookshelf_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveBookshelf_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelBookshelf_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Keyword Lookup
        private void btnKeywordLookup_Click(object sender, EventArgs e)
        {
            itemLookup.Items.Clear();

            var matchingItems = _itemService.GetItemsByKeyword(keywordLookupValue.Text);
            foreach(var matchingItem in matchingItems)
            {
                itemLookup.Items.Add(matchingItem.Type + "-" + matchingItem.Name);
            }
        }

        private void btnAddBookshelf1_Click(object sender, EventArgs e)
        {

        }

        private void btnAddBookshelf2_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
        }
        #endregion
    }
}

using DvdFormApp.Services;
using System;
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

        }

        private void btnAddBookshelfItem_Click(object sender, EventArgs e)
        {

        }
        private void btnSaveItem_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelItem_Click(object sender, EventArgs e)
        {

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

        }

        private void btnAddBookshelf1_Click(object sender, EventArgs e)
        {

        }

        private void btnAddBookshelf2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}

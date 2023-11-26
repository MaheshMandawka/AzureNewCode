using DvdFormApp.Services;
using System;
using System.Windows.Forms;

namespace DvdFormApp
{
    public partial class Form1 : Form
    {
        private IItemService _itemService;

        public Form1(IItemService itemService)
        {
            InitializeComponent();
            _itemService = itemService;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void btnAddLibraryItem_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAddBookshelfItem_Click(object sender, EventArgs e)
        {

        }
    }
}

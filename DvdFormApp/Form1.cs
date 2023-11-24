using System;
using System.Linq;
using System.Windows.Forms;

namespace DvdFormApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) && !lstNames.Items.Contains(txtName.Text))
            {
                lstNames.Items.Add(txtName.Text);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var dbContext = new MediaContext();

            var nameToAdd = dbContext.Items.FirstOrDefault(x => x.Id == 1)?.Name;

            if (!string.IsNullOrWhiteSpace(nameToAdd) && !lstNames.Items.Contains(nameToAdd))
            {
                lstNames.Items.Add(nameToAdd);
            }
        }
    }
}

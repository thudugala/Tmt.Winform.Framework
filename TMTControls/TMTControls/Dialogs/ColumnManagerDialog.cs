using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.Dialogs
{
    public partial class ColumnManagerDialog : BaseDialog
    {
        public ColumnManagerDialog()
        {
            InitializeComponent();
        }

        public IList<ColumnData> GetColumnList()
        {
            var colList = new List<ColumnData>();

            foreach (ListViewItem item in listViewHiddenColumns.Items)
            {
                colList.Add(new ColumnData(item.Text, false));
            }
            foreach (ListViewItem item in listViewShownColumns.Items)
            {
                colList.Add(new ColumnData(item.Text, true));
            }

            return colList.AsReadOnly();
        }

        public void SetColumnList(IReadOnlyCollection<ColumnData> colList)
        {
            if (colList == null)
            {
                throw new ArgumentNullException(nameof(colList));
            }

            listViewShownColumns.Items.Clear();
            listViewHiddenColumns.Items.Clear();

            foreach (ColumnData col in colList)
            {
                if (col.Visibility)
                {
                    listViewShownColumns.Items.Add(col.Name);
                }
                else
                {
                    listViewHiddenColumns.Items.Add(col.Name);
                }
            }
        }

        private void ButtonHideAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewShownColumns.Items)
            {
                listViewHiddenColumns.Items.Add(item.Text);
            }
            listViewShownColumns.Items.Clear();
        }

        private void ButtonHideOne_Click(object sender, EventArgs e)
        {
            if (listViewShownColumns.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in listViewShownColumns.SelectedItems)
                {
                    listViewHiddenColumns.Items.Add(item.Text);
                    item.Remove();
                }
            }
        }

        private void ButtonShowAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHiddenColumns.Items)
            {
                listViewShownColumns.Items.Add(item.Text);
            }
            listViewHiddenColumns.Items.Clear();
        }

        private void ButtonShowOne_Click(object sender, EventArgs e)
        {
            if (listViewHiddenColumns.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in listViewHiddenColumns.SelectedItems)
                {
                    listViewShownColumns.Items.Add(item.Text);
                    item.Remove();
                }
            }
        }

        private void ColumnManagerDialog_Load(object sender, EventArgs e)
        {
            try
            {
                this.Image = IconChar.Columns.ToBitmap(72, Color.DarkSeaGreen);
            }
            catch
            {
            }
        }
    }
}
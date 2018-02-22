using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TMTControls.TMTDialogs
{
    public partial class TMTColumnManagerDialog : TMTDialog
    {
        public TMTColumnManagerDialog()
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

        private void tmtButtonHideAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewShownColumns.Items)
            {
                listViewHiddenColumns.Items.Add(item.Text);
            }
            listViewShownColumns.Items.Clear();
        }

        private void tmtButtonHideOne_Click(object sender, EventArgs e)
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

        private void tmtButtonShowAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewHiddenColumns.Items)
            {
                listViewShownColumns.Items.Add(item.Text);
            }
            listViewHiddenColumns.Items.Clear();
        }

        private void tmtButtonShowOne_Click(object sender, EventArgs e)
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

        private void TMTColumnManagerDialog_Load(object sender, EventArgs e)
        {
            var pro = new FontAwesome5.Properties(FontAwesome5.Type.Columns)
            {
                Size = 128,
                ForeColor = Color.DarkSeaGreen
            };
            this.Image = pro.AsImage();
        }
    }
}
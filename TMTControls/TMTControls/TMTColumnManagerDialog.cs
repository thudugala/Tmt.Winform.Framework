using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TMTControls
{
    public partial class TMTColumnManagerDialog : TMTDialog
    {
        public TMTColumnManagerDialog()
        {
            InitializeComponent();
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

        public List<ColumnData> GetColumnList()
        {
            List<ColumnData> colList = new List<ColumnData>();

            foreach (ListViewItem item in listViewHiddenColumns.Items)
            {
                colList.Add(new ColumnData(item.Text, false));
            }
            foreach (ListViewItem item in listViewShownColumns.Items)
            {
                colList.Add(new ColumnData(item.Text, true));
            }

            return colList;
        }

        public void SetColumnList(List<ColumnData> colList)
        {
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

        public class ColumnData
        {
            public string Name { get; set; }
            public bool Visibility { get; set; }

            public ColumnData(string name, bool visibility)
            {
                this.Name = name;
                this.Visibility = visibility;
            }
        }
    }
}
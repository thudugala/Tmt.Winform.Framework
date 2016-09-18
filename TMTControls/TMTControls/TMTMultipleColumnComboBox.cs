using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace TMTControls
{
    public class TMTMultipleColumnComboBox : ComboBox
    {
        public TMTMultipleColumnComboBox()
        {
            this.SuspendLayout();

            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.FlatStyle = FlatStyle.Flat;

            this.TabStop = false;

            this.DisplayMemberListToDisplay = new BindingList<String>();

            this.DrawItem += TMTMultipleColumnComboBox_DrawItem;

            this.ResumeLayout(false);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                base.DataSource = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataTable DataSourceTable
        {
            get
            {
                return this.DataSource as DataTable;
            }
            set
            {
                this.DataSource = value;
            }
        }

        [Category("Data"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BindingList<String> DisplayMemberListToDisplay { get; private set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new ComboBox.ObjectCollection Items
        {
            get
            {
                return base.Items;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new object Tag
        {
            get
            {
                return base.Tag;
            }
            set
            {
                base.Tag = value;
            }
        }

        private void TMTMultipleColumnComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the default background
            e.DrawBackground();

            if (e.Index > -1 && this.Items != null && this.Items[e.Index] is DataRowView)
            {
                // The ComboBox is bound to a DataTable,
                // so the items are DataRowView objects.
                DataRowView drv = this.Items[e.Index] as DataRowView;

                int displayMemberCount = this.DisplayMemberListToDisplay.Count;
                string columnToDisplay;
                string cellValue;
                Rectangle rectangleColumn = e.Bounds;
                rectangleColumn.Width /= this.DisplayMemberListToDisplay.Count;
                for (int i = 0; i < displayMemberCount; i++)
                {
                    columnToDisplay = this.DisplayMemberListToDisplay[i];

                    cellValue = drv[columnToDisplay].ToString();

                    // Get the bounds for the first column
                    rectangleColumn.X = i * rectangleColumn.Width;

                    // Draw the text on the first column
                    using (SolidBrush sb = new SolidBrush(e.ForeColor))
                    {
                        e.Graphics.DrawString(cellValue, e.Font, sb, rectangleColumn);
                    }

                    if (i + 1 < displayMemberCount)
                    {
                        // Draw a line to isolate the columns
                        using (Pen p = new Pen(Color.Black))
                        {
                            e.Graphics.DrawLine(p, rectangleColumn.Right, 0, rectangleColumn.Right, rectangleColumn.Bottom);
                        }
                    }
                }
            }
        }
    }
}
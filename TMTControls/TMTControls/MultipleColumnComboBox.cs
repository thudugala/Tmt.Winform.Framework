using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TMT.Controls.WinForms
{
    [ToolboxBitmap(typeof(ComboBox))]
    public class MultipleColumnComboBox : ComboBox
    {
        public MultipleColumnComboBox()
        {
            InitializeComponent();

            this.SuspendLayout();

            this.DisplayMemberListToDisplay = new BindingList<string>();
            this.ValueMemberList = new BindingList<string>();

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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new string DisplayMember
        {
            get
            {
                return base.DisplayMember;
            }
            set
            {
                base.DisplayMember = value;
            }
        }

        [Category("Data"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BindingList<string> DisplayMemberListToDisplay { get; }

        [DefaultValue(DrawMode.OwnerDrawFixed)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public new DrawMode DrawMode
        {
            get
            {
                return base.DrawMode;
            }
            set
            {
                base.DrawMode = value;
            }
        }

        [DefaultValue(FlatStyle.Flat)]
        [Localizable(true)]
        public new FlatStyle FlatStyle
        {
            get
            {
                return base.FlatStyle;
            }
            set
            {
                base.FlatStyle = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new ComboBox.ObjectCollection Items
        {
            get
            {
                return base.Items;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public object PreviousSelectedValue { get; set; }

        [DefaultValue(false)]
        [DispId(-516)]
        public new bool TabStop
        {
            get
            {
                return base.TabStop;
            }
            set
            {
                base.TabStop = value;
            }
        }

        [Category("Data"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BindingList<string> ValueMemberList { get; }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // MultipleColumnComboBox
            //
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TabStop = false;
            this.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.MultipleColumnComboBox_DrawItem);
            this.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.MultipleColumnComboBox_Format);
            this.ResumeLayout(false);
        }

        private void MultipleColumnComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the default background
            e.DrawBackground();

            if (e.Index <= -1 && this.Items == null)
            {
                return;
            }

            if (this.Items[e.Index] is DataRowView drv)
            {
                int displayMemberCount = this.DisplayMemberListToDisplay.Count;

                var rectangleColumn = e.Bounds;
                rectangleColumn.Width /= this.DisplayMemberListToDisplay.Count;
                for (int i = 0; i < displayMemberCount; i++)
                {
                    var columnToDisplay = this.DisplayMemberListToDisplay[i];
                    var cellValue = drv[columnToDisplay].ToString();

                    // Get the bounds for the first column
                    rectangleColumn.X = i * rectangleColumn.Width;

                    // Draw the text on the first column
                    using (var sb = new SolidBrush(e.ForeColor))
                    {
                        e.Graphics.DrawString(cellValue, e.Font, sb, rectangleColumn);
                    }

                    if (i + 1 < displayMemberCount)
                    {
                        // Draw a line to isolate the columns
                        using (var p = new Pen(Color.Black))
                        {
                            e.Graphics.DrawLine(p, rectangleColumn.Right, 0, rectangleColumn.Right, rectangleColumn.Bottom);
                        }
                    }
                }
            }
        }

        private void MultipleColumnComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is DataRowView drv)
            {
                var cellValueList = new List<string>();
                foreach (string columnToDisplay in this.DisplayMemberListToDisplay)
                {
                    cellValueList.Add(drv[columnToDisplay].ToString());
                }

                e.Value = string.Join(" | ", cellValueList);
            }
        }
    }
}
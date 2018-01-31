using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TMTControls
{
    [ToolboxBitmap(typeof(ComboBox))]
    public class TMTMultipleColumnComboBox : ComboBox
    {
        public TMTMultipleColumnComboBox()
        {
            InitializeComponent();

            this.SuspendLayout();

            this.DisplayMemberListToDisplay = new BindingList<string>();
            this.ValueMemberList = new BindingList<string>();

            this.ResumeLayout(false);
        }

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
        public BindingList<string> DisplayMemberListToDisplay { get; }

        [Category("Data"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BindingList<string> ValueMemberList { get; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new ComboBox.ObjectCollection Items
        {
            get
            {
                return base.Items;
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public object PreviousSelectedValue { get; set; }

        private void TMTMultipleColumnComboBox_DrawItem(object sender, DrawItemEventArgs e)
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

        private void TMTMultipleColumnComboBox_Format(object sender, ListControlConvertEventArgs e)
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // TMTMultipleColumnComboBox
            //
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TabStop = false;
            this.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TMTMultipleColumnComboBox_DrawItem);
            this.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.TMTMultipleColumnComboBox_Format);
            this.ResumeLayout(false);
        }
    }
}
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TMTControls
{
    public class TMTComboBox : ComboBox
    {
        private Label _connectedLabel;

        public TMTComboBox()
        {
            this.SuspendLayout();

            this.DbColumnType = TypeCode.String;
            this.FlatStyle = FlatStyle.Flat;

            this.ResumeLayout(false);
        }

        [Category("Design")]
        public Label ConnectedLabel
        {
            get
            {
                return this._connectedLabel;
            }
            set
            {
                this._connectedLabel = value;
                if (this._connectedLabel != null)
                {
                    this.GetDataSourceInformation().DbLabelText = this._connectedLabel.Text;
                }
            }
        }

        [Category("Data"), DefaultValue("")]
        public string DbColumnName
        {
            get
            {
                return this.GetDataSourceInformation().DbColumnName;
            }
            set
            {
                this.GetDataSourceInformation().DbColumnName = value;
            }
        }

        [Category("Data"), DefaultValue(TypeCode.String)]
        public TypeCode DbColumnType
        {
            get
            {
                return this.GetDataSourceInformation().DbColumnType;
            }
            set
            {
                this.GetDataSourceInformation().DbColumnType = value;
            }
        }

        [Category("Data"), DefaultValue(false)]
        public bool KeyColum
        {
            get
            {
                return this.GetDataSourceInformation().KeyColum;
            }
            set
            {
                this.GetDataSourceInformation().KeyColum = value;
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

        //private Color _borderColor = Color.Black;
        private Border3DStyle _borderStyle = Border3DStyle.Flat;

        private static int WM_PAINT = 0x000F;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {
                Graphics g = Graphics.FromHwnd(Handle);
                Rectangle bounds = new Rectangle(0, 0, Width, Height);
                ControlPaint.DrawBorder3D(g, bounds, _borderStyle);
            }
        }

        [Category("Appearance")]
        public Border3DStyle BorderStyle
        {
            get { return _borderStyle; }
            set
            {
                _borderStyle = value;
                Invalidate();
            }
        }
    }
}
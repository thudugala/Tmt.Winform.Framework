using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TMTControls
{
    public class TMTComboBox : ComboBox
    {
        public TMTComboBox()
        {
            this.SuspendLayout();

            this.DbColumnType = TypeCode.String;
            this.FlatStyle = FlatStyle.Flat;

            this.ResumeLayout(false);
        }

        [Category("Design")]
        public Label ConnectedLabel { get; set; }

        [Category("Data"), DefaultValue("")]
        public string DbColumnName { get; set; }

        [Category("Data"), DefaultValue(TypeCode.String)]
        public TypeCode DbColumnType { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool KeyColum { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool MandatoryColum { get; set; }

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
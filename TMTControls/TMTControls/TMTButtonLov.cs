using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TMTControls
{
    public class TMTButtonLov : Button
    {
        private Control _connectedControl;

        public TMTButtonLov()
        {
            this.InitializeComponent();
        }

        [Category("Design")]
        public Control ConnectedBox
        {
            get
            {
                return this._connectedControl;
            }
            set
            {
                if (value is TextBox || value is NumericUpDown)
                {
                    this._connectedControl = value;
                }
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TMTButtonLov
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.FlatAppearance.BorderSize = 0;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.Text = "...";
            this.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.UseVisualStyleBackColor = false;
            this.ResumeLayout(false);

        }
    }
}
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TMTControls
{
    [DefaultProperty("PanelName")]
    public class TMTTileButton : Button
    {
        public TMTTileButton()
        {
            InitializeComponent();

            this.SuspendLayout();
            this.SetTheme();
            this.ResumeLayout(false);
        }

        public void SetTheme()
        {
            this.BackColor = Properties.Settings.Default.TileButtonBackColor;
            this.ForeColor = Properties.Settings.Default.TileButtonForeColor;
            this.FlatAppearance.BorderColor = this.BackColor;
        }

        [DefaultValue(ContentAlignment.BottomCenter)]
        public override ContentAlignment TextAlign
        {
            get
            {
                return base.TextAlign;
            }
            set
            {
                base.TextAlign = value;
            }
        }

        [DefaultValue(TextImageRelation.ImageAboveText)]
        [Localizable(true)]
        public new TextImageRelation TextImageRelation
        {
            get
            {
                return base.TextImageRelation;
            }
            set
            {
                base.TextImageRelation = value;
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

        [Localizable(true)]
        [DefaultValue(typeof(Padding), "20, 20, 20, 20")]
        public new Padding Margin
        {
            get
            {
                return base.Margin;
            }
            set
            {
                base.Margin = value;
            }
        }

        [Localizable(true)]
        [DefaultValue(typeof(Size), "150, 160")]
        public new Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value;
            }
        }

        [Category("TMT")]
        public string PanelName { get; set; }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // TMTTileButton
            //
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Margin = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(150, 160);
            this.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.UseVisualStyleBackColor = false;
            this.ResumeLayout(false);
        }
    }
}
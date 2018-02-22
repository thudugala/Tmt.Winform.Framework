using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TMTControls
{
    public class IconButton : Button
    {
        public IconButton()
        {
            InitializeComponent();

            this.SuspendLayout();

            this.IconSize = 48;
            this.IconLocation = Point.Empty;

            this.ResumeLayout(false);
        }

        [Category("Appearance"), DefaultValue(FlatStyle.Flat)]
        public new FlatStyle FlatStyle
        {
            get => base.FlatStyle;
            set => base.FlatStyle = value;
        }

        [Category("Icon"), DefaultValue(null)]
        public Color IconBackColor { get; set; }

        [Category("Icon"), DefaultValue(null)]
        public Color IconBorderColor { get; set; }

        [Category("Icon"), DefaultValue(0)]
        public int IconBorderSize { get; set; }

        [Category("Icon"), DefaultValue(FontAwesome5.Brand.None)]
        public FontAwesome5.Brand IconBrand { get; set; }

        [Category("Icon"), DefaultValue(null)]
        public Color IconForeColor { get; set; }

        [Category("Icon")]
        public Point IconLocation { get; set; }

        [Category("Icon"), DefaultValue(RotateFlipType.RotateNoneFlipNone)]
        public RotateFlipType IconRotateFlip { get; set; }

        [Category("Icon"), DefaultValue(false)]
        public bool IconShowBorder { get; set; }

        [Category("Icon"), DefaultValue(48)]
        public int IconSize { get; set; }

        [Category("Icon"), DefaultValue(FontAwesome5.Type.None)]
        public FontAwesome5.Type IconType { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Image Image
        {
            get => base.Image;
        }

        public void UpdateIcon()
        {
            var iconProperties = new FontAwesome5.Properties(this.IconType)
            {
                BackColor = this.IconBackColor,
                BorderSize = this.IconBorderSize,
                BorderColor = this.IconBorderColor,
                Brand = this.IconBrand,
                ForeColor = this.IconForeColor,
                Location = this.IconLocation,
                RotateFlip = this.IconRotateFlip,
                ShowBorder = this.IconShowBorder,
                Size = this.IconSize,
            };
            base.Text = string.Empty;
            base.Image = iconProperties.AsImage();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // IconButton
            //
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResumeLayout(false);
        }
    }
}
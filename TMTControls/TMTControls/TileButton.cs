using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace TMT.Controls.WinForms
{
    [DefaultProperty(nameof(NavigatePanel))]
    public class TileButton : Button
    {
        public TileButton()
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

        [Category("Design")]
        [Editor(typeof(UserControlEditor), typeof(UITypeEditor))]
        public Type NavigatePanel { get; set; }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // TileButton
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
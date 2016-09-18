using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Mail;
using System.Windows.Forms;

namespace TMTControls
{
    public class TMTTextButtonBox : TMTTextButtonBoxBase
    {
        private Label _connectedLabel;

        public TMTTextButtonBox()
        {
            InitializeComponent();

            this.DbColumnType = TypeCode.String;
            this.LovText = string.Empty;
            this.ValidateType = MaskValidateType.None;
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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue("")]
        public string LovText
        {
            get
            {
                return this.GetDataSourceInformation().LovText;
            }
            set
            {
                this.GetDataSourceInformation().LovText = value;
            }
        }

        [Category("Data"), DefaultValue("")]
        public string LovViewName
        {
            get
            {
                return this.GetDataSourceInformation().LovViewName;
            }
            set
            {
                this.GetDataSourceInformation().LovViewName = value;
            }
        }

        [Category("Data"), DefaultValue(false)]
        public bool MandatoryColum
        {
            get
            {
                return this.GetDataSourceInformation().MandatoryColum;
            }
            set
            {
                this.GetDataSourceInformation().MandatoryColum = value;
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

        [Category("Design"), DefaultValue(MaskValidateType.None)]
        public MaskValidateType ValidateType { get; set; }
                
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // InnerTextBox
            // 
            this.InnerTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.InnerTextBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.InnerTextBox.Size = new System.Drawing.Size(172, 26);
            this.InnerTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.InnerTextBox_Validating);
            // 
            // TMTTextButtonBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.Name = "TMTTextButtonBox";
            this.Size = new System.Drawing.Size(200, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void InnerTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.ValidateType == MaskValidateType.Email)
                {
                    this.Text = new MailAddress(this.Text).Address;
                    //email address is valid since the above line has not thrown an exception

                    this.BackColor = Color.Empty;
                }
            }
            catch (Exception)
            {
                this.BackColor = Color.Red;
            }
        }
    }
}
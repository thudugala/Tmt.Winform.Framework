using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Mail;
using System.Windows.Forms;

namespace TMTControls
{
    public class TMTTextBox : TextBox
    {
        private Label _connectedLabel;

        public TMTTextBox()
        {
            this.SuspendLayout();

            this.DbColumnType = TypeCode.String;
            this.LovText = string.Empty;
            this.ValidateType = MaskValidateType.None;

            this.Validating += TMTTextBox_Validating;

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

        private void TMTTextBox_Validating(object sender, CancelEventArgs e)
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
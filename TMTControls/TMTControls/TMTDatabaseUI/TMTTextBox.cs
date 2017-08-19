using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Mail;
using System.Windows.Forms;

namespace TMTControls.TMTDatabaseUI
{
    public class TMTTextBox : TextBox, ITMTDatabaseUIControl
    {
        public TMTTextBox()
        {
            this.SuspendLayout();

            this.DbColumnType = TypeCode.String;
            this.ValidateType = MaskValidateType.None;

            this.Validating += TMTTextBox_Validating;

            this.ResumeLayout(false);
        }

        [Category("Design")]
        public Label ConnectedLabel { get; set; }

        [Category("Data"), DefaultValue("")]
        public string DbColumnName { get; set; }

        [Category("Data"), DefaultValue(TypeCode.String)]
        public TypeCode DbColumnType { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool KeyColumn { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool MandatoryColumn { get; set; }

        [Category("Design"), DefaultValue(MaskValidateType.None)]
        public MaskValidateType ValidateType { get; set; }

        public string GetLableText()
        {            
            return this.ConnectedLabel?.Text;
        }

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
            catch
            {
                this.BackColor = Color.Red;
            }
        }

        public Type GetDbColumnSystemType()
        {
            return Type.GetType("System." + this.DbColumnType);
        }
    }
}
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net.Mail;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.DatabaseUI
{
    [ToolboxBitmap(typeof(TextBox))]
    public class DbTextBox : TextBox, IDbControl
    {
        private HashSet<char> allowedKeySet = new HashSet<char>() { '.', ',', };

        public DbTextBox()
        {
            InitializeComponent();

            this.SuspendLayout();

            this.DbColumnType = TypeCode.String;
            this.ValidateType = MaskValidateType.None;
            this.CountryCode = "lk";

            this.ResumeLayout(false);
        }

        [Category("Design")]
        public Label ConnectedLabel { get; set; }

        [Category("Design"), DefaultValue("lk")]
        public string CountryCode { get; set; }

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

        public Type GetDbColumnSystemType()
        {
            return Type.GetType("System." + this.DbColumnType);
        }

        public string GetLableText()
        {
            return this.ConnectedLabel?.Text;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // DbTextBox
            //
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DbTextBox_KeyPress);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.DbTextBox_Validating);
            this.ResumeLayout(false);
        }

        private void DbTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.ValidateType == MaskValidateType.Double)
            {
                if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) ||
                    (this.allowedKeySet.Contains(e.KeyChar) && this.Text.IndexOf(e.KeyChar) < 0))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else if (this.ValidateType == MaskValidateType.Int)
            {
                if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = false;
            }
        }

        private void DbTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.ValidateType == MaskValidateType.Email)
                {
                    this.Text = new MailAddress(this.Text).Address;
                    //email address is valid since the above line has not thrown an exception
                }
                else if (this.ValidateType == MaskValidateType.Phone)
                {                    
                    this.Text = PhoneNumberUtil.GetInstance().Parse(this.Text, this.CountryCode).NationalNumber.ToString();
                }
                this.BackColor = Color.Empty;
            }
            catch
            {
                this.BackColor = Color.Red;
            }
        }
    }
}
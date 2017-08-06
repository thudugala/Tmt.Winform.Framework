using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Mail;
using System.Windows.Forms;

namespace TMTControls
{
    public class TMTTextButtonBox : TMTTextButtonBoxBase
    {
        public TMTTextButtonBox()
        {
            InitializeComponent();

            this.SuspendLayout();

            this.DbColumnType = TypeCode.String;
            this.LovText = string.Empty;
            this.LovViewName = string.Empty;
            this.ValidateType = MaskValidateType.None;

            this.ResumeLayout(false);
        }

        [Category("TMT Data")]
        public event LovLoadedEventHandler LovLoaded;

        [Category("TMT Data")]
        public event LovLoadingEventHandler LovLoading;

        [Category("Design")]
        public Label ConnectedLabel { get; set; }

        [Category("Data"), DefaultValue("")]
        public string DbColumnName { get; set; }

        [Category("Data"), DefaultValue(TypeCode.String)]
        public TypeCode DbColumnType { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool KeyColum { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue("")]
        public string LovText { get; set; }

        [Category("Data"), DefaultValue("")]
        public string LovViewName { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool MandatoryColum { get; set; }

        [Category("Design"), DefaultValue(MaskValidateType.None)]
        public MaskValidateType ValidateType { get; set; }

        public void GetLovSelectedRow(LovLoadingEventArgs rowEvent)
        {
            LovLoading?.Invoke(this, rowEvent);
        }

        public void SetLovSelectedRow(LovLoadedEventArgs rowEvent)
        {
            LovLoaded?.Invoke(this, rowEvent);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // InnerTextBox
            //
            this.InnerTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.InnerTextBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.InnerTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.InnerTextBox_Validating);
            //
            // TMTTextButtonBox
            //
            this.Name = "TMTTextButtonBox";
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
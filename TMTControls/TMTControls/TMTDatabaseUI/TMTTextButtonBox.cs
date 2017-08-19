using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Mail;
using System.Windows.Forms;

namespace TMTControls.TMTDatabaseUI
{
    public class TMTTextButtonBox : TMTTextButtonBoxBase, ITMTDatabaseUIControl
    {
        public TMTTextButtonBox()
        {
            InitializeComponent();

            this.SuspendLayout();

            this.DbColumnType = TypeCode.String;
            this.ListOfValueText = string.Empty;
            this.ListOfValueViewName = string.Empty;
            this.ValidateType = MaskValidateType.None;

            this.ResumeLayout(false);
        }

        [Category("TMT Data")]
        public event EventHandler<ListOfValueLoadedEventArgs> ListOfValueLoaded;

        [Category("TMT Data")]
        public event EventHandler<ListOfValueLoadingEventArgs> ListOfValueLoading;

        [Category("Design")]
        public Label ConnectedLabel { get; set; }

        [Category("Data"), DefaultValue("")]
        public string DbColumnName { get; set; }

        [Category("Data"), DefaultValue(TypeCode.String)]
        public TypeCode DbColumnType { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool KeyColumn { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue("")]
        public string ListOfValueText { get; set; }

        [Category("Data"), DefaultValue("")]
        public string ListOfValueViewName { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool MandatoryColumn { get; set; }

        [Category("Design"), DefaultValue(MaskValidateType.None)]
        public MaskValidateType ValidateType { get; set; }

        public void GetListOfValueSelectedRow(ListOfValueLoadingEventArgs rowEvent)
        {
            ListOfValueLoading?.Invoke(this, rowEvent);
        }

        public void SetListOfValueSelectedRow(ListOfValueLoadedEventArgs rowEvent)
        {
            ListOfValueLoaded?.Invoke(this, rowEvent);
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
            catch
            {
                this.BackColor = Color.Red;
            }
        }

        public string GetLableText()
        {            
            return this.ConnectedLabel?.Text;
        }

        public Type GetDbColumnSystemType()
        {
            return Type.GetType("System." + this.DbColumnType);
        }
    }
}
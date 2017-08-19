namespace TMTControls
{
    partial class TMTDateTimePickerDropDown
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TMTDateTimePickerDropDown));
            this.monthCalendarMain = new System.Windows.Forms.MonthCalendar();
            this.SuspendLayout();
            // 
            // monthCalendarMain
            // 
            resources.ApplyResources(this.monthCalendarMain, "monthCalendarMain");
            this.monthCalendarMain.Name = "monthCalendarMain";
            this.monthCalendarMain.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarMain_DateSelected);
            // 
            // TMTDateTimePickerDropDown
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.monthCalendarMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TMTDateTimePickerDropDown";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.MonthCalendar monthCalendarMain;
    }
}
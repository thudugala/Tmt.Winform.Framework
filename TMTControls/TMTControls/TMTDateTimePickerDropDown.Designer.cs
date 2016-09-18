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
            this.monthCalendarMain = new System.Windows.Forms.MonthCalendar();
            this.SuspendLayout();
            // 
            // monthCalendarMain
            // 
            this.monthCalendarMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monthCalendarMain.Location = new System.Drawing.Point(0, 0);
            this.monthCalendarMain.Margin = new System.Windows.Forms.Padding(0);
            this.monthCalendarMain.Name = "monthCalendarMain";
            this.monthCalendarMain.TabIndex = 1;
            this.monthCalendarMain.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarMain_DateSelected);
            // 
            // TMTDateTimePickerDropDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 162);
            this.Controls.Add(this.monthCalendarMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "TMTDateTimePickerDropDown";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.MonthCalendar monthCalendarMain;
    }
}
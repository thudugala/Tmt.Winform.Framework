namespace TMT.Controls.WinForms
{
    partial class DateTimePickerForSearch
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateTimePickerForSearch));
            this.textBoxMain = new System.Windows.Forms.TextBox();
            this.buttonDorpDown = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // textBoxMain
            // 
            this.textBoxMain.AutoCompleteCustomSource.AddRange(new string[] {
            resources.GetString("textBoxMain.AutoCompleteCustomSource"),
            resources.GetString("textBoxMain.AutoCompleteCustomSource1"),
            resources.GetString("textBoxMain.AutoCompleteCustomSource2"),
            resources.GetString("textBoxMain.AutoCompleteCustomSource3"),
            resources.GetString("textBoxMain.AutoCompleteCustomSource4"),
            resources.GetString("textBoxMain.AutoCompleteCustomSource5")});
            this.textBoxMain.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxMain, "textBoxMain");
            this.textBoxMain.Name = "textBoxMain";
            this.textBoxMain.Enter += new System.EventHandler(this.textBoxMain_Enter);
            // 
            // buttonDorpDown
            // 
            resources.ApplyResources(this.buttonDorpDown, "buttonDorpDown");
            this.buttonDorpDown.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonDorpDown.FlatAppearance.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.buttonDorpDown.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.buttonDorpDown.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonDorpDown.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.buttonDorpDown.IconChar = FontAwesome.Sharp.IconChar.CaretDown;
            this.buttonDorpDown.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(117)))), ((int)(((byte)(142)))));
            this.buttonDorpDown.IconSize = 18;
            this.buttonDorpDown.Name = "buttonDorpDown";
            this.buttonDorpDown.Rotation = 0D;
            this.buttonDorpDown.TabStop = false;
            this.buttonDorpDown.UseVisualStyleBackColor = false;
            this.buttonDorpDown.Click += new System.EventHandler(this.buttonDorpDown_Click);
            // 
            // DateTimePickerForSearch
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxMain);
            this.Controls.Add(this.buttonDorpDown);
            this.Name = "DateTimePickerForSearch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxMain;
        private FontAwesome.Sharp.IconButton buttonDorpDown;
    }
}

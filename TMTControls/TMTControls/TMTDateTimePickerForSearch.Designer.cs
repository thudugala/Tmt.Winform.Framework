namespace TMTControls
{
    partial class TMTDateTimePickerForSearch
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
            this.textBoxMain = new System.Windows.Forms.TextBox();
            this.buttonDorpDown = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxMain
            // 
            this.textBoxMain.AutoCompleteCustomSource.AddRange(new string[] {
            "!=",
            ">",
            ">=",
            "<",
            "<=",
            ";"});
            this.textBoxMain.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMain.Location = new System.Drawing.Point(0, 0);
            this.textBoxMain.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.textBoxMain.Multiline = true;
            this.textBoxMain.Name = "textBoxMain";
            this.textBoxMain.Size = new System.Drawing.Size(72, 20);
            this.textBoxMain.TabIndex = 3;
            this.textBoxMain.Enter += new System.EventHandler(this.textBoxMain_Enter);
            // 
            // buttonDorpDown
            // 
            this.buttonDorpDown.AutoSize = true;
            this.buttonDorpDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonDorpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonDorpDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonDorpDown.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonDorpDown.FlatAppearance.BorderSize = 0;
            this.buttonDorpDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDorpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDorpDown.Location = new System.Drawing.Point(74, 0);
            this.buttonDorpDown.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.buttonDorpDown.Name = "buttonDorpDown";
            this.buttonDorpDown.Size = new System.Drawing.Size(26, 27);
            this.buttonDorpDown.TabIndex = 4;
            this.buttonDorpDown.Text = "˅";
            this.buttonDorpDown.UseVisualStyleBackColor = false;
            this.buttonDorpDown.Click += new System.EventHandler(this.buttonDorpDown_Click);
            // 
            // TMTDateTimePickerForSearch
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.textBoxMain);
            this.Controls.Add(this.buttonDorpDown);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TMTDateTimePickerForSearch";
            this.Size = new System.Drawing.Size(100, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxMain;
        private System.Windows.Forms.Button buttonDorpDown;
    }
}

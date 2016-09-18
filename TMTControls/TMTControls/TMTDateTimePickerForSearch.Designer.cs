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
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxMain = new System.Windows.Forms.TextBox();
            this.buttonDorpDown = new System.Windows.Forms.Button();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.AutoSize = true;
            this.tableLayoutPanelMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.Controls.Add(this.textBoxMain, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonDorpDown, 1, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(150, 26);
            this.tableLayoutPanelMain.TabIndex = 6;
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
            this.textBoxMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMain.Location = new System.Drawing.Point(0, 0);
            this.textBoxMain.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.textBoxMain.Name = "textBoxMain";
            this.textBoxMain.Size = new System.Drawing.Size(122, 26);
            this.textBoxMain.TabIndex = 3;
            this.textBoxMain.Enter += new System.EventHandler(this.textBoxMain_Enter);
            // 
            // buttonDorpDown
            // 
            this.buttonDorpDown.AutoSize = true;
            this.buttonDorpDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonDorpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonDorpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDorpDown.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonDorpDown.FlatAppearance.BorderSize = 0;
            this.buttonDorpDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDorpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDorpDown.Location = new System.Drawing.Point(124, 0);
            this.buttonDorpDown.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.buttonDorpDown.Name = "buttonDorpDown";
            this.buttonDorpDown.Size = new System.Drawing.Size(26, 26);
            this.buttonDorpDown.TabIndex = 4;
            this.buttonDorpDown.Text = "˅";
            this.buttonDorpDown.UseVisualStyleBackColor = false;
            this.buttonDorpDown.Click += new System.EventHandler(this.buttonDorpDown_Click);
            // 
            // TMTDateTimePickerForSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "TMTDateTimePickerForSearch";
            this.Size = new System.Drawing.Size(150, 26);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TextBox textBoxMain;
        private System.Windows.Forms.Button buttonDorpDown;
    }
}

namespace TMTControls.TMTPanels
{
    partial class BaseWindow
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
            this.buttonChangeColumnVisibility = new TMTControls.IconButton();
            this.buttonClear = new TMTControls.IconButton();
            this.buttonDelete = new TMTControls.IconButton();
            this.buttonDuplicate = new TMTControls.IconButton();
            this.buttonNew = new TMTControls.IconButton();
            this.buttonSave = new TMTControls.IconButton();
            this.buttonRefresh = new TMTControls.IconButton();
            this.buttonSearch = new TMTControls.IconButton();
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonChangeColumnVisibility
            // 
            this.buttonChangeColumnVisibility.AutoSize = true;
            this.buttonChangeColumnVisibility.Enabled = false;
            this.buttonChangeColumnVisibility.FlatAppearance.BorderSize = 0;
            this.buttonChangeColumnVisibility.IconBackColor = System.Drawing.Color.Empty;
            this.buttonChangeColumnVisibility.IconBorderColor = System.Drawing.Color.Empty;
            this.buttonChangeColumnVisibility.IconForeColor = System.Drawing.Color.DarkSeaGreen;
            this.buttonChangeColumnVisibility.IconLocation = new System.Drawing.Point(0, 0);
            this.buttonChangeColumnVisibility.IconType = FontAwesome5.Type.Columns;
            this.buttonChangeColumnVisibility.Location = new System.Drawing.Point(0, 378);
            this.buttonChangeColumnVisibility.Margin = new System.Windows.Forms.Padding(0);
            this.buttonChangeColumnVisibility.Name = "buttonChangeColumnVisibility";
            this.buttonChangeColumnVisibility.Size = new System.Drawing.Size(54, 54);
            this.buttonChangeColumnVisibility.TabIndex = 7;
            this.buttonChangeColumnVisibility.TabStop = false;
            this.buttonChangeColumnVisibility.Text = "CV";
            this.toolTipBase.SetToolTip(this.buttonChangeColumnVisibility, "Column Visibility Manager");
            this.buttonChangeColumnVisibility.UseVisualStyleBackColor = true;
            this.buttonChangeColumnVisibility.Click += new System.EventHandler(this.ButtonChangeColumnVisibility_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.AutoSize = true;
            this.buttonClear.Enabled = false;
            this.buttonClear.FlatAppearance.BorderSize = 0;
            this.buttonClear.IconBackColor = System.Drawing.Color.Empty;
            this.buttonClear.IconBorderColor = System.Drawing.Color.Empty;
            this.buttonClear.IconForeColor = System.Drawing.Color.DarkSeaGreen;
            this.buttonClear.IconLocation = new System.Drawing.Point(0, 0);
            this.buttonClear.IconType = FontAwesome5.Type.Eraser;
            this.buttonClear.Location = new System.Drawing.Point(0, 324);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(0);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(54, 54);
            this.buttonClear.TabIndex = 6;
            this.buttonClear.TabStop = false;
            this.buttonClear.Text = "C";
            this.toolTipBase.SetToolTip(this.buttonClear, "Clear");
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.AutoSize = true;
            this.buttonDelete.Enabled = false;
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.IconBackColor = System.Drawing.Color.Empty;
            this.buttonDelete.IconBorderColor = System.Drawing.Color.Empty;
            this.buttonDelete.IconForeColor = System.Drawing.Color.IndianRed;
            this.buttonDelete.IconLocation = new System.Drawing.Point(0, 0);
            this.buttonDelete.IconType = FontAwesome5.Type.Trash;
            this.buttonDelete.Location = new System.Drawing.Point(0, 270);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(54, 54);
            this.buttonDelete.TabIndex = 5;
            this.buttonDelete.TabStop = false;
            this.buttonDelete.Text = "D";
            this.toolTipBase.SetToolTip(this.buttonDelete, "Delete (F7)");
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // buttonDuplicate
            // 
            this.buttonDuplicate.AutoSize = true;
            this.buttonDuplicate.Enabled = false;
            this.buttonDuplicate.FlatAppearance.BorderSize = 0;
            this.buttonDuplicate.IconBackColor = System.Drawing.Color.Empty;
            this.buttonDuplicate.IconBorderColor = System.Drawing.Color.Empty;
            this.buttonDuplicate.IconForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(189)))), ((int)(((byte)(167)))));
            this.buttonDuplicate.IconLocation = new System.Drawing.Point(0, 0);
            this.buttonDuplicate.IconType = FontAwesome5.Type.Copy;
            this.buttonDuplicate.Location = new System.Drawing.Point(0, 216);
            this.buttonDuplicate.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDuplicate.Name = "buttonDuplicate";
            this.buttonDuplicate.Size = new System.Drawing.Size(54, 54);
            this.buttonDuplicate.TabIndex = 4;
            this.buttonDuplicate.TabStop = false;
            this.buttonDuplicate.Text = "DP";
            this.toolTipBase.SetToolTip(this.buttonDuplicate, "Duplicate");
            this.buttonDuplicate.UseVisualStyleBackColor = true;
            this.buttonDuplicate.Click += new System.EventHandler(this.ButtonDuplicate_Click);
            // 
            // buttonNew
            // 
            this.buttonNew.AutoSize = true;
            this.buttonNew.Enabled = false;
            this.buttonNew.FlatAppearance.BorderSize = 0;
            this.buttonNew.IconBackColor = System.Drawing.Color.Empty;
            this.buttonNew.IconBorderColor = System.Drawing.Color.Empty;
            this.buttonNew.IconForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(189)))), ((int)(((byte)(167)))));
            this.buttonNew.IconLocation = new System.Drawing.Point(0, 0);
            this.buttonNew.IconType = FontAwesome5.Type.PlusCircle;
            this.buttonNew.Location = new System.Drawing.Point(0, 162);
            this.buttonNew.Margin = new System.Windows.Forms.Padding(0);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(54, 54);
            this.buttonNew.TabIndex = 3;
            this.buttonNew.TabStop = false;
            this.buttonNew.Text = "N";
            this.toolTipBase.SetToolTip(this.buttonNew, "New (F5)");
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.ButtonNew_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.AutoSize = true;
            this.buttonSave.Enabled = false;
            this.buttonSave.FlatAppearance.BorderSize = 0;
            this.buttonSave.IconBackColor = System.Drawing.Color.Empty;
            this.buttonSave.IconBorderColor = System.Drawing.Color.Empty;
            this.buttonSave.IconForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(72)))), ((int)(((byte)(152)))));
            this.buttonSave.IconLocation = new System.Drawing.Point(0, 0);
            this.buttonSave.IconType = FontAwesome5.Type.Save;
            this.buttonSave.Location = new System.Drawing.Point(0, 108);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(54, 54);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.TabStop = false;
            this.buttonSave.Text = "S";
            this.toolTipBase.SetToolTip(this.buttonSave, "Save (F12)");
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.AutoSize = true;
            this.buttonRefresh.Enabled = false;
            this.buttonRefresh.FlatAppearance.BorderSize = 0;
            this.buttonRefresh.IconBackColor = System.Drawing.Color.Empty;
            this.buttonRefresh.IconBorderColor = System.Drawing.Color.Empty;
            this.buttonRefresh.IconForeColor = System.Drawing.Color.CornflowerBlue;
            this.buttonRefresh.IconLocation = new System.Drawing.Point(0, 0);
            this.buttonRefresh.IconType = FontAwesome5.Type.Redo;
            this.buttonRefresh.Location = new System.Drawing.Point(0, 54);
            this.buttonRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(54, 54);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.TabStop = false;
            this.buttonRefresh.Text = "R";
            this.toolTipBase.SetToolTip(this.buttonRefresh, "Refresh");
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.AutoSize = true;
            this.buttonSearch.FlatAppearance.BorderSize = 0;
            this.buttonSearch.IconBackColor = System.Drawing.Color.Empty;
            this.buttonSearch.IconBorderColor = System.Drawing.Color.Empty;
            this.buttonSearch.IconForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(134)))), ((int)(((byte)(244)))));
            this.buttonSearch.IconLocation = new System.Drawing.Point(0, 0);
            this.buttonSearch.IconType = FontAwesome5.Type.Search;
            this.buttonSearch.Location = new System.Drawing.Point(0, 0);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(54, 54);
            this.buttonSearch.TabIndex = 0;
            this.buttonSearch.TabStop = false;
            this.buttonSearch.Text = "SCH";
            this.toolTipBase.SetToolTip(this.buttonSearch, "Search (F3)");
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // flowLayoutPanelButtons
            // 
            this.flowLayoutPanelButtons.BackColor = System.Drawing.SystemColors.MenuBar;
            this.flowLayoutPanelButtons.Controls.Add(this.buttonSearch);
            this.flowLayoutPanelButtons.Controls.Add(this.buttonRefresh);
            this.flowLayoutPanelButtons.Controls.Add(this.buttonSave);
            this.flowLayoutPanelButtons.Controls.Add(this.buttonNew);
            this.flowLayoutPanelButtons.Controls.Add(this.buttonDuplicate);
            this.flowLayoutPanelButtons.Controls.Add(this.buttonDelete);
            this.flowLayoutPanelButtons.Controls.Add(this.buttonClear);
            this.flowLayoutPanelButtons.Controls.Add(this.buttonChangeColumnVisibility);
            this.flowLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(0, 54);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(54, 524);
            this.flowLayoutPanelButtons.TabIndex = 2;
            // 
            // BaseWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.Controls.Add(this.flowLayoutPanelButtons);
            this.Name = "BaseWindow";
            this.Load += new System.EventHandler(this.BaseWindow_Load);
            this.Controls.SetChildIndex(this.flowLayoutPanelButtons, 0);
            this.flowLayoutPanelButtons.ResumeLayout(false);
            this.flowLayoutPanelButtons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private IconButton buttonSearch;
        private IconButton buttonRefresh;
        private IconButton buttonSave;
        private IconButton buttonClear;
        private IconButton buttonDuplicate;
        private IconButton buttonDelete;
        private IconButton buttonNew;
        private IconButton buttonChangeColumnVisibility;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
    }
}

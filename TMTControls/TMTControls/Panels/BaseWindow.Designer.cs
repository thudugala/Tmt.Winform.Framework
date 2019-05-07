namespace TMT.Controls.WinForms.Panels
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
            this.buttonChangeColumnVisibility = new FontAwesome.Sharp.IconButton();
            this.buttonClear = new FontAwesome.Sharp.IconButton();
            this.buttonDelete = new FontAwesome.Sharp.IconButton();
            this.buttonDuplicate = new FontAwesome.Sharp.IconButton();
            this.buttonNew = new FontAwesome.Sharp.IconButton();
            this.buttonSave = new FontAwesome.Sharp.IconButton();
            this.buttonRefresh = new FontAwesome.Sharp.IconButton();
            this.buttonSearch = new FontAwesome.Sharp.IconButton();
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonChangeColumnVisibility
            // 
            this.buttonChangeColumnVisibility.AutoSize = true;
            this.buttonChangeColumnVisibility.Enabled = false;
            this.buttonChangeColumnVisibility.FlatAppearance.BorderSize = 0;
            this.buttonChangeColumnVisibility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonChangeColumnVisibility.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.buttonChangeColumnVisibility.IconChar = FontAwesome.Sharp.IconChar.Columns;
            this.buttonChangeColumnVisibility.IconColor = System.Drawing.Color.DarkSeaGreen;
            this.buttonChangeColumnVisibility.IconSize = 48;
            this.buttonChangeColumnVisibility.Location = new System.Drawing.Point(0, 378);
            this.buttonChangeColumnVisibility.Margin = new System.Windows.Forms.Padding(0);
            this.buttonChangeColumnVisibility.Name = "buttonChangeColumnVisibility";
            this.buttonChangeColumnVisibility.Rotation = 0D;
            this.buttonChangeColumnVisibility.Size = new System.Drawing.Size(54, 54);
            this.buttonChangeColumnVisibility.TabIndex = 7;
            this.buttonChangeColumnVisibility.TabStop = false;
            this.toolTipBase.SetToolTip(this.buttonChangeColumnVisibility, "Column Visibility Manager");
            this.buttonChangeColumnVisibility.UseVisualStyleBackColor = true;
            this.buttonChangeColumnVisibility.Click += new System.EventHandler(this.ButtonChangeColumnVisibility_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.AutoSize = true;
            this.buttonClear.Enabled = false;
            this.buttonClear.FlatAppearance.BorderSize = 0;
            this.buttonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClear.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.buttonClear.IconChar = FontAwesome.Sharp.IconChar.Eraser;
            this.buttonClear.IconColor = System.Drawing.Color.DarkSeaGreen;
            this.buttonClear.IconSize = 48;
            this.buttonClear.Location = new System.Drawing.Point(0, 324);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(0);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Rotation = 0D;
            this.buttonClear.Size = new System.Drawing.Size(54, 54);
            this.buttonClear.TabIndex = 6;
            this.buttonClear.TabStop = false;
            this.toolTipBase.SetToolTip(this.buttonClear, "Clear");
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.AutoSize = true;
            this.buttonDelete.Enabled = false;
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.buttonDelete.IconChar = FontAwesome.Sharp.IconChar.Trash;
            this.buttonDelete.IconColor = System.Drawing.Color.IndianRed;
            this.buttonDelete.IconSize = 48;
            this.buttonDelete.Location = new System.Drawing.Point(0, 270);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Rotation = 0D;
            this.buttonDelete.Size = new System.Drawing.Size(54, 54);
            this.buttonDelete.TabIndex = 5;
            this.buttonDelete.TabStop = false;
            this.toolTipBase.SetToolTip(this.buttonDelete, "Delete (F7)");
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // buttonDuplicate
            // 
            this.buttonDuplicate.AutoSize = true;
            this.buttonDuplicate.Enabled = false;
            this.buttonDuplicate.FlatAppearance.BorderSize = 0;
            this.buttonDuplicate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDuplicate.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.buttonDuplicate.IconChar = FontAwesome.Sharp.IconChar.Clone;
            this.buttonDuplicate.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(189)))), ((int)(((byte)(167)))));
            this.buttonDuplicate.IconSize = 48;
            this.buttonDuplicate.Location = new System.Drawing.Point(0, 216);
            this.buttonDuplicate.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDuplicate.Name = "buttonDuplicate";
            this.buttonDuplicate.Rotation = 0D;
            this.buttonDuplicate.Size = new System.Drawing.Size(54, 54);
            this.buttonDuplicate.TabIndex = 4;
            this.buttonDuplicate.TabStop = false;
            this.toolTipBase.SetToolTip(this.buttonDuplicate, "Duplicate");
            this.buttonDuplicate.UseVisualStyleBackColor = true;
            this.buttonDuplicate.Click += new System.EventHandler(this.ButtonDuplicate_Click);
            // 
            // buttonNew
            // 
            this.buttonNew.AutoSize = true;
            this.buttonNew.Enabled = false;
            this.buttonNew.FlatAppearance.BorderSize = 0;
            this.buttonNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNew.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.buttonNew.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.buttonNew.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(189)))), ((int)(((byte)(167)))));
            this.buttonNew.IconSize = 48;
            this.buttonNew.Location = new System.Drawing.Point(0, 162);
            this.buttonNew.Margin = new System.Windows.Forms.Padding(0);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Rotation = 0D;
            this.buttonNew.Size = new System.Drawing.Size(54, 54);
            this.buttonNew.TabIndex = 3;
            this.buttonNew.TabStop = false;
            this.toolTipBase.SetToolTip(this.buttonNew, "New (F5)");
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.ButtonNew_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.AutoSize = true;
            this.buttonSave.Enabled = false;
            this.buttonSave.FlatAppearance.BorderSize = 0;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.buttonSave.IconChar = FontAwesome.Sharp.IconChar.Save;
            this.buttonSave.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(72)))), ((int)(((byte)(152)))));
            this.buttonSave.IconSize = 48;
            this.buttonSave.Location = new System.Drawing.Point(0, 108);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Rotation = 0D;
            this.buttonSave.Size = new System.Drawing.Size(54, 54);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.TabStop = false;
            this.toolTipBase.SetToolTip(this.buttonSave, "Save (F12)");
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.AutoSize = true;
            this.buttonRefresh.Enabled = false;
            this.buttonRefresh.FlatAppearance.BorderSize = 0;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.buttonRefresh.IconChar = FontAwesome.Sharp.IconChar.Redo;
            this.buttonRefresh.IconColor = System.Drawing.Color.CornflowerBlue;
            this.buttonRefresh.IconSize = 48;
            this.buttonRefresh.Location = new System.Drawing.Point(0, 54);
            this.buttonRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Rotation = 0D;
            this.buttonRefresh.Size = new System.Drawing.Size(54, 54);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.TabStop = false;
            this.toolTipBase.SetToolTip(this.buttonRefresh, "Refresh");
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.AutoSize = true;
            this.buttonSearch.FlatAppearance.BorderSize = 0;
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.buttonSearch.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.buttonSearch.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(134)))), ((int)(((byte)(244)))));
            this.buttonSearch.IconSize = 48;
            this.buttonSearch.Location = new System.Drawing.Point(0, 0);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Rotation = 0D;
            this.buttonSearch.Size = new System.Drawing.Size(54, 54);
            this.buttonSearch.TabIndex = 0;
            this.buttonSearch.TabStop = false;
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
        private FontAwesome.Sharp.IconButton buttonSearch;
        private FontAwesome.Sharp.IconButton buttonRefresh;
        private FontAwesome.Sharp.IconButton buttonSave;
        private FontAwesome.Sharp.IconButton buttonClear;
        private FontAwesome.Sharp.IconButton buttonDuplicate;
        private FontAwesome.Sharp.IconButton buttonDelete;
        private FontAwesome.Sharp.IconButton buttonNew;
        private FontAwesome.Sharp.IconButton buttonChangeColumnVisibility;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
    }
}

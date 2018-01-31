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
            this.buttonChangeColumnVisibility = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonDuplicate = new System.Windows.Forms.Button();
            this.buttonNew = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelWindowName
            // 
            this.labelWindowName.Size = new System.Drawing.Size(752, 46);           
            // 
            // buttonChangeColumnVisibility
            // 
            this.buttonChangeColumnVisibility.AutoSize = true;
            this.buttonChangeColumnVisibility.Enabled = false;
            this.buttonChangeColumnVisibility.FlatAppearance.BorderSize = 0;
            this.buttonChangeColumnVisibility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonChangeColumnVisibility.Image = global::TMTControls.Properties.Resources.columnsV2;
            this.buttonChangeColumnVisibility.Location = new System.Drawing.Point(0, 378);
            this.buttonChangeColumnVisibility.Margin = new System.Windows.Forms.Padding(0);
            this.buttonChangeColumnVisibility.Name = "buttonChangeColumnVisibility";
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
            this.buttonClear.Image = global::TMTControls.Properties.Resources.clean48;
            this.buttonClear.Location = new System.Drawing.Point(0, 324);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(0);
            this.buttonClear.Name = "buttonClear";
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
            this.buttonDelete.Image = global::TMTControls.Properties.Resources.removeV2;
            this.buttonDelete.Location = new System.Drawing.Point(0, 270);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDelete.Name = "buttonDelete";
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
            this.buttonDuplicate.Image = global::TMTControls.Properties.Resources.duplicateV2;
            this.buttonDuplicate.Location = new System.Drawing.Point(0, 216);
            this.buttonDuplicate.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDuplicate.Name = "buttonDuplicate";
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
            this.buttonNew.Image = global::TMTControls.Properties.Resources.addV2;
            this.buttonNew.Location = new System.Drawing.Point(0, 162);
            this.buttonNew.Margin = new System.Windows.Forms.Padding(0);
            this.buttonNew.Name = "buttonNew";
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
            this.buttonSave.Image = global::TMTControls.Properties.Resources.saveV2;
            this.buttonSave.Location = new System.Drawing.Point(0, 108);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSave.Name = "buttonSave";
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
            this.buttonRefresh.Image = global::TMTControls.Properties.Resources.reloadV2;
            this.buttonRefresh.Location = new System.Drawing.Point(0, 54);
            this.buttonRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRefresh.Name = "buttonRefresh";
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
            this.buttonSearch.Image = global::TMTControls.Properties.Resources.searchV2;
            this.buttonSearch.Location = new System.Drawing.Point(0, 0);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSearch.Name = "buttonSearch";
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
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(0, 46);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(54, 536);
            this.flowLayoutPanelButtons.TabIndex = 2;
            // 
            // BaseWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.Controls.Add(this.flowLayoutPanelButtons);
            this.Name = "BaseWindow";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.BaseWindow_Load);           
            this.Controls.SetChildIndex(this.flowLayoutPanelButtons, 0);
            this.flowLayoutPanelButtons.ResumeLayout(false);
            this.flowLayoutPanelButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonDuplicate;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonChangeColumnVisibility;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
    }
}

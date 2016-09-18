namespace TMTControls.TMTPanels
{
    partial class TMTPanelV2
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
            this.tmtButtonSearch = new TMTControls.TMTButton();
            this.tmtButtonReload = new TMTControls.TMTButton();
            this.tmtButtonAdd = new TMTControls.TMTButton();
            this.tmtButtonDelete = new TMTControls.TMTButton();
            this.tmtButtonDuplicate = new TMTControls.TMTButton();
            this.tmtButtonSave = new TMTControls.TMTButton();
            this.tmtButtonColumnManager = new TMTControls.TMTButton();
            this.progressBarMain = new System.Windows.Forms.ProgressBar();
            this.backgroundWorkerMain = new System.ComponentModel.BackgroundWorker();
            this.panelControlContainer = new System.Windows.Forms.Panel();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.tmtButtonClear = new TMTControls.TMTButton();
            this.panelMain.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelControlContainer);
            this.panelMain.Controls.Add(this.progressBarMain);
            this.panelMain.Controls.Add(this.panelButtons);
            // 
            // buttonBack
            // 
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.buttonBack, "Go to Home Page");
            // 
            // tmtButtonSearch
            // 
            this.tmtButtonSearch.AutoSize = true;
            this.tmtButtonSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tmtButtonSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonSearch.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonSearch.FlatAppearance.BorderSize = 0;
            this.tmtButtonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtButtonSearch.Image = global::TMTControls.Properties.Resources.searchV2;
            this.tmtButtonSearch.Location = new System.Drawing.Point(0, 0);
            this.tmtButtonSearch.Margin = new System.Windows.Forms.Padding(0);
            this.tmtButtonSearch.Name = "tmtButtonSearch";
            this.tmtButtonSearch.Size = new System.Drawing.Size(54, 54);
            this.tmtButtonSearch.TabIndex = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonSearch, "Search");
            this.tmtButtonSearch.UseVisualStyleBackColor = false;
            this.tmtButtonSearch.Click += new System.EventHandler(this.tmtButtonSearch_Click);
            // 
            // tmtButtonReload
            // 
            this.tmtButtonReload.AutoSize = true;
            this.tmtButtonReload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tmtButtonReload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonReload.Enabled = false;
            this.tmtButtonReload.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonReload.FlatAppearance.BorderSize = 0;
            this.tmtButtonReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtButtonReload.Image = global::TMTControls.Properties.Resources.reloadV2;
            this.tmtButtonReload.Location = new System.Drawing.Point(0, 54);
            this.tmtButtonReload.Margin = new System.Windows.Forms.Padding(0);
            this.tmtButtonReload.Name = "tmtButtonReload";
            this.tmtButtonReload.Size = new System.Drawing.Size(54, 54);
            this.tmtButtonReload.TabIndex = 1;
            this.toolTipMain.SetToolTip(this.tmtButtonReload, "Refresh");
            this.tmtButtonReload.UseVisualStyleBackColor = false;
            this.tmtButtonReload.Click += new System.EventHandler(this.tmtButtonReload_Click);
            // 
            // tmtButtonAdd
            // 
            this.tmtButtonAdd.AutoSize = true;
            this.tmtButtonAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tmtButtonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonAdd.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonAdd.FlatAppearance.BorderSize = 0;
            this.tmtButtonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtButtonAdd.Image = global::TMTControls.Properties.Resources.addV2;
            this.tmtButtonAdd.Location = new System.Drawing.Point(0, 108);
            this.tmtButtonAdd.Margin = new System.Windows.Forms.Padding(0);
            this.tmtButtonAdd.Name = "tmtButtonAdd";
            this.tmtButtonAdd.Size = new System.Drawing.Size(54, 54);
            this.tmtButtonAdd.TabIndex = 2;
            this.toolTipMain.SetToolTip(this.tmtButtonAdd, "Add New");
            this.tmtButtonAdd.UseVisualStyleBackColor = false;
            this.tmtButtonAdd.Click += new System.EventHandler(this.tmtButtonAdd_Click);
            // 
            // tmtButtonDelete
            // 
            this.tmtButtonDelete.AutoSize = true;
            this.tmtButtonDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tmtButtonDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonDelete.Enabled = false;
            this.tmtButtonDelete.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonDelete.FlatAppearance.BorderSize = 0;
            this.tmtButtonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtButtonDelete.Image = global::TMTControls.Properties.Resources.removeV2;
            this.tmtButtonDelete.Location = new System.Drawing.Point(0, 162);
            this.tmtButtonDelete.Margin = new System.Windows.Forms.Padding(0);
            this.tmtButtonDelete.Name = "tmtButtonDelete";
            this.tmtButtonDelete.Size = new System.Drawing.Size(54, 54);
            this.tmtButtonDelete.TabIndex = 3;
            this.toolTipMain.SetToolTip(this.tmtButtonDelete, "Remove");
            this.tmtButtonDelete.UseVisualStyleBackColor = false;
            this.tmtButtonDelete.Click += new System.EventHandler(this.tmtButtonDelete_Click);
            // 
            // tmtButtonDuplicate
            // 
            this.tmtButtonDuplicate.AutoSize = true;
            this.tmtButtonDuplicate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tmtButtonDuplicate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonDuplicate.Enabled = false;
            this.tmtButtonDuplicate.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonDuplicate.FlatAppearance.BorderSize = 0;
            this.tmtButtonDuplicate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtButtonDuplicate.Image = global::TMTControls.Properties.Resources.duplicateV2;
            this.tmtButtonDuplicate.Location = new System.Drawing.Point(0, 216);
            this.tmtButtonDuplicate.Margin = new System.Windows.Forms.Padding(0);
            this.tmtButtonDuplicate.Name = "tmtButtonDuplicate";
            this.tmtButtonDuplicate.Size = new System.Drawing.Size(54, 54);
            this.tmtButtonDuplicate.TabIndex = 4;
            this.toolTipMain.SetToolTip(this.tmtButtonDuplicate, "Duplicate");
            this.tmtButtonDuplicate.UseVisualStyleBackColor = false;
            this.tmtButtonDuplicate.Click += new System.EventHandler(this.tmtButtonDuplicate_Click);
            // 
            // tmtButtonSave
            // 
            this.tmtButtonSave.AutoSize = true;
            this.tmtButtonSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tmtButtonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonSave.Enabled = false;
            this.tmtButtonSave.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonSave.FlatAppearance.BorderSize = 0;
            this.tmtButtonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtButtonSave.Image = global::TMTControls.Properties.Resources.saveV2;
            this.tmtButtonSave.Location = new System.Drawing.Point(0, 270);
            this.tmtButtonSave.Margin = new System.Windows.Forms.Padding(0);
            this.tmtButtonSave.Name = "tmtButtonSave";
            this.tmtButtonSave.Size = new System.Drawing.Size(54, 54);
            this.tmtButtonSave.TabIndex = 5;
            this.toolTipMain.SetToolTip(this.tmtButtonSave, "Save");
            this.tmtButtonSave.UseVisualStyleBackColor = false;
            this.tmtButtonSave.Click += new System.EventHandler(this.tmtButtonSave_Click);
            // 
            // tmtButtonColumnManager
            // 
            this.tmtButtonColumnManager.AutoSize = true;
            this.tmtButtonColumnManager.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tmtButtonColumnManager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonColumnManager.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonColumnManager.FlatAppearance.BorderSize = 0;
            this.tmtButtonColumnManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtButtonColumnManager.Image = global::TMTControls.Properties.Resources.columnsV2;
            this.tmtButtonColumnManager.Location = new System.Drawing.Point(0, 324);
            this.tmtButtonColumnManager.Margin = new System.Windows.Forms.Padding(0);
            this.tmtButtonColumnManager.Name = "tmtButtonColumnManager";
            this.tmtButtonColumnManager.Size = new System.Drawing.Size(54, 54);
            this.tmtButtonColumnManager.TabIndex = 6;
            this.toolTipMain.SetToolTip(this.tmtButtonColumnManager, "Select what columns to show");
            this.tmtButtonColumnManager.UseVisualStyleBackColor = false;
            this.tmtButtonColumnManager.Click += new System.EventHandler(this.tmtButtonColumnManager_Click);
            // 
            // progressBarMain
            // 
            this.progressBarMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarMain.Location = new System.Drawing.Point(54, 517);
            this.progressBarMain.Margin = new System.Windows.Forms.Padding(0);
            this.progressBarMain.Name = "progressBarMain";
            this.progressBarMain.Size = new System.Drawing.Size(746, 23);
            this.progressBarMain.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarMain.TabIndex = 3;
            this.progressBarMain.Visible = false;
            // 
            // backgroundWorkerMain
            // 
            this.backgroundWorkerMain.WorkerSupportsCancellation = true;
            // 
            // panelControlContainer
            // 
            this.panelControlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlContainer.Location = new System.Drawing.Point(54, 0);
            this.panelControlContainer.Margin = new System.Windows.Forms.Padding(0);
            this.panelControlContainer.Name = "panelControlContainer";
            this.panelControlContainer.Size = new System.Drawing.Size(746, 517);
            this.panelControlContainer.TabIndex = 4;
            // 
            // panelButtons
            // 
            this.panelButtons.AutoSize = true;
            this.panelButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelButtons.Controls.Add(this.tmtButtonClear);
            this.panelButtons.Controls.Add(this.tmtButtonColumnManager);
            this.panelButtons.Controls.Add(this.tmtButtonSave);
            this.panelButtons.Controls.Add(this.tmtButtonDuplicate);
            this.panelButtons.Controls.Add(this.tmtButtonDelete);
            this.panelButtons.Controls.Add(this.tmtButtonAdd);
            this.panelButtons.Controls.Add(this.tmtButtonReload);
            this.panelButtons.Controls.Add(this.tmtButtonSearch);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelButtons.Location = new System.Drawing.Point(0, 0);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(54, 540);
            this.panelButtons.TabIndex = 0;
            // 
            // tmtButtonClear
            // 
            this.tmtButtonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tmtButtonClear.AutoSize = true;
            this.tmtButtonClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tmtButtonClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonClear.Enabled = false;
            this.tmtButtonClear.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonClear.FlatAppearance.BorderSize = 0;
            this.tmtButtonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtButtonClear.Image = global::TMTControls.Properties.Resources.clean48;
            this.tmtButtonClear.Location = new System.Drawing.Point(0, 378);
            this.tmtButtonClear.Margin = new System.Windows.Forms.Padding(0);
            this.tmtButtonClear.Name = "tmtButtonClear";
            this.tmtButtonClear.Size = new System.Drawing.Size(54, 54);
            this.tmtButtonClear.TabIndex = 7;
            this.toolTipMain.SetToolTip(this.tmtButtonClear, "Clear");
            this.tmtButtonClear.UseVisualStyleBackColor = false;
            this.tmtButtonClear.Click += new System.EventHandler(this.tmtButtonClear_Click);
            // 
            // TMTPanelV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "TMTPanelV2";
            this.DataAdded += new System.EventHandler(this.TMTPanel_DataAdded);
            this.DataChanged += new System.EventHandler(this.TMTPanel_DataChanged);
            this.DataLoaded += new System.EventHandler(this.TMTPanel_DataLoaded);
            this.DataSaved += new System.EventHandler(this.TMTPanel_DataSaved);
            this.DataCleared += new System.EventHandler(this.TMTPanelV2_DataCleared);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.ComponentModel.BackgroundWorker backgroundWorkerMain;
        protected TMTButton tmtButtonSearch;
        protected TMTButton tmtButtonReload;
        protected TMTButton tmtButtonAdd;
        protected TMTButton tmtButtonDelete;
        protected TMTButton tmtButtonDuplicate;
        protected TMTButton tmtButtonSave;
        protected TMTButton tmtButtonColumnManager;
        public System.Windows.Forms.Panel panelControlContainer;
        public System.Windows.Forms.ProgressBar progressBarMain;
        public System.Windows.Forms.Panel panelButtons;
        protected TMTButton tmtButtonClear;
    }
}

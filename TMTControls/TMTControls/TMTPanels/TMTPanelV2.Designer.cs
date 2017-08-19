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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TMTPanelV2));
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
            this.panelTop.SuspendLayout();
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
            this.toolTipMain.SetToolTip(this.buttonBack, resources.GetString("buttonBack.ToolTip"));
            // 
            // tmtButtonSearch
            // 
            resources.ApplyResources(this.tmtButtonSearch, "tmtButtonSearch");
            this.tmtButtonSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonSearch.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonSearch.FlatAppearance.BorderSize = 0;
            this.tmtButtonSearch.Image = global::TMTControls.Properties.Resources.searchV2;
            this.tmtButtonSearch.Name = "tmtButtonSearch";
            this.toolTipMain.SetToolTip(this.tmtButtonSearch, resources.GetString("tmtButtonSearch.ToolTip"));
            this.tmtButtonSearch.UseVisualStyleBackColor = false;
            this.tmtButtonSearch.Click += new System.EventHandler(this.tmtButtonSearch_Click);
            // 
            // tmtButtonReload
            // 
            resources.ApplyResources(this.tmtButtonReload, "tmtButtonReload");
            this.tmtButtonReload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonReload.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonReload.FlatAppearance.BorderSize = 0;
            this.tmtButtonReload.Image = global::TMTControls.Properties.Resources.reloadV2;
            this.tmtButtonReload.Name = "tmtButtonReload";
            this.toolTipMain.SetToolTip(this.tmtButtonReload, resources.GetString("tmtButtonReload.ToolTip"));
            this.tmtButtonReload.UseVisualStyleBackColor = false;
            this.tmtButtonReload.Click += new System.EventHandler(this.tmtButtonReload_Click);
            // 
            // tmtButtonAdd
            // 
            resources.ApplyResources(this.tmtButtonAdd, "tmtButtonAdd");
            this.tmtButtonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonAdd.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonAdd.FlatAppearance.BorderSize = 0;
            this.tmtButtonAdd.Image = global::TMTControls.Properties.Resources.addV2;
            this.tmtButtonAdd.Name = "tmtButtonAdd";
            this.toolTipMain.SetToolTip(this.tmtButtonAdd, resources.GetString("tmtButtonAdd.ToolTip"));
            this.tmtButtonAdd.UseVisualStyleBackColor = false;
            this.tmtButtonAdd.Click += new System.EventHandler(this.tmtButtonAdd_Click);
            // 
            // tmtButtonDelete
            // 
            resources.ApplyResources(this.tmtButtonDelete, "tmtButtonDelete");
            this.tmtButtonDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonDelete.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonDelete.FlatAppearance.BorderSize = 0;
            this.tmtButtonDelete.Image = global::TMTControls.Properties.Resources.removeV2;
            this.tmtButtonDelete.Name = "tmtButtonDelete";
            this.toolTipMain.SetToolTip(this.tmtButtonDelete, resources.GetString("tmtButtonDelete.ToolTip"));
            this.tmtButtonDelete.UseVisualStyleBackColor = false;
            this.tmtButtonDelete.Click += new System.EventHandler(this.tmtButtonDelete_Click);
            // 
            // tmtButtonDuplicate
            // 
            resources.ApplyResources(this.tmtButtonDuplicate, "tmtButtonDuplicate");
            this.tmtButtonDuplicate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonDuplicate.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonDuplicate.FlatAppearance.BorderSize = 0;
            this.tmtButtonDuplicate.Image = global::TMTControls.Properties.Resources.duplicateV2;
            this.tmtButtonDuplicate.Name = "tmtButtonDuplicate";
            this.toolTipMain.SetToolTip(this.tmtButtonDuplicate, resources.GetString("tmtButtonDuplicate.ToolTip"));
            this.tmtButtonDuplicate.UseVisualStyleBackColor = false;
            this.tmtButtonDuplicate.Click += new System.EventHandler(this.tmtButtonDuplicate_Click);
            // 
            // tmtButtonSave
            // 
            resources.ApplyResources(this.tmtButtonSave, "tmtButtonSave");
            this.tmtButtonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonSave.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonSave.FlatAppearance.BorderSize = 0;
            this.tmtButtonSave.Image = global::TMTControls.Properties.Resources.saveV2;
            this.tmtButtonSave.Name = "tmtButtonSave";
            this.toolTipMain.SetToolTip(this.tmtButtonSave, resources.GetString("tmtButtonSave.ToolTip"));
            this.tmtButtonSave.UseVisualStyleBackColor = false;
            this.tmtButtonSave.Click += new System.EventHandler(this.tmtButtonSave_Click);
            // 
            // tmtButtonColumnManager
            // 
            resources.ApplyResources(this.tmtButtonColumnManager, "tmtButtonColumnManager");
            this.tmtButtonColumnManager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonColumnManager.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonColumnManager.FlatAppearance.BorderSize = 0;
            this.tmtButtonColumnManager.Image = global::TMTControls.Properties.Resources.columnsV2;
            this.tmtButtonColumnManager.Name = "tmtButtonColumnManager";
            this.toolTipMain.SetToolTip(this.tmtButtonColumnManager, resources.GetString("tmtButtonColumnManager.ToolTip"));
            this.tmtButtonColumnManager.UseVisualStyleBackColor = false;
            this.tmtButtonColumnManager.Click += new System.EventHandler(this.tmtButtonColumnManager_Click);
            // 
            // progressBarMain
            // 
            resources.ApplyResources(this.progressBarMain, "progressBarMain");
            this.progressBarMain.Name = "progressBarMain";
            this.progressBarMain.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // backgroundWorkerMain
            // 
            this.backgroundWorkerMain.WorkerSupportsCancellation = true;
            // 
            // panelControlContainer
            // 
            resources.ApplyResources(this.panelControlContainer, "panelControlContainer");
            this.panelControlContainer.Name = "panelControlContainer";
            // 
            // panelButtons
            // 
            resources.ApplyResources(this.panelButtons, "panelButtons");
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelButtons.Controls.Add(this.tmtButtonClear);
            this.panelButtons.Controls.Add(this.tmtButtonColumnManager);
            this.panelButtons.Controls.Add(this.tmtButtonSave);
            this.panelButtons.Controls.Add(this.tmtButtonDuplicate);
            this.panelButtons.Controls.Add(this.tmtButtonDelete);
            this.panelButtons.Controls.Add(this.tmtButtonAdd);
            this.panelButtons.Controls.Add(this.tmtButtonReload);
            this.panelButtons.Controls.Add(this.tmtButtonSearch);
            this.panelButtons.Name = "panelButtons";
            // 
            // tmtButtonClear
            // 
            resources.ApplyResources(this.tmtButtonClear, "tmtButtonClear");
            this.tmtButtonClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonClear.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonClear.FlatAppearance.BorderSize = 0;
            this.tmtButtonClear.Image = global::TMTControls.Properties.Resources.clean48;
            this.tmtButtonClear.Name = "tmtButtonClear";
            this.toolTipMain.SetToolTip(this.tmtButtonClear, resources.GetString("tmtButtonClear.ToolTip"));
            this.tmtButtonClear.UseVisualStyleBackColor = false;
            this.tmtButtonClear.Click += new System.EventHandler(this.tmtButtonClear_Click);
            // 
            // TMTPanelV2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "TMTPanelV2";
            this.DataAdded += new System.EventHandler(this.TMTPanel_DataAdded);
            this.DataChanged += new System.EventHandler(this.TMTPanel_DataChanged);
            this.DataLoaded += new System.EventHandler(this.TMTPanel_DataLoaded);
            this.DataSaved += new System.EventHandler(this.TMTPanel_DataSaved);
            this.DataCleared += new System.EventHandler(this.TMTPanelV2_DataCleared);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
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

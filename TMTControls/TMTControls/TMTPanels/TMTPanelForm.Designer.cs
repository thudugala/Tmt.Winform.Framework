namespace TMTControls.TMTPanels
{
    partial class TMTPanelForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TMTPanelForm));
            this.bindingSourceMain = new System.Windows.Forms.BindingSource(this.components);
            this.tmtNavigator = new TMTControls.TMTMultipleColumnComboBox();
            this.errorProviderMain = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelButtons.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderMain)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorkerMain
            // 
            this.backgroundWorkerMain.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerMain_DoWork);
            this.backgroundWorkerMain.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerMain_RunWorkerCompleted);
            // 
            // tmtButtonSearch
            // 
            this.tmtButtonSearch.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonSearch.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonSearch, resources.GetString("tmtButtonSearch.ToolTip"));
            // 
            // tmtButtonReload
            // 
            this.tmtButtonReload.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonReload.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonReload, resources.GetString("tmtButtonReload.ToolTip"));
            // 
            // tmtButtonAdd
            // 
            this.tmtButtonAdd.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonAdd.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonAdd, resources.GetString("tmtButtonAdd.ToolTip"));
            // 
            // tmtButtonDelete
            // 
            this.tmtButtonDelete.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonDelete.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonDelete, resources.GetString("tmtButtonDelete.ToolTip"));
            // 
            // tmtButtonDuplicate
            // 
            this.tmtButtonDuplicate.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonDuplicate.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonDuplicate, resources.GetString("tmtButtonDuplicate.ToolTip"));
            // 
            // tmtButtonSave
            // 
            this.tmtButtonSave.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonSave.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonSave, resources.GetString("tmtButtonSave.ToolTip"));
            // 
            // tmtButtonColumnManager
            // 
            this.tmtButtonColumnManager.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonColumnManager.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonColumnManager, resources.GetString("tmtButtonColumnManager.ToolTip"));
            // 
            // panelControlContainer
            // 
            resources.ApplyResources(this.panelControlContainer, "panelControlContainer");
            // 
            // tmtButtonClear
            // 
            this.tmtButtonClear.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonClear.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonClear, resources.GetString("tmtButtonClear.ToolTip"));
            // 
            // buttonBack
            // 
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.buttonBack, resources.GetString("buttonBack.ToolTip"));
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.tmtNavigator);
            this.panelTop.Controls.SetChildIndex(this.buttonBack, 0);
            this.panelTop.Controls.SetChildIndex(this.labelPanelName, 0);
            this.panelTop.Controls.SetChildIndex(this.tmtNavigator, 0);
            // 
            // tmtNavigator
            // 
            this.tmtNavigator.BackColor = global::TMTControls.Properties.Settings.Default.PanelLabelBackColor;
            this.tmtNavigator.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::TMTControls.Properties.Settings.Default, "PanelLabelBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tmtNavigator.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::TMTControls.Properties.Settings.Default, "PanelLabelForeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tmtNavigator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            resources.ApplyResources(this.tmtNavigator, "tmtNavigator");
            this.tmtNavigator.ForeColor = global::TMTControls.Properties.Settings.Default.PanelLabelForeColor;
            this.tmtNavigator.Name = "tmtNavigator";
            this.tmtNavigator.TabStop = false;
            this.tmtNavigator.SelectedIndexChanged += new System.EventHandler(this.TmtNavigator_SelectedIndexChanged);
            // 
            // errorProviderMain
            // 
            this.errorProviderMain.ContainerControl = this;
            // 
            // TMTPanelForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "TMTPanelForm";
            this.DataLoaded += new System.EventHandler(this.TMTPanelForm_DataLoaded);
            this.Load += new System.EventHandler(this.TMTPanelForm_Load);
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.BindingSource bindingSourceMain;
        public TMTMultipleColumnComboBox tmtNavigator;
        public System.Windows.Forms.ErrorProvider errorProviderMain;
    }
}

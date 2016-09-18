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
            this.bindingSourceMain = new System.Windows.Forms.BindingSource(this.components);
            this.tmtNavigator = new TMTControls.TMTMultipleColumnComboBox();
            this.panelButtons.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMain)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorkerMain
            // 
            this.backgroundWorkerMain.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMain_DoWork);
            this.backgroundWorkerMain.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerMain_RunWorkerCompleted);
            // 
            // tmtButtonSearch
            // 
            this.tmtButtonSearch.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonSearch.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonSearch, "Search");
            // 
            // tmtButtonReload
            // 
            this.tmtButtonReload.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonReload.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonReload, "Refresh");
            // 
            // tmtButtonAdd
            // 
            this.tmtButtonAdd.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonAdd.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonAdd, "Add New");
            // 
            // tmtButtonDelete
            // 
            this.tmtButtonDelete.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonDelete.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonDelete, "Remove");
            // 
            // tmtButtonDuplicate
            // 
            this.tmtButtonDuplicate.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonDuplicate.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonDuplicate, "Duplicate");
            // 
            // tmtButtonSave
            // 
            this.tmtButtonSave.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonSave.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonSave, "Save");
            // 
            // tmtButtonColumnManager
            // 
            this.tmtButtonColumnManager.Enabled = false;
            this.tmtButtonColumnManager.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonColumnManager.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonColumnManager, "Select what columns to show");
            // 
            // panelControlContainer
            // 
            this.panelControlContainer.Size = new System.Drawing.Size(746, 517);
            // 
            // tmtButtonClear
            // 
            this.tmtButtonClear.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonClear.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonClear, "Clear");
            // 
            // buttonBack
            // 
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.buttonBack, "Go to Home Page");
            // 
            // tmtNavigator
            // 
            this.tmtNavigator.BackColor = global::TMTControls.Properties.Settings.Default.PanelLabelBackColor;
            this.tmtNavigator.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::TMTControls.Properties.Settings.Default, "PanelLabelBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tmtNavigator.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::TMTControls.Properties.Settings.Default, "PanelLabelForeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tmtNavigator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.tmtNavigator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtNavigator.ForeColor = global::TMTControls.Properties.Settings.Default.PanelLabelForeColor;
            this.tmtNavigator.Location = new System.Drawing.Point(227, 17);
            this.tmtNavigator.Name = "tmtNavigator";
            this.tmtNavigator.Size = new System.Drawing.Size(195, 27);
            this.tmtNavigator.TabIndex = 0;
            this.tmtNavigator.TabStop = false;
            this.tmtNavigator.SelectedIndexChanged += new System.EventHandler(this.tmtNavigator_SelectedIndexChanged);
            // 
            // TMTPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tmtNavigator);
            this.Name = "TMTPanelForm";
            this.Controls.SetChildIndex(this.labelPanelName, 0);
            this.Controls.SetChildIndex(this.tmtNavigator, 0);
            this.Controls.SetChildIndex(this.panelMain, 0);
            this.Controls.SetChildIndex(this.buttonBack, 0);
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.BindingSource bindingSourceMain;
        public TMTMultipleColumnComboBox tmtNavigator;

    }
}

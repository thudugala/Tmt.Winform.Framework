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
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMain)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorkerMain
            // 
            this.backgroundWorkerMain.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMain_DoWork);
            this.backgroundWorkerMain.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerMain_RunWorkerCompleted);
            
            // 
            // panelControlContainer
            // 
            this.panelControlContainer.Size = new System.Drawing.Size(746, 517);            
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
            this.tmtNavigator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtNavigator.ForeColor = global::TMTControls.Properties.Settings.Default.PanelLabelForeColor;
            this.tmtNavigator.Location = new System.Drawing.Point(330, 13);
            this.tmtNavigator.Margin = new System.Windows.Forms.Padding(5);
            this.tmtNavigator.Name = "tmtNavigator";
            this.tmtNavigator.Size = new System.Drawing.Size(250, 28);
            this.tmtNavigator.TabIndex = 0;
            this.tmtNavigator.TabStop = false;
            this.tmtNavigator.SelectedIndexChanged += new System.EventHandler(this.tmtNavigator_SelectedIndexChanged);
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
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.BindingSource bindingSourceMain;
        public TMTMultipleColumnComboBox tmtNavigator;

    }
}

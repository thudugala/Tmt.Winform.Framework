namespace TMTControls.TMTPanels
{
    partial class TMTPanelTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TMTPanelTable));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tmtDataGridViewMain = new TMTControls.TMTDataGrid.TMTDataGridView();
            this.panelControlContainer.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tmtDataGridViewMain)).BeginInit();
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
            this.panelControlContainer.Controls.Add(this.tmtDataGridViewMain);
            resources.ApplyResources(this.panelControlContainer, "panelControlContainer");
            // 
            // tmtButtonClear
            // 
            this.tmtButtonClear.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonClear.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonClear, resources.GetString("tmtButtonClear.ToolTip"));
            // 
            // labelPanelName
            // 
            resources.ApplyResources(this.labelPanelName, "labelPanelName");
            // 
            // buttonBack
            // 
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.buttonBack, resources.GetString("buttonBack.ToolTip"));
            // 
            // tmtDataGridViewMain
            // 
            this.tmtDataGridViewMain.AllowUserToAddRows = false;
            this.tmtDataGridViewMain.AllowUserToDeleteRows = false;
            this.tmtDataGridViewMain.AllowUserToOrderColumns = true;
            this.tmtDataGridViewMain.AutoGenerateColumns = false;
            this.tmtDataGridViewMain.BackgroundColor = System.Drawing.Color.White;
            this.tmtDataGridViewMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tmtDataGridViewMain.DefaultCellStyle = dataGridViewCellStyle1;
            this.tmtDataGridViewMain.DefaultOrderByStatement = null;
            this.tmtDataGridViewMain.DefaultWhereStatement = null;
            resources.ApplyResources(this.tmtDataGridViewMain, "tmtDataGridViewMain");
            this.tmtDataGridViewMain.EnableHeadersVisualStyles = false;
            this.tmtDataGridViewMain.Name = "tmtDataGridViewMain";
            this.tmtDataGridViewMain.TableName = null;
            this.tmtDataGridViewMain.ViewName = null;
            this.tmtDataGridViewMain.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.tmtDataGridViewMain_CellValueChanged);
            this.tmtDataGridViewMain.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.tmtDataGridViewMain_UserDeletedRow);
            this.tmtDataGridViewMain.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.tmtDataGridViewMain_UserDeletingRow);
            // 
            // TMTPanelTable
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "TMTPanelTable";
            this.DataLoaded += new System.EventHandler(this.TMTPanelTable_DataLoaded);
            this.Load += new System.EventHandler(this.TMTPanelTable_Load);
            this.panelControlContainer.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tmtDataGridViewMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public TMTControls.TMTDataGrid.TMTDataGridView tmtDataGridViewMain;
    }
}

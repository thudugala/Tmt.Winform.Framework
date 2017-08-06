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
            // panelControlContainer
            // 
            this.panelControlContainer.Controls.Add(this.tmtDataGridViewMain);
            this.panelControlContainer.Size = new System.Drawing.Size(746, 517);           
            // 
            // labelPanelName
            // 
            this.labelPanelName.Text = "Table Panel Name";           
            // 
            // tmtDataGridViewMain
            // 
            this.tmtDataGridViewMain.AllowUserToAddRows = false;
            this.tmtDataGridViewMain.AllowUserToDeleteRows = false;
            this.tmtDataGridViewMain.AllowUserToOrderColumns = true;
            this.tmtDataGridViewMain.AutoGenerateColumns = false;
            this.tmtDataGridViewMain.DefaultOrderByStatment = null;
            this.tmtDataGridViewMain.DefaultWhereStatment = null;
            this.tmtDataGridViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tmtDataGridViewMain.Location = new System.Drawing.Point(0, 0);
            this.tmtDataGridViewMain.Name = "tmtDataGridViewMain";
            this.tmtDataGridViewMain.Size = new System.Drawing.Size(746, 517);
            this.tmtDataGridViewMain.TabIndex = 0;
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

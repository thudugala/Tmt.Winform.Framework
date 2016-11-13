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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tmtDataGridViewMain = new TMTControls.TMTDataGrid.TMTDataGridView();
            this.panelControlContainer.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelMain.SuspendLayout();
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
            this.tmtButtonColumnManager.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonColumnManager.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonColumnManager, "Select what columns to show");
            // 
            // panelControlContainer
            // 
            this.panelControlContainer.Controls.Add(this.tmtDataGridViewMain);
            this.panelControlContainer.Size = new System.Drawing.Size(746, 517);
            // 
            // tmtButtonClear
            // 
            this.tmtButtonClear.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonClear.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.tmtButtonClear, "Clear");
            // 
            // labelPanelName
            // 
            this.labelPanelName.Text = "Table Panel Name";
            // 
            // buttonBack
            // 
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.toolTipMain.SetToolTip(this.buttonBack, "Go to Home Page");
            // 
            // tmtDataGridViewMain
            // 
            this.tmtDataGridViewMain.AddDataAllowed = true;
            this.tmtDataGridViewMain.AllowUserToAddRows = false;
            this.tmtDataGridViewMain.AllowUserToDeleteRows = false;
            this.tmtDataGridViewMain.AllowUserToOrderColumns = true;
            this.tmtDataGridViewMain.AutoGenerateColumns = false;
            this.tmtDataGridViewMain.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.tmtDataGridViewMain.BackgroundColor = System.Drawing.Color.White;
            this.tmtDataGridViewMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tmtDataGridViewMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.tmtDataGridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tmtDataGridViewMain.DefaultCellStyle = dataGridViewCellStyle2;
            this.tmtDataGridViewMain.DeleteDataAllowed = true;
            this.tmtDataGridViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tmtDataGridViewMain.EnableHeadersVisualStyles = false;
            this.tmtDataGridViewMain.Location = new System.Drawing.Point(0, 0);
            this.tmtDataGridViewMain.Name = "tmtDataGridViewMain";
            this.tmtDataGridViewMain.Size = new System.Drawing.Size(746, 517);
            this.tmtDataGridViewMain.TabIndex = 0;
            this.tmtDataGridViewMain.TableName = null;
            this.tmtDataGridViewMain.ViewName = null;
            this.tmtDataGridViewMain.ViewRowCount = null;
            this.tmtDataGridViewMain.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.tmtDataGridViewMain_CellValueChanged);
            this.tmtDataGridViewMain.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.tmtDataGridViewMain_UserDeletedRow);
            this.tmtDataGridViewMain.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.tmtDataGridViewMain_UserDeletingRow);
            // 
            // TMTPanelTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "TMTPanelTable";
            this.Load += new System.EventHandler(this.TMTPanelTable_Load);
            this.panelControlContainer.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tmtDataGridViewMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public TMTControls.TMTDataGrid.TMTDataGridView tmtDataGridViewMain;
    }
}

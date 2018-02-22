namespace TMTControls.TMTDialogs
{
    partial class TMTListOfValueDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TMTListOfValueDialog));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonOK = new TMTControls.TMTButton();
            this.tmtDataGridViewMain = new TMTControls.TMTDataGrid.TMTDataGridView();
            this.buttonSearch = new TMTControls.TMTButton();
            this.buttonCancel = new TMTControls.TMTButton();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tmtDataGridViewMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.buttonCancel);
            this.panelMain.Controls.Add(this.buttonSearch);
            this.panelMain.Controls.Add(this.tmtDataGridViewMain);
            this.panelMain.Controls.Add(this.buttonOK);
            resources.ApplyResources(this.panelMain, "panelMain");
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // tmtDataGridViewMain
            // 
            this.tmtDataGridViewMain.AllowUserToOrderColumns = true;
            resources.ApplyResources(this.tmtDataGridViewMain, "tmtDataGridViewMain");
            this.tmtDataGridViewMain.AutoGenerateColumns = false;
            this.tmtDataGridViewMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.tmtDataGridViewMain.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.tmtDataGridViewMain.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tmtDataGridViewMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.tmtDataGridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tmtDataGridViewMain.DefaultCellStyle = dataGridViewCellStyle4;
            this.tmtDataGridViewMain.DefaultOrderByStatement = null;
            this.tmtDataGridViewMain.DefaultWhereStatement = null;
            this.tmtDataGridViewMain.EnableHeadersVisualStyles = true;
            this.tmtDataGridViewMain.MultiSelect = false;
            this.tmtDataGridViewMain.Name = "tmtDataGridViewMain";
            this.tmtDataGridViewMain.ReadOnly = true;
            this.tmtDataGridViewMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tmtDataGridViewMain.StandardTab = true;
            this.tmtDataGridViewMain.TableName = null;
            this.tmtDataGridViewMain.ViewName = null;
            this.tmtDataGridViewMain.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TmtDataGridViewMain_CellDoubleClick);
            this.tmtDataGridViewMain.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.TmtDataGridViewMain_DataBindingComplete);
            this.tmtDataGridViewMain.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.TmtDataGridViewMain_RowHeaderMouseDoubleClick);
            this.tmtDataGridViewMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TmtDataGridViewMain_KeyDown);
            // 
            // buttonSearch
            // 
            resources.ApplyResources(this.buttonSearch, "buttonSearch");
            this.buttonSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonSearch.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonSearch.FlatAppearance.BorderSize = 0;
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // TMTListOfValueDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.buttonCancel;
            resources.ApplyResources(this, "$this");
            this.Name = "TMTListOfValueDialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.TMTListOfValueDialog_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tmtDataGridViewMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TMTControls.TMTButton buttonOK;
        private TMTDataGrid.TMTDataGridView tmtDataGridViewMain;
        private TMTControls.TMTButton buttonSearch;
        private TMTControls.TMTButton buttonCancel;
    }
}
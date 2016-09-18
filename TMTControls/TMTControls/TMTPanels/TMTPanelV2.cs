using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls.TMTPanels
{
    public partial class TMTPanelV2 : TMTUserControl
    {
        public TMTPanelV2()
        {
            InitializeComponent();

            this.SearchDialog = new TMTSearchDialog();
            this.ColumnManagerDialog = new TMTColumnManagerDialog();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TMTColumnManagerDialog ColumnManagerDialog { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TMTSearchDialog SearchDialog { get; private set; }

        private void tmtButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.AddData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Properties.Resources.ERROR_AddingNew, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tmtButtonClear_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Properties.Resources.ERROR_ClearingData, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tmtButtonColumnManager_Click(object sender, EventArgs e)
        {
            try
            {
                this.ColumnManager();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Properties.Resources.ERROR_ColumnManager, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tmtButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.RemoveData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Properties.Resources.ERROR_AddingNew, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tmtButtonDuplicate_Click(object sender, EventArgs e)
        {
            try
            {
                this.DuplicateData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Properties.Resources.ERROR_DuplicatingNew, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tmtButtonReload_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Properties.Resources.ERROR_ReLoadingData, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tmtButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Properties.Resources.ERROR_SavingData, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tmtButtonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SearchDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    this.LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Properties.Resources.ERROR_LoadingData, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TMTPanel_DataAdded(object sender, EventArgs e)
        {
            tmtButtonSave.Enabled = true;
            tmtButtonClear.Enabled = true;
        }

        private void TMTPanel_DataChanged(object sender, EventArgs e)
        {
            tmtButtonSave.Enabled = true;
        }

        private void TMTPanel_DataLoaded(object sender, EventArgs e)
        {
            tmtButtonReload.Enabled = true;
            tmtButtonDuplicate.Enabled = true;
            tmtButtonDelete.Enabled = true;
            tmtButtonClear.Enabled = true;

            tmtButtonSave.Enabled = false;
        }

        private void TMTPanel_DataSaved(object sender, EventArgs e)
        {
            tmtButtonSave.Enabled = false;
        }

        private void TMTPanelV2_DataCleared(object sender, EventArgs e)
        {
            tmtButtonReload.Enabled = false;
            tmtButtonDuplicate.Enabled = false;
            tmtButtonDelete.Enabled = false;
            tmtButtonSave.Enabled = false;
            tmtButtonClear.Enabled = false;
        }
    }
}
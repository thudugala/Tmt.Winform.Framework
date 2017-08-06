using System;
using System.ComponentModel;

namespace TMTControls.TMTPanels
{
    public partial class TMTPanelV2 : TMTUserControl
    {
        public TMTPanelV2()
        {
            InitializeComponent();

            this.SuspendLayout();

            this.SearchDialog = new TMTSearchDialog();
            this.SearchDialog.SearchLovLoading += (sender, e) => this.SearchLovLoading?.Invoke(sender, e);
            this.ColumnManagerDialog = new TMTColumnManagerDialog();

            this.ResumeLayout(false);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TMTColumnManagerDialog ColumnManagerDialog { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TMTSearchDialog SearchDialog { get; private set; }

        [Category("TMT Data")]
        public event LovLoadingEventHandler SearchLovLoading;

        private void tmtButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.AddData();
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_AddingNew);
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
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_ClearingData);
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
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_ColumnManager);
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
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_Removing);
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
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_DuplicatingNew);
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
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_ReLoadingData);
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
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_SavingData);
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
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_LoadingData);
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
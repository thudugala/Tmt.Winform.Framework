using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace TMTControls
{
    [ToolboxBitmap(typeof(UserControl))]
    [DefaultEvent("DataValidateBeforeSave")]
    public partial class TMTUserControl : UserControl
    {
        public TMTUserControl()
        {
            InitializeComponent();
        }

        public delegate void DataValidateEventHandler(object sender, DataValidatingEventArgs e);

        public delegate void DataManagerEventHandler(object sender, PanelBackgroundWorkeArg e);

        [Category("TMT")]
        public event EventHandler BackButtonClicked;

        [Category("TMT Data")]
        public event EventHandler DataAdded;

        [Category("TMT Data")]
        public event EventHandler DataChanged;

        [Category("TMT Data")]
        public event EventHandler DataDuplicated;

        [Category("TMT Data")]
        public event EventHandler DataLoaded;

        [Category("TMT Data")]
        public event EventHandler DataSaved;

        [Category("TMT Data")]
        public event EventHandler DataCleared;

        [Category("TMT Data")]
        public event DataValidateEventHandler DataValidateBeforeSave;

        [Category("TMT Data")]
        public event DataManagerEventHandler DataManager;

        public virtual void AddData()
        {
            if (DataAdded != null)
            {
                DataAdded(this, EventArgs.Empty);
            }
        }

        public virtual void RemoveData()
        {
        }

        public virtual void ColumnManager()
        {
        }

        public virtual void ClearData()
        {
            if (DataCleared != null)
            {
                DataCleared(this, EventArgs.Empty);
            }
        }

        public virtual void DuplicateData()
        {
            if (DataDuplicated != null)
            {
                DataDuplicated(this, EventArgs.Empty);
            }
        }

        public virtual void LoadData()
        {
            if (DataLoaded != null)
            {
                DataLoaded(this, EventArgs.Empty);
            }
        }

        public virtual void OnDataChanged()
        {
            if (DataChanged != null)
            {
                DataChanged(this, EventArgs.Empty);
            }
        }

        public virtual void OnDataManager(PanelBackgroundWorkeArg data)
        {
            if (DataManager != null)
            {
                DataManager(this, data);
            }
        }

        public virtual void OnValidateBeforeSave(DataValidatingEventArgs dataToBeSaved)
        {
            try
            {
                if (DataValidateBeforeSave != null)
                {
                    DataValidateBeforeSave(this, dataToBeSaved);
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_SavingDataValidation);
            }
        }

        public virtual void SaveData()
        {
            if (DataSaved != null)
            {
                DataSaved(this, EventArgs.Empty);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (BackButtonClicked != null)
            {
                BackButtonClicked(sender, e);
            }
        }

        public class DataValidatingEventArgs : EventArgs
        {
            public bool CancelSave { get; set; }

            public DataSet DataToBeSaved { get; set; }
        }
    }
}
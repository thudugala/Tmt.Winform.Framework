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
            DataAdded?.Invoke(this, EventArgs.Empty);
        }

        public virtual void RemoveData()
        {
        }

        public virtual void ColumnManager()
        {
        }

        public virtual void ClearData()
        {
            DataCleared?.Invoke(this, EventArgs.Empty);
        }

        public virtual void DuplicateData()
        {
            DataDuplicated?.Invoke(this, EventArgs.Empty);
        }

        public virtual void LoadData()
        {
            DataLoaded?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnDataManager(PanelBackgroundWorkeArg data)
        {
            DataManager?.Invoke(this, data);
        }

        public virtual void OnValidateBeforeSave(DataValidatingEventArgs dataToBeSaved)
        {
            try
            {
                DataValidateBeforeSave?.Invoke(this, dataToBeSaved);
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_SavingDataValidation);
            }
        }

        public virtual void SaveData()
        {
            DataSaved?.Invoke(this, EventArgs.Empty);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            BackButtonClicked?.Invoke(this, e);
        }

        public class DataValidatingEventArgs : EventArgs
        {
            public bool CancelSave { get; set; }

            public DataSet DataToBeSaved { get; set; }
        }
    }
}
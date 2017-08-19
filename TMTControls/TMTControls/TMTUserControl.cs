using System;
using System.ComponentModel;
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
        public event EventHandler<DataValidatingEventArgs> DataValidateBeforeSave;

        [Category("TMT Data")]
        public event EventHandler<PanelBackgroundWorkEventArgs> DataManager;

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

        public virtual void OnDataManager(PanelBackgroundWorkEventArgs data)
        {
            DataManager?.Invoke(this, data);
        }

        public virtual void OnValidateBeforeSave(DataValidatingEventArgs dataToBeSaved)
        {
            DataValidateBeforeSave?.Invoke(this, dataToBeSaved);
        }

        public virtual void SaveData()
        {
            DataSaved?.Invoke(this, EventArgs.Empty);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            BackButtonClicked?.Invoke(this, e);
        }
    }
}
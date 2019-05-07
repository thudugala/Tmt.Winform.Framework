using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using TinyIoC;
using TMT.Controls.WinForms.Dialogs;

namespace TMT.Controls.WinForms.Panels
{
    [ToolboxItem(false)]
    public partial class BaseWindow : TMT.Controls.WinForms.BaseUserControl
    {
        private IDataManager _dataManager;

        public BaseWindow()
        {
            InitializeComponent();
        }

        [Category("Data"), DefaultValue(false)]
        public virtual bool IsDeleteAllowed { get; set; }

        [Category("Data"), DefaultValue(false)]
        public virtual bool IsNewAllowed { get; set; }

        internal WindowRecordState WindowRecordState { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected ColumnManagerDialog ColumnManagerDialog { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected SearchDialog SearchDialog { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(true)]
        protected bool ShowSaveNullMessage { get; set; }

        public bool PerformClear()
        {
            try
            {
                if(this.progressBarBase.Visible)
                {
                    return false;
                }

                this.UseWaitCursor = true;
                this.WindowRecordState = WindowRecordState.None;

                this.DataClear();

                buttonRefresh.Enabled = false;
                buttonDuplicate.Enabled = false;
                buttonDelete.Enabled = false;
                buttonSave.Enabled = false;
                buttonClear.Enabled = false;

                return true;
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_ClearingData);
                return false;
            }
            finally
            {
                this.UseWaitCursor = false;
                progressBarBase.Visible = false;
            }
        }

        public bool PerformDataNew()
        {
            try
            {
                if (this.progressBarBase.Visible)
                {
                    return false;
                }

                this.UseWaitCursor = true;
                progressBarBase.Visible = true;
                this.WindowRecordState = WindowRecordState.New;

                this.DataNew();

                buttonRefresh.Enabled = true;
                buttonClear.Enabled = true;

                buttonNew.Enabled = false;
                buttonDuplicate.Enabled = false;

                buttonDelete.Enabled = this.IsDeleteAllowed;
                buttonSave.Enabled = true;

                return true;
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_AddingNew);
                return false;
            }
            finally
            {
                this.UseWaitCursor = false;
                progressBarBase.Visible = false;
            }
        }

        public async Task<bool> PerformDataSave()
        {
            try
            {
                if (this.progressBarBase.Visible)
                {
                    return false;
                }

                this.UseWaitCursor = true;
                progressBarBase.Visible = true;
                this.WindowRecordState = WindowRecordState.None;

                await this.DataSave();

                buttonRefresh.Enabled = true;
                buttonClear.Enabled = true;

                buttonNew.Enabled = this.IsNewAllowed;
                buttonDuplicate.Enabled = this.IsNewAllowed;

                buttonDelete.Enabled = this.IsDeleteAllowed;
                buttonSave.Enabled = false;

                return true;
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_SavingData);
                return false;
            }
            finally
            {
                this.UseWaitCursor = false;
                progressBarBase.Visible = false;
            }
        }

        public bool PerformDelete()
        {
            try
            {
                if (this.progressBarBase.Visible)
                {
                    return false;
                }

                this.UseWaitCursor = true;
                progressBarBase.Visible = true;
                this.WindowRecordState = WindowRecordState.None;

                this.DataDelete();

                buttonRefresh.Enabled = true;
                buttonClear.Enabled = true;

                buttonNew.Enabled = this.IsNewAllowed;
                buttonDuplicate.Enabled = this.IsNewAllowed;

                buttonDelete.Enabled = this.IsDeleteAllowed;
                buttonSave.Enabled = true;

                return true;
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_Removing);
                return false;
            }
            finally
            {
                this.UseWaitCursor = false;
                progressBarBase.Visible = false;
            }
        }

        public bool PerformDuplicate()
        {
            try
            {
                if (this.progressBarBase.Visible)
                {
                    return false;
                }

                this.UseWaitCursor = true;
                progressBarBase.Visible = true;
                this.WindowRecordState = WindowRecordState.New;

                this.DataDuplicate();

                buttonRefresh.Enabled = true;
                buttonClear.Enabled = true;

                buttonNew.Enabled = false;
                buttonDuplicate.Enabled = false;

                buttonDelete.Enabled = this.IsDeleteAllowed;
                buttonSave.Enabled = true;

                return true;
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_DuplicatingNew);
                return false;
            }
            finally
            {
                this.UseWaitCursor = false;
                progressBarBase.Visible = false;
            }
        }

        public async Task<bool> PerformRefresh()
        {
            try
            {
                if (this.progressBarBase.Visible)
                {
                    return false;
                }

                this.UseWaitCursor = true;
                progressBarBase.Visible = true;
                this.WindowRecordState = WindowRecordState.None;

                await this.DataSearch();

                buttonRefresh.Enabled = true;
                buttonClear.Enabled = true;

                buttonNew.Enabled = this.IsNewAllowed;
                buttonDuplicate.Enabled = this.IsNewAllowed;

                buttonDelete.Enabled = this.IsDeleteAllowed;
                buttonSave.Enabled = false;

                return true;
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_ReLoadingData);
                return false;
            }
            finally
            {
                this.UseWaitCursor = false;
                progressBarBase.Visible = false;
            }
        }

        public async Task<bool> PerformSearch()
        {
            try
            {
                if (this.progressBarBase.Visible)
                {
                    return false;
                }

                this.UseWaitCursor = true;
                progressBarBase.Visible = true;
                this.WindowRecordState = WindowRecordState.None;

                if (this.SearchDialog.ShowDialog(this) == DialogResult.OK)
                {
                    await this.DataSearch();

                    buttonRefresh.Enabled = true;
                    buttonClear.Enabled = true;

                    buttonNew.Enabled = this.IsNewAllowed;
                    buttonDuplicate.Enabled = this.IsNewAllowed;

                    buttonDelete.Enabled = this.IsDeleteAllowed;
                    buttonSave.Enabled = false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_LoadingData);
                return false;
            }
            finally
            {
                this.UseWaitCursor = false;
                progressBarBase.Visible = false;
            }
        }

        internal async Task DataPopulateAllListOfValueRecords(ListOfValueLoadingEventArgs arg)
        {
            DataTable dataTable = null;
            if (arg.LimitLoad)
            {
                dataTable = await _dataManager?.LoadListOfValuesDataFromDatabase(arg,
                    this.GetListOfValueViewColumnDbNameList(arg.ListOfValueViewName), null);
            }
            else
            {
                var limitOffset = 0;
                var keepOnloading = true;
                while (keepOnloading)
                {
                    var table = await _dataManager?.LoadListOfValuesDataFromDatabase(arg,
                        this.GetListOfValueViewColumnDbNameList(arg.ListOfValueViewName), limitOffset);

                    limitOffset += 100;
                    if (table != null)
                    {
                        if (dataTable == null)
                        {
                            dataTable = table;
                        }
                        else
                        {
                            dataTable.Merge(table);
                        }

                        if (table.Rows.Count < 100)
                        {
                            keepOnloading = false;
                        }
                    }
                    else
                    {
                        keepOnloading = false;
                    }
                }
            }
            arg.ListOfValueDataTable = dataTable;
        }

        internal void FillSearchConditionTable(ListOfValueLoadingEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            if (e.IsValidate)
            {
                e.SearchConditionList.Add(new SearchEntity
                {
                    ColumnName = e.PrimaryColumnName,
                    Value = e.PrimaryColumnValue,
                    DataType = e.PrimaryColumnType,
                    IsFuntion = false
                });
            }

            if (e.FilterColumns == null || e.FilterColumns.Count <= 0)
            {
                return;
            }

            foreach (var filterColumn in e.FilterColumns)
            {
                if (e.Row.Cells[filterColumn.Value].Value != null)
                {
                    e.SearchConditionList.Add(new SearchEntity
                    {
                        ColumnName = filterColumn.Key,
                        Value = e.Row.Cells[filterColumn.Value].ValueString(),
                        DataType = e.Row.DataGridView.Columns[filterColumn.Value].ValueType,
                        IsFuntion = false
                    });
                }
            }
        }

        internal virtual Task LoadIfActive()
        {
            throw new NotImplementedException();
        }

        protected internal virtual void DataClear()
        {
            throw new NotImplementedException();
        }

        protected internal virtual void DataDelete()
        {
            throw new NotImplementedException();
        }

        protected internal virtual void DataDuplicate()
        {
            throw new NotImplementedException();
        }

        protected internal virtual Task<bool> DataSave()
        {
            throw new NotImplementedException();
        }

        protected internal virtual Task<bool> DataSearch()
        {
            throw new NotImplementedException();
        }

        protected internal void SetChangeColumnButtonEnabled(bool value)
        {
            buttonChangeColumnVisibility.Enabled = value;
        }

        protected internal void SetDeleteButtonEnabled(bool isDeleteAllowed)
        {
            buttonDelete.Enabled = isDeleteAllowed;
        }

        protected internal void SetNewButtonEnabled(bool isNewAllowed)
        {
            buttonNew.Enabled = isNewAllowed;
        }

        protected virtual void ChangeSelectedDataGridViewColumnVisibility()
        {
            throw new NotImplementedException();
        }

        protected void DataChanged()
        {
            buttonSave.Enabled = true;
        }

        protected virtual void DataNew()
        {
            throw new NotImplementedException();
        }

        protected async Task DataPopulateAllRecords(DataLoadEventArgs args)
        {
            if (args == null)
            {
                return;
            }

            foreach (var arg in args.DataLoadArgList)
            {
                DataTable dataTable = null;
                if (arg.LimitLoad)
                {
                    dataTable = await _dataManager?.LoadDataFromDatabase(arg, null);
                }
                else
                {
                    var limitOffset = 0;
                    var keepOnloading = true;
                    while (keepOnloading)
                    {
                        var table = await _dataManager?.LoadDataFromDatabase(arg, limitOffset);

                        limitOffset += 100;
                        if (table != null)
                        {
                            if (dataTable == null)
                            {
                                dataTable = table;
                            }
                            else
                            {
                                dataTable.Merge(table);
                            }

                            if (table.Rows.Count < 100)
                            {
                                keepOnloading = false;
                            }
                        }
                        else
                        {
                            keepOnloading = false;
                        }
                    }
                }
                if (dataTable != null)
                {
                    args.ChangedDataSet.Tables.Add(dataTable);
                }
            }
        }

        protected virtual bool DataValidateBeforeSave(DataSet dataToBeSaved)
        {
            throw new NotImplementedException();
        }

        protected virtual IList<string> GetListOfValueViewColumnDbNameList(string listOfValueViewName)
        {
            return new List<string>();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == Keys.F3)
                {
                    this.buttonSearch.PerformClick();
                }
                else if (keyData == Keys.F5)
                {
                    this.buttonNew.PerformClick();
                }
                else if (keyData == Keys.F12)
                {
                    this.buttonSave.PerformClick();
                }
                else if (keyData == Keys.F7)
                {
                    this.buttonDelete.PerformClick();
                }
            }
            catch { }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected Task<int[]> SaveDataToDatabase(DataSaveEventArgs saveArg)
        {
            return _dataManager?.SaveDataToDatabase(saveArg);
        }

        protected async virtual Task SearchDialogListOfValuesLoading(ListOfValueLoadingEventArgs e)
        {
            e.Handled = true;
            await this.DataPopulateAllListOfValueRecords(e);
        }

        private void BaseWindow_Load(object sender, EventArgs e)
        {
            try
            {
                this.SuspendLayout();

                TinyIoCContainer.Current.TryResolve(out this._dataManager);

                this.SearchDialog = new SearchDialog
                {
                    SearchListOfValueLoading = SearchDialogListOfValuesLoading
                };
                this.ColumnManagerDialog = new ColumnManagerDialog();
                this.ShowSaveNullMessage = true;

                this.ResumeLayout(false);
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
            }
        }

        private void ButtonChangeColumnVisibility_Click(object sender, EventArgs e)
        {
            try
            {
                this.ChangeSelectedDataGridViewColumnVisibility();
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_ColumnManager);
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            PerformClear();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            PerformDelete();
        }

        private void ButtonDuplicate_Click(object sender, EventArgs e)
        {
            PerformDuplicate();
        }

        private void ButtonNew_Click(object sender, EventArgs e)
        {
            PerformDataNew();
        }

        private async void ButtonRefresh_Click(object sender, EventArgs e)
        {
            await PerformRefresh();
        }

        private async void ButtonSave_Click(object sender, EventArgs e)
        {
            await PerformDataSave();
        }

        private async void ButtonSearch_Click(object sender, EventArgs e)
        {
            await PerformSearch();
        }
    }
}
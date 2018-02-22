using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using TinyIoC;
using TMTControls.TMTDialogs;

namespace TMTControls.TMTPanels
{
    [ToolboxItem(false)]
    public partial class BaseWindow : TMTControls.BaseUserControl
    {
        private IDataManager DataManager;

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
        protected TMTColumnManagerDialog ColumnManagerDialog { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected TMTSearchDialog SearchDialog { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(true)]
        protected bool ShowSaveNullMessage { get; set; }

        public void PerformDataNew()
        {
            try
            {
                this.UseWaitCursor = true;
                this.WindowRecordState = WindowRecordState.New;

                this.DataNew();

                buttonRefresh.Enabled = true;
                buttonClear.Enabled = true;

                buttonNew.Enabled = false;
                buttonDuplicate.Enabled = false;

                buttonDelete.Enabled = this.IsDeleteAllowed;
                buttonSave.Enabled = true;
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_AddingNew);
            }
            finally
            {
                this.UseWaitCursor = false;
            }
        }

        public async Task PerformDataSave()
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_SavingData);
            }
            finally
            {
                this.UseWaitCursor = false;
                progressBarBase.Visible = false;
            }
        }

        public async Task PerformRefresh()
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_ReLoadingData);
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
                dataTable = await DataManager?.LoadListOfValuesDataFromDatabase(arg,
                    this.GetListOfValueViewColumnDbNameList(arg.ListOfValueViewName), null);
            }
            else
            {
                int limitOffset = 0;
                bool keepOnloading = true;
                while (keepOnloading)
                {
                    var table = await DataManager?.LoadListOfValuesDataFromDatabase(arg,
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

            foreach (KeyValuePair<string, string> filterColumn in e.FilterColumns)
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

        internal virtual void LoadIfActive()
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

        protected async Task DataPopulateAllRecords(DataLoadArgs args)
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
                    dataTable = await DataManager?.LoadDataFromDatabase(arg, null);
                }
                else
                {
                    int limitOffset = 0;
                    bool keepOnloading = true;
                    while (keepOnloading)
                    {
                        var table = await DataManager?.LoadDataFromDatabase(arg, limitOffset);

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

        protected async Task SaveDataToDatabase(DataSaveArg saveArg)
        {
            await DataManager?.SaveDataToDatabase(saveArg);
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

                TinyIoCContainer.Current.TryResolve(out this.DataManager);

                this.SearchDialog = new TMTSearchDialog
                {
                    SearchListOfValueLoading = SearchDialogListOfValuesLoading
                };
                this.ColumnManagerDialog = new TMTColumnManagerDialog();
                this.ShowSaveNullMessage = true;

                buttonSearch.UpdateIcon();
                buttonRefresh.UpdateIcon();
                buttonSave.UpdateIcon();
                buttonClear.UpdateIcon();
                buttonChangeColumnVisibility.UpdateIcon();
                buttonNew.UpdateIcon();
                buttonDuplicate.UpdateIcon();
                buttonDelete.UpdateIcon();

                this.ResumeLayout(false);
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
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
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_ColumnManager);
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            try
            {
                this.UseWaitCursor = true;
                this.WindowRecordState = WindowRecordState.None;

                this.DataClear();

                buttonRefresh.Enabled = false;
                buttonDuplicate.Enabled = false;
                buttonDelete.Enabled = false;
                buttonSave.Enabled = false;
                buttonClear.Enabled = false;
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_ClearingData);
            }
            finally
            {
                this.UseWaitCursor = false;
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.UseWaitCursor = true;
                this.WindowRecordState = WindowRecordState.None;

                this.DataDelete();

                buttonRefresh.Enabled = true;
                buttonClear.Enabled = true;

                buttonNew.Enabled = this.IsNewAllowed;
                buttonDuplicate.Enabled = this.IsNewAllowed;

                buttonDelete.Enabled = this.IsDeleteAllowed;
                buttonSave.Enabled = true;
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_Removing);
            }
            finally
            {
                this.UseWaitCursor = false;
            }
        }

        private void ButtonDuplicate_Click(object sender, EventArgs e)
        {
            try
            {
                this.UseWaitCursor = true;
                this.WindowRecordState = WindowRecordState.New;

                this.DataDuplicate();

                buttonRefresh.Enabled = true;
                buttonClear.Enabled = true;

                buttonNew.Enabled = false;
                buttonDuplicate.Enabled = false;

                buttonDelete.Enabled = this.IsDeleteAllowed;
                buttonSave.Enabled = true;
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_DuplicatingNew);
            }
            finally
            {
                this.UseWaitCursor = false;
            }
        }

        private void ButtonNew_Click(object sender, EventArgs e)
        {
            this.PerformDataNew();
        }

        private async void ButtonRefresh_Click(object sender, EventArgs e)
        {
            await this.PerformRefresh();
        }

        private async void ButtonSave_Click(object sender, EventArgs e)
        {
            await this.PerformDataSave();
        }

        private async void ButtonSearch_Click(object sender, EventArgs e)
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_LoadingData);
            }
            finally
            {
                this.UseWaitCursor = false;
                progressBarBase.Visible = false;
            }
        }
    }
}
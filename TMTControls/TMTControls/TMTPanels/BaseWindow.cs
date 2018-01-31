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
        public BaseWindow()
        {
            InitializeComponent();
        }

        private IDataManager DataManager;

        private void BaseWindow_Load(object sender, EventArgs e)
        {
            try
            {
                this.SuspendLayout();

                TinyIoCContainer.Current.TryResolve(out this.DataManager);

                this.SearchDialog = new TMTSearchDialog();
                this.SearchDialog.SearchListOfValueLoading += SearchDialog_SearchListOfValueLoading;
                this.ColumnManagerDialog = new TMTColumnManagerDialog();
                this.ShowSaveNullMessage = true;

                this.ResumeLayout(false);
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
            }
        }

        internal WindowRecordState WindowRecordState { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(true)]
        protected bool ShowSaveNullMessage { get; set; }

        [Category("Data"), DefaultValue(false)]
        public virtual bool IsNewAllowed { get; set; }

        [Category("Data"), DefaultValue(false)]
        public virtual bool IsDeleteAllowed { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected TMTSearchDialog SearchDialog { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected TMTColumnManagerDialog ColumnManagerDialog { get; private set; }

        protected internal virtual Task<bool> DataSearch()
        {
            throw new NotImplementedException();
        }

        protected virtual bool DataValidateBeforeSave(DataSet dataToBeSaved)
        {
            throw new NotImplementedException();
        }

        protected internal virtual Task<bool> DataSave()
        {
            throw new NotImplementedException();
        }

        protected virtual void DataNew()
        {
            throw new NotImplementedException();
        }

        protected internal virtual void DataDuplicate()
        {
            throw new NotImplementedException();
        }

        protected internal virtual void DataDelete()
        {
            throw new NotImplementedException();
        }

        protected internal virtual void DataClear()
        {
            throw new NotImplementedException();
        }

        protected void DataPopulateAllRecords(DataLoadArgs args)
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
                    dataTable = DataManager?.LoadDataFromDatabase(arg, null);
                }
                else
                {
                    int limitOffset = 0;
                    bool keepOnloading = true;
                    while (keepOnloading)
                    {
                        var table = DataManager?.LoadDataFromDatabase(arg, limitOffset);

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

        internal void DataPopulateAllListOfValueRecords(ListOfValueLoadingEventArgs arg)
        {
            DataTable dataTable = null;
            if (arg.LimitLoad)
            {
                dataTable = DataManager?.LoadListOfValuesDataFromDatabase(arg,
                    this.GetListOfValueViewColumnDbNameList(arg.ListOfValueViewName), null);
            }
            else
            {
                int limitOffset = 0;
                bool keepOnloading = true;
                while (keepOnloading)
                {
                    var table = DataManager?.LoadListOfValuesDataFromDatabase(arg,
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

        protected virtual IList<string> GetListOfValueViewColumnDbNameList(string listOfValueViewName)
        {
            return new List<string>();
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

        protected void SaveDataToDatabase(DataSaveArg saveArg)
        {
            DataManager?.SaveDataToDatabase(saveArg);
        }

        protected virtual void ChangeSelectedDataGridViewColumnVisibility()
        {
            throw new NotImplementedException();
        }

        protected void DataChanged()
        {
            buttonSave.Enabled = true;
        }

        protected internal void SetNewButtonEnabled(bool isNewAllowed)
        {
            buttonNew.Enabled = isNewAllowed;
        }

        protected internal void SetDeleteButtonEnabled(bool isDeleteAllowed)
        {
            buttonDelete.Enabled = isDeleteAllowed;
        }

        protected internal void SetChangeColumnButtonEnabled(bool value)
        {
            buttonChangeColumnVisibility.Enabled = value;
        }

        protected virtual void SearchDialogListOfValuesLoading(ListOfValueLoadingEventArgs e)
        {
            throw new NotImplementedException();
        }

        internal virtual void LoadIfActive()
        {
            throw new NotImplementedException();
        }

        private void SearchDialog_SearchListOfValueLoading(object sender, ListOfValueLoadingEventArgs e)
        {
            try
            {
                this.SearchDialogListOfValuesLoading(e);
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_SearchDialogListOfValueLoading);
            }
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

        private async void ButtonRefresh_Click(object sender, EventArgs e)
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

        private async void ButtonSave_Click(object sender, EventArgs e)
        {
            await this.PerformDataSave();
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

        private void ButtonNew_Click(object sender, EventArgs e)
        {
            this.PerformDataNew();
        }

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
    }
}
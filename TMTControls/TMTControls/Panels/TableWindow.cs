using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMT.Controls.WinForms.Dialogs;

namespace TMT.Controls.WinForms.Panels
{
    public partial class TableWindow : TMT.Controls.WinForms.Panels.BaseWindow
    {
        public TableWindow()
        {
            InitializeComponent();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool IsDeleteAllowed
        {
            get
            {
                return this.dbViewTable.IsDeleteAllowed;
            }
            set
            {
                this.dbViewTable.IsDeleteAllowed = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool IsNewAllowed
        {
            get
            {
                return this.dbViewTable.IsNewAllowed;
            }
            set
            {
                this.dbViewTable.IsNewAllowed = value;
            }
        }

        public void InitializeTableControls()
        {
            this.SearchDialog.EntityList.Clear();
            this.SearchDialog.EntityListAddRange(this.dbViewTable.InitializeTableControls());
        }

        internal async override Task LoadIfActive()
        {
            try
            {
                if (this.dbViewTable.LoadDataWhenActive == false)
                {
                    return;
                }

                this.SearchDialog.DialogResult = DialogResult.Ignore;
                await this.PerformRefresh();
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
            }
        }

        protected internal override void DataClear()
        {
            this.dbViewTable.DataSourceTable.Clear();
        }

        protected internal override void DataDelete()
        {
            if (this.dbViewTable.DeleteSelectedRows())
            {
                this.DataChanged();
            }
        }

        protected internal override void DataDuplicate()
        {
            if (this.dbViewTable.DuplicateSelectedRows())
            {
                this.DataChanged();
            }
        }

        protected internal override async Task<bool> DataSave()
        {
            try
            {
                this.Validate();
                var changedData = this.dbViewTable.GetDataSourceTableChanges();

                if (changedData != null && changedData.Rows.Count > 0)
                {
                    var arg = new DataSaveEventArgs();
                    arg.ChangedDataSet.Tables.Add(changedData);
                    this.Validate();

                    var validateArg = new DataValidatingEventArgs
                    {
                        DataToBeSaved = arg.ChangedDataSet,
                        CancelSave = false
                    };
                    if (this.DataValidateBeforeSave(arg.ChangedDataSet) == false)
                    {
                        var saveResults = await Task.Run(async () =>
                        {
                            return await this.SaveDataToDatabase(arg);
                        });

                        if (saveResults.All(i => i > 0))
                        {
                            await this.LoadOnlySavedData(arg.ChangedDataSet.Tables[this.dbViewTable.TableName]);
                            //await this.DataSearch();
                        }
                        else
                        {
                            MessageDialog.Show(this, Properties.Resources.MSG_SAVE_SaveIssue,
                                Properties.Resources.MSG_HEADER_Save,
                                MessageDialogIcon.Warning);
                        }
                    }
                }
                else
                {
                    if (this.ShowSaveNullMessage)
                    {
                        MessageDialog.Show(this, Properties.Resources.MSG_SAVE_Null,
                                        Properties.Resources.MSG_HEADER_Save,
                                        MessageDialogIcon.Asterisk);
                    }
                }
                return true;
            }
            catch
            {
                throw;
            }
        }

        protected internal async override Task<bool> DataSearch()
        {
            var viewColumnsDictionary = this.dbViewTable.GetViewColumnDbNameDictionary();
            var arg = new DataLoadArg
            {
                IsCaseSensitive = this.SearchDialog.IsCaseSensitive,
                LimitLoad = this.SearchDialog.LimitLoad,
                ViewName = this.dbViewTable.ViewName,
                DefaultWhereStatement = this.dbViewTable.DefaultWhereStatement,
                DefaultOrderByStatement = this.dbViewTable.DefaultOrderByStatement,
                LoadSchema = this.dbViewTable.LoadSchema
            };
            arg.SearchConditionList.AddRange(this.SearchDialog.EntityList);
            arg.ColumnDbNameList.AddRange(viewColumnsDictionary.Keys.ToList());

            foreach (var searchCondition in arg.SearchConditionList)
            {
                if (viewColumnsDictionary.ContainsKey(searchCondition.ColumnName))
                {
                    searchCondition.IsFuntion = viewColumnsDictionary[searchCondition.ColumnName];
                }
            }

            var selectedRowIndexes = this.dbViewTable.SelectedRowIndexList;
            if (selectedRowIndexes.Count() > 0)
            {
                arg.SelectedRowIndexList.UnionWith(selectedRowIndexes);
            }

            var args = new DataLoadEventArgs();
            args.DataLoadArgList.Add(arg);

            await Task.Run(async () => await this.DataPopulateAllRecords(args));

            this.AfterLoad(args);

            return true;
        }

        protected override void ChangeSelectedDataGridViewColumnVisibility()
        {
            var colList = this.dbViewTable.Columns.Cast<DataGridViewColumn>()
                                                  .Where(c => (c is DataGridViewButtonColumn) == false)
                                                  .Select(c => new ColumnData(c.HeaderText, c.Visible)).ToList();

            this.ColumnManagerDialog.SetColumnList(colList);
            if (this.ColumnManagerDialog.ShowDialog(this) == DialogResult.OK)
            {
                var colDictionary = this.ColumnManagerDialog.GetColumnList().ToDictionary(c => c.Name);

                this.dbViewTable.Columns.Cast<DataGridViewColumn>()
                                                      .Where(c => colDictionary.ContainsKey(c.HeaderText))
                                                      .ToList()
                                                      .ForEach(c => c.Visible = colDictionary[c.HeaderText].Visibility);
            }
        }

        protected override void DataNew()
        {
            if (this.dbViewTable.AddNewRow())
            {
                this.DataChanged();
            }
        }

        protected override bool DataValidateBeforeSave(DataSet dataToBeSaved)
        {
            return this.dbViewTable.DataValidateBeforeSave(dataToBeSaved);
        }

        private static void SetSearchConditionTable(IList<SearchEntity> searchConditionList, DataTable dataTable, IReadOnlyCollection<DataGridViewColumn> keyViewColumnList)
        {
            var rowList = dataTable.Rows.Cast<DataRow>().Where(r => r.RowState != DataRowState.Deleted);

            foreach (var keyViewColumn in keyViewColumnList)
            {
                var keyDbColumnValueList = rowList.Select(r => r[keyViewColumn.DataPropertyName].ToString());
                if (keyDbColumnValueList.Count() <= 0)
                {
                    continue;
                }
                var idConcatList = string.Join("; ", keyDbColumnValueList);

                var searchCondition = searchConditionList.SingleOrDefault(r => r.ColumnName == keyViewColumn.DataPropertyName);
                if (searchCondition != null)
                {
                    searchCondition.Value += $"; {idConcatList}";
                }
                else
                {
                    searchConditionList.Add(new SearchEntity
                    {
                        ColumnName = keyViewColumn.DataPropertyName,
                        Value = idConcatList,
                        DataType = keyViewColumn.ValueType
                    });
                }
            }
        }

        private void AfterLoad(DataLoadEventArgs args)
        {
            var arg = args.DataLoadArgList.First();

            this.dbViewTable.DataSourceTable = args.ChangedDataSet.Tables[arg.ViewName];
            this.dbViewTable.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            this.dbViewTable.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);

            if (this.dbViewTable.DataSourceTable == null ||
                this.dbViewTable.DataSourceTable.Rows.Count == 0)
            {
                MessageDialog.Show(this, Properties.Resources.Exclamation_NoDataFoundText,
                                      Properties.Resources.Exclamation_NoDataFound,
                                      MessageDialogIcon.Asterisk);
                return;
            }

            if (arg.SelectedRowIndexList == null ||
                arg.SelectedRowIndexList.Count <= 0)
            {
                return;
            }
            bool firstIndex = true;
            foreach (DataGridViewRow row in this.dbViewTable.Rows)
            {
                if (arg.SelectedRowIndexList.Contains(row.Index) == false)
                {
                    continue;
                }
                row.Selected = true;
                if (firstIndex)
                {
                    this.dbViewTable.FirstDisplayedScrollingRowIndex = row.Index;
                    firstIndex = false;
                }
            }
            arg.SelectedRowIndexList.Clear();
        }

        private void DbTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                this.DataChanged();
            }
        }

        private void DbTable_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row != null)
            {
                this.DataChanged();
            }
        }

        private void DbTable_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row != null)
            {
                if (MessageDialog.ShowQuestion(this, Properties.Resources.QUESTION_DeleteConfirmAll,
                                          Properties.Resources.QUESTION_Delete) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private async Task LoadOnlySavedData(DataTable savedData)
        {
            await Task.Delay(100);

            var viewColumnsDictionary = this.dbViewTable.GetViewColumnDbNameDictionary();
            var arg = new DataLoadArg
            {
                IsCaseSensitive = this.SearchDialog.IsCaseSensitive,
                LimitLoad = this.SearchDialog.LimitLoad,
                ViewName = this.dbViewTable.ViewName,
                DefaultWhereStatement = this.dbViewTable.DefaultWhereStatement,
                DefaultOrderByStatement = this.dbViewTable.DefaultOrderByStatement,
                LoadSchema = this.dbViewTable.LoadSchema
            };
            arg.SearchConditionList.AddRange(this.SearchDialog.EntityList);
            arg.ColumnDbNameList.AddRange(viewColumnsDictionary.Keys.ToList());

            foreach (var searchCondition in arg.SearchConditionList)
            {
                if (viewColumnsDictionary.ContainsKey(searchCondition.ColumnName))
                {
                    searchCondition.IsFuntion = viewColumnsDictionary[searchCondition.ColumnName];
                }
            }

            var selectedRowIndexes = this.dbViewTable.SelectedRowIndexList;
            if (selectedRowIndexes.Count() > 0)
            {
                arg.SelectedRowIndexList.UnionWith(selectedRowIndexes);
            }
            if (this.SearchDialog.DialogResult != DialogResult.OK &&
                this.SearchDialog.DialogResult != DialogResult.Ignore)
            {
                var keyViewColumnList = this.dbViewTable.GetKeyViewColumnList();
                SetSearchConditionTable(arg.SearchConditionList, savedData, keyViewColumnList);
            }

            var args = new DataLoadEventArgs();
            args.DataLoadArgList.Add(arg);

            await Task.Run(async () => await this.DataPopulateAllRecords(args));

            this.AfterLoad(args);
        }

        private void TableWindow_Load(object sender, EventArgs e)
        {
            try
            {
                this.dbViewTable.DataSourceTable = new DataTable(this.dbViewTable.ViewName)
                {
                    Locale = CultureInfo.InvariantCulture
                };

                InitializeTableControls();

                this.SetNewButtonEnabled(this.IsNewAllowed);
                this.SetChangeColumnButtonEnabled(true);
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
            }
        }
    }
}
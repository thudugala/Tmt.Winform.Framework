using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMTControls.TMTDialogs;

namespace TMTControls.TMTPanels
{
    public partial class TableWindow : TMTControls.TMTPanels.BaseWindow
    {
        public TableWindow()
        {
            InitializeComponent();
        }

        private void TableWindow_Load(object sender, EventArgs e)
        {
            try
            {
                this.tmtDataGridViewTable.DataSourceTable = new DataTable(this.tmtDataGridViewTable.ViewName)
                {
                    Locale = CultureInfo.InvariantCulture
                };

                InitializeTableControls();

                this.SetNewButtonEnabled(this.IsNewAllowed);
                this.SetChangeColumnButtonEnabled(true);
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
            }
        }

        internal async override void LoadIfActive()
        {
            try
            {
                if (this.tmtDataGridViewTable.LoadDataWhenActive)
                {
                    this.SearchDialog.DialogResult = DialogResult.Ignore;
                    await this.DataSearch();
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool IsNewAllowed
        {
            get
            {
                return this.tmtDataGridViewTable.IsNewAllowed;
            }
            set
            {
                this.tmtDataGridViewTable.IsNewAllowed = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool IsDeleteAllowed
        {
            get
            {
                return this.tmtDataGridViewTable.IsDeleteAllowed;
            }
            set
            {
                this.tmtDataGridViewTable.IsDeleteAllowed = value;
            }
        }

        public void InitializeTableControls()
        {
            this.SearchDialog.EntityList.Clear();
            this.SearchDialog.EntityListAddRange(this.tmtDataGridViewTable.InitializeTableControls());
        }

        protected internal override void DataClear()
        {
            this.tmtDataGridViewTable.DataSourceTable.Clear();
        }

        protected override void DataNew()
        {
            if (this.tmtDataGridViewTable.AddNewRow())
            {
                this.DataChanged();
            }
        }

        protected internal override void DataDuplicate()
        {
            if (this.tmtDataGridViewTable.DuplicateSelectedRows())
            {
                this.DataChanged();
            }
        }

        protected internal override void DataDelete()
        {
            if (this.tmtDataGridViewTable.DeleteSelectedRows())
            {
                this.DataChanged();
            }
        }

        protected override void ChangeSelectedDataGridViewColumnVisibility()
        {
            var colList = this.tmtDataGridViewTable.Columns.Cast<DataGridViewColumn>()
                                                  .Where(c => (c is DataGridViewButtonColumn) == false)
                                                  .Select(c => new ColumnData(c.HeaderText, c.Visible)).ToList();

            this.ColumnManagerDialog.SetColumnList(colList);
            if (this.ColumnManagerDialog.ShowDialog(this) == DialogResult.OK)
            {
                var colDictionary = this.ColumnManagerDialog.GetColumnList().ToDictionary(c => c.Name);

                this.tmtDataGridViewTable.Columns.Cast<DataGridViewColumn>()
                                                      .Where(c => colDictionary.ContainsKey(c.HeaderText))
                                                      .ToList()
                                                      .ForEach(c => c.Visible = colDictionary[c.HeaderText].Visibility);
            }
        }

        protected internal async override Task<bool> DataSearch()
        {
            var viewColumnsDictionary = this.tmtDataGridViewTable.GetViewColumnDbNameDictionary();
            var arg = new DataLoadArg
            {
                IsCaseSensitive = this.SearchDialog.IsCaseSensitive,
                LimitLoad = this.SearchDialog.LimitLoad,
                ViewName = this.tmtDataGridViewTable.ViewName,
                DefaultWhereStatement = this.tmtDataGridViewTable.DefaultWhereStatement,
                DefaultOrderByStatement = this.tmtDataGridViewTable.DefaultOrderByStatement,
                LoadSchema = this.tmtDataGridViewTable.LoadSchema
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

            var args = new DataLoadArgs();
            args.DataLoadArgList.Add(arg);

            await Task.Run(() => this.DataPopulateAllRecords(args));

            this.AfterLoad(args);

            return true;
        }

        protected internal override async Task<bool> DataSave()
        {
            try
            {
                this.Validate();
                var changedData = this.tmtDataGridViewTable.GetDataSourceTableChanges();

                if (changedData != null && changedData.Rows.Count > 0)
                {
                    var arg = new DataSaveArg();
                    arg.ChangedDataSet.Tables.Add(changedData);
                    this.Validate();

                    var validateArg = new DataValidatingEventArgs
                    {
                        DataToBeSaved = arg.ChangedDataSet,
                        CancelSave = false
                    };
                    if (this.DataValidateBeforeSave(arg.ChangedDataSet) == false)
                    {
                        await Task.Run(() => this.SaveDataToDatabase(arg));

                        if (arg.SaveResults.All(i => i > 0))
                        {
                            this.LoadOnlySavedData(arg.ChangedDataSet.Tables[this.tmtDataGridViewTable.TableName]);
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

        protected override bool DataValidateBeforeSave(DataSet dataToBeSaved)
        {
            return this.tmtDataGridViewTable.DataValidateBeforeSave(dataToBeSaved);
        }

        private async void LoadOnlySavedData(DataTable savedData)
        {
            var viewColumnsDictionary = this.tmtDataGridViewTable.GetViewColumnDbNameDictionary();
            var arg = new DataLoadArg
            {
                IsCaseSensitive = this.SearchDialog.IsCaseSensitive,
                LimitLoad = this.SearchDialog.LimitLoad,
                ViewName = this.tmtDataGridViewTable.ViewName,
                DefaultWhereStatement = this.tmtDataGridViewTable.DefaultWhereStatement,
                DefaultOrderByStatement = this.tmtDataGridViewTable.DefaultOrderByStatement,
                LoadSchema = this.tmtDataGridViewTable.LoadSchema
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

            var selectedRowIndexes = this.tmtDataGridViewTable.SelectedRowIndexList;
            if (selectedRowIndexes.Count() > 0)
            {
                arg.SelectedRowIndexList.UnionWith(selectedRowIndexes);
            }
            if (this.SearchDialog.DialogResult != DialogResult.OK &&
                this.SearchDialog.DialogResult != DialogResult.Ignore)
            {
                var keyViewColumnList = this.tmtDataGridViewTable.GetKeyViewColumnList();
                SetSearchConditionTable(arg.SearchConditionList, savedData, keyViewColumnList);
            }

            var args = new DataLoadArgs();
            args.DataLoadArgList.Add(arg);

            await Task.Run(() => this.DataPopulateAllRecords(args));

            this.AfterLoad(args);
        }

        private void AfterLoad(DataLoadArgs args)
        {
            var arg = args.DataLoadArgList.First();

            this.tmtDataGridViewTable.DataSourceTable = args.ChangedDataSet.Tables[arg.ViewName];
            this.tmtDataGridViewTable.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            this.tmtDataGridViewTable.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);

            if (arg.SelectedRowIndexList != null &&
                arg.SelectedRowIndexList.Count > 0)
            {
                bool firstIndex = true;
                foreach (DataGridViewRow row in this.tmtDataGridViewTable.Rows)
                {
                    if (arg.SelectedRowIndexList.Contains(row.Index))
                    {
                        row.Selected = true;
                        if (firstIndex)
                        {
                            this.tmtDataGridViewTable.FirstDisplayedScrollingRowIndex = row.Index;
                            firstIndex = false;
                        }
                    }
                }
                arg.SelectedRowIndexList.Clear();
            }

            if (this.tmtDataGridViewTable.DataSourceTable == null ||
                this.tmtDataGridViewTable.DataSourceTable.Rows.Count == 0)
            {
                MessageDialog.Show(this, Properties.Resources.Exclamation_NoDataFoundText,
                                      Properties.Resources.Exclamation_NoDataFound,
                                      MessageDialogIcon.Asterisk);
            }
        }

        private static void SetSearchConditionTable(IList<SearchEntity> searchConditionList, DataTable dataTable, IReadOnlyCollection<DataGridViewColumn> keyViewColumnList)
        {
            var rowList = dataTable.Rows.Cast<DataRow>().Where(r => r.RowState != DataRowState.Deleted);

            foreach (var keyViewColumn in keyViewColumnList)
            {
                var keyDbColumnValueList = rowList.Select(r => r[keyViewColumn.DataPropertyName].ToString());
                if (keyDbColumnValueList.Count() > 0)
                {
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
        }

        private void TmtDataGridViewTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                this.DataChanged();
            }
        }

        private void TmtDataGridViewTable_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row != null)
            {
                this.DataChanged();
            }
        }

        private void TmtDataGridViewTable_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
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

        protected override void SearchDialogListOfValuesLoading(ListOfValueLoadingEventArgs e)
        {
            this.FillSearchConditionTable(e);
            this.DataPopulateAllListOfValueRecords(e);
        }
    }
}
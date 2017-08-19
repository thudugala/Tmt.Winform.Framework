using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using TMTControls.TMTDialogs;

namespace TMTControls.TMTPanels
{
    public partial class TMTPanelTable : TMTPanelV2
    {
        public TMTPanelTable()
        {
            InitializeComponent();

            tmtButtonColumnManager.Enabled = true;
        }

        private void TMTPanelTable_Load(object sender, EventArgs e)
        {
            this.InitializeTableControls();
        }

        public void LoadWindowWithDataWhenActive()
        {
            if (tmtDataGridViewMain.LoadDataWhenActive)
            {
                this.SearchDialog.DialogResult = DialogResult.Ignore;
                this.LoadData();
            }
        }

        public override void ClearData()
        {
            tmtDataGridViewMain.DataSourceTable.Clear();
            base.ClearData();
        }

        public override void ColumnManager()
        {
            List<ColumnData> colList = new List<ColumnData>();
            foreach (DataGridViewColumn viewCol in tmtDataGridViewMain.Columns)
            {
                if ((viewCol is DataGridViewButtonColumn) == false)
                {
                    colList.Add(new ColumnData(viewCol.HeaderText, viewCol.Visible));
                }
            }

            ColumnManagerDialog.SetColumnList(colList);
            if (ColumnManagerDialog.ShowDialog(this) == DialogResult.OK)
            {
                Dictionary<string, ColumnData> colDictionary = ColumnManagerDialog.GetColumnList().ToDictionary(c => c.Name);

                foreach (DataGridViewColumn viewCol in tmtDataGridViewMain.Columns)
                {
                    if (colDictionary.ContainsKey(viewCol.HeaderText))
                    {
                        viewCol.Visible = colDictionary[viewCol.HeaderText].Visibility;
                    }
                }
            }
            base.ColumnManager();
        }

        public override void DuplicateData()
        {
            if (tmtDataGridViewMain.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                DataRow newRow;
                foreach (DataGridViewRow selectedViewRow in tmtDataGridViewMain.SelectedRows)
                {
                    newRow = tmtDataGridViewMain.DataSourceTable.NewRow();
                    foreach (DataGridViewColumn col in tmtDataGridViewMain.Columns)
                    {
                        if (col.Visible && col.ReadOnly == false && string.IsNullOrWhiteSpace(col.DataPropertyName) == false)
                        {
                            newRow[col.DataPropertyName] = selectedViewRow.Cells[col.Name].Value;
                        }
                    }

                    if (newRow.ItemArray.Any(i => i != null && string.IsNullOrWhiteSpace(i.ToString()) == false))
                    {
                        tmtDataGridViewMain.DataSourceTable.Rows.Add(newRow);
                    }
                }
                tmtButtonSave.Enabled = true;
            }
        }

        public void InitializeTableControls()
        {
            this.SearchDialog.EntityList.Clear();
            this.SearchDialog.EntityListAddRange(tmtDataGridViewMain.InitializeTableControls());

            tmtButtonAdd.Enabled = tmtDataGridViewMain.AddDataAllowed;
            tmtButtonDuplicate.Enabled = tmtDataGridViewMain.AddDataAllowed;
            tmtButtonDelete.Enabled = tmtDataGridViewMain.DeleteDataAllowed;
        }

        public override void AddData()
        {
            if (tmtDataGridViewMain.AddDataAllowed)
            {
                tmtDataGridViewMain.AddNewRow();
                base.AddData();
            }
        }

        public override void LoadData()
        {
            if (backgroundWorkerMain.IsBusy == false)
            {
                progressBarMain.Visible = true;
                this.UseWaitCursor = true;

                PanelBackgroundWorkEventArgs arg = new PanelBackgroundWorkEventArgs()
                {
                    WorkType = PanelBackgroundWorkType.Load,
                    HeaderSearchConditionTable = this.SearchDialog.GetSearchCondition(),
                    IsCaseSensitive = this.SearchDialog.IsCaseSensitive,
                    LimitLoad = this.SearchDialog.LimitLoad,
                    HeaderViewName = tmtDataGridViewMain.ViewName,
                    DefaultWhereStatement = tmtDataGridViewMain.DefaultWhereStatement,
                    DefaultOrderByStatement = tmtDataGridViewMain.DefaultOrderByStatement,
                    LoadSchema = tmtDataGridViewMain.LoadSchema
                };
                arg.HeaderViewColumnDbNameListAddRange(tmtDataGridViewMain.GetViewColumnDbNameDictionary().Keys.ToList());
                string dbColumnName;
                foreach (DataRow searchRow in arg.HeaderSearchConditionTable.Rows)
                {
                    dbColumnName = searchRow["COLUMN"].ToString();
                    if (tmtDataGridViewMain.GetViewColumnDbNameDictionary().ContainsKey(dbColumnName))
                    {
                        searchRow["IS_FUNCTION"] = tmtDataGridViewMain.GetViewColumnDbNameDictionary()[dbColumnName];
                    }
                }

                backgroundWorkerMain.RunWorkerAsync(arg);
            }
        }

        public override void OnValidateBeforeSave(DataValidatingEventArgs dataToBeSaved)
        {
            if (dataToBeSaved == null)
            {
                throw new ArgumentNullException(nameof(dataToBeSaved));
            }

            try
            {
                base.OnValidateBeforeSave(dataToBeSaved);

                if (dataToBeSaved.DataToBeSaved.Tables.Contains(tmtDataGridViewMain.TableName))
                {
                    var mandatoryViewColumnList = tmtDataGridViewMain.GetMandatoryViewColumnList();

                    if (mandatoryViewColumnList != null &&
                        mandatoryViewColumnList.Count > 0)
                    {
                        DataRowCollection rowCollection = dataToBeSaved.DataToBeSaved.Tables[tmtDataGridViewMain.TableName].Rows;
                        foreach (DataRow row in rowCollection)
                        {
                            if (row.RowState != DataRowState.Deleted)
                            {
                                foreach (DataGridViewColumn mandatoryViewColumn in mandatoryViewColumnList)
                                {
                                    dataToBeSaved.CancelSave = this.ShowEmptyExclamation(row[mandatoryViewColumn.DataPropertyName], mandatoryViewColumn.HeaderText);
                                    if (dataToBeSaved.CancelSave)
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_SavingDataValidation);
            }
        }

        public override void RemoveData()
        {
            if (tmtDataGridViewMain.DeleteDataAllowed && tmtDataGridViewMain.SelectedRows != null && tmtDataGridViewMain.SelectedRows.Count > 0)
            {
                if (MessageBox.Show(this, Properties.Resources.QUESTION_DeleteConfirmAll,
                                          Properties.Resources.QUESTION_Delete,
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow deleteRow in tmtDataGridViewMain.SelectedRows)
                    {
                        tmtDataGridViewMain.Rows.RemoveAt(deleteRow.Index);
                    }
                    this.OnDataChanged();
                }
            }
            base.RemoveData();
        }

        public override void SaveData()
        {
            try
            {
                if (backgroundWorkerMain.IsBusy == false)
                {
                    this.Validate();
                    var changedData = tmtDataGridViewMain.GetDataSourceTableChanges();

                    if (changedData != null && changedData.Rows.Count > 0)
                    {
                        PanelBackgroundWorkEventArgs arg = new PanelBackgroundWorkEventArgs()
                        {
                            WorkType = PanelBackgroundWorkType.Save,
                            HeaderViewName = tmtDataGridViewMain.TableName
                        };
                        arg.ChangedDataSet.Tables.Add(changedData);

                        DataValidatingEventArgs validateArg = new DataValidatingEventArgs();
                        validateArg.DataToBeSaved = arg.ChangedDataSet;
                        validateArg.CancelSave = false;
                        this.OnValidateBeforeSave(validateArg);

                        if (validateArg.CancelSave == false)
                        {
                            backgroundWorkerMain.RunWorkerAsync(arg);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, Properties.Resources.MSG_SAVE_Null, Properties.Resources.MSG_HEADER_Save, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private void backgroundWorkerMain_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Do not access the form's BackgroundWorker reference directly.
                // Instead, use the reference provided by the sender parameter.
                BackgroundWorker worker = sender as BackgroundWorker;

                if (worker.CancellationPending == false)
                {
                    if (e.Argument is PanelBackgroundWorkEventArgs)
                    {
                        PanelBackgroundWorkEventArgs arg = e.Argument as PanelBackgroundWorkEventArgs;
                        e.Result = arg;

                        this.OnDataManager(arg);
                    }
                }

                // If the operation was canceled by the user,
                // set the DoWorkEventArgs.Cancel property to true.
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception)
            {
                //throw the exception so that RunWorkerCompleted can catch it.
                throw;
            }
        }

        private void backgroundWorkerMain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    TMTErrorDialog.Show(this, e.Error, Properties.Resources.ERROR_BW_Issue);
                }
                else
                {
                    if (e.Result != null && e.Result is PanelBackgroundWorkEventArgs)
                    {
                        PanelBackgroundWorkEventArgs arg = e.Result as PanelBackgroundWorkEventArgs;

                        if (arg.WorkType == PanelBackgroundWorkType.Load)
                        {
                            tmtDataGridViewMain.DataSourceTable = arg.ChangedDataSet.Tables[arg.HeaderViewName];
                            base.LoadData();
                            tmtDataGridViewMain.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                            tmtDataGridViewMain.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);

                            if (arg.SelectedRowIndexList.Count > 0)
                            {
                                bool firstIndex = true;
                                //tmtDataGridViewMain.SelectedRows.Clear();
                                foreach (DataGridViewRow row in tmtDataGridViewMain.Rows)
                                {
                                    if (arg.SelectedRowIndexList.Contains(row.Index))
                                    {
                                        row.Selected = true;
                                        if (firstIndex)
                                        {
                                            tmtDataGridViewMain.FirstDisplayedScrollingRowIndex = row.Index;
                                            firstIndex = false;
                                        }
                                    }
                                }
                                arg.SelectedRowIndexList.Clear();
                            }
                        }
                        else if (arg.WorkType == PanelBackgroundWorkType.Save)
                        {
                            if (arg.SaveResults.All(i => i > 0))
                            {
                                base.SaveData();
                                this.LoadOnlySavedData(arg.ChangedDataSet.Tables[arg.HeaderViewName]);
                            }
                            else
                            {
                                MessageBox.Show(this, Properties.Resources.MSG_SAVE_SaveIssue, Properties.Resources.MSG_HEADER_Save, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_BW_Issue);
            }
            finally
            {
                progressBarMain.Visible = false;
                this.UseWaitCursor = false;
            }
        }

        private void LoadOnlySavedData(DataTable savedData)
        {
            if (backgroundWorkerMain.IsBusy == false)
            {
                progressBarMain.Visible = true;
                this.UseWaitCursor = true;
                PanelBackgroundWorkEventArgs arg = new PanelBackgroundWorkEventArgs()
                {
                    WorkType = PanelBackgroundWorkType.Load,
                    HeaderSearchConditionTable = this.SearchDialog.GetSearchCondition(),
                    IsCaseSensitive = this.SearchDialog.IsCaseSensitive,
                    LimitLoad = this.SearchDialog.LimitLoad,
                    HeaderViewName = tmtDataGridViewMain.ViewName,
                    DefaultWhereStatement = tmtDataGridViewMain.DefaultWhereStatement
                };
                var selectedRowIndexes = this.tmtDataGridViewMain.SelectedRowIndexList;
                if (selectedRowIndexes.Count() > 0)
                {
                    arg.SelectedRowIndexList.UnionWith(selectedRowIndexes);
                }
                if (this.SearchDialog.DialogResult != DialogResult.OK &&
                    this.SearchDialog.DialogResult != DialogResult.Ignore)
                {
                    var keyViewColumnList = tmtDataGridViewMain.GetKeyViewColumnList();
                    SetSearchConditionTable(arg.HeaderSearchConditionTable, savedData, keyViewColumnList);
                }
                arg.HeaderViewColumnDbNameListAddRange(tmtDataGridViewMain.GetViewColumnDbNameDictionary().Keys.ToList());

                backgroundWorkerMain.RunWorkerAsync(arg);
            }
        }

        private static void SetSearchConditionTable(DataTable searchConditionTable, DataTable dataTable, IReadOnlyCollection<DataGridViewColumn> keyViewColumnList)
        {
            string idConcatList;
            foreach (DataGridViewColumn keyViewColumn in keyViewColumnList)
            {
                var keyDbColumnValueList = dataTable.Rows.Cast<DataRow>().Where(r => r.RowState != DataRowState.Deleted).Select(r => r[keyViewColumn.DataPropertyName].ToString());
                if (keyDbColumnValueList.Count() > 0)
                {
                    idConcatList = string.Empty;
                    foreach (string distinctId in keyDbColumnValueList)
                    {
                        if (string.IsNullOrWhiteSpace(idConcatList) == false)
                        {
                            idConcatList += ";";
                        }
                        idConcatList += distinctId;
                    }

                    var searchCondition = searchConditionTable.Rows.Cast<DataRow>().SingleOrDefault(r => r["COLUMN"].ToString() == keyViewColumn.DataPropertyName);
                    if (searchCondition != null)
                    {
                        searchCondition["VALUE"] += "; " + idConcatList;
                    }
                    else
                    {
                        searchConditionTable.Rows.Add(keyViewColumn.DataPropertyName, idConcatList, keyViewColumn.ValueType.FullName, "FALSE");
                    }
                }
            }
        }

        protected virtual void GetListOfValueDataTable(ListOfValueLoadingEventArgs e, params KeyValuePair<string, string>[] filterColumns)
        {
            this.tmtDataGridViewMain.FillSearchConditionTable(e, filterColumns);
        }

        protected virtual IList<string> GetListOfValueViewColumnDbNameList(string listOfValueViewName)
        {
            return null;
        }

        private bool ShowEmptyExclamation(object oValue, string columnHeaderText)
        {
            bool cancelSave = false;
            if (oValue == null || string.IsNullOrWhiteSpace(oValue.ToString()))
            {
                MessageBox.Show(this,
                    string.Format(CultureInfo.InvariantCulture, Properties.Resources.Exclamation_MandatoryValueEmpty, columnHeaderText),
                    Properties.Resources.Exclamation_MandatoryEmpty,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                cancelSave = true;
            }
            return cancelSave;
        }

        private void tmtDataGridViewMain_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                this.OnDataChanged();
            }
        }

        private void tmtDataGridViewMain_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row != null)
            {
                this.OnDataChanged();
            }
        }

        private void tmtDataGridViewMain_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row != null)
            {
                if (MessageBox.Show(this, Properties.Resources.QUESTION_DeleteConfirmAll,
                                          Properties.Resources.QUESTION_Delete,
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void TMTPanelTable_DataLoaded(object sender, EventArgs e)
        {
            if (tmtDataGridViewMain.DataSourceTable == null || tmtDataGridViewMain.DataSourceTable.Rows.Count == 0)
            {
                if (tmtDataGridViewMain.LoadDataWhenActive == false)
                {
                    MessageBox.Show(this, Properties.Resources.Exclamation_NoDataFoundText,
                                          Properties.Resources.Exclamation_NoDataFound,
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Asterisk);
                }
            }
        }
    }
}
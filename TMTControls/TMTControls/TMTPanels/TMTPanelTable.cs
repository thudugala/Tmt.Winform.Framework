using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TMTControls.TMTPanels
{
    public partial class TMTPanelTable : TMTPanelV2
    {
        public TMTPanelTable()
        {
            InitializeComponent();

            tmtButtonColumnManager.Enabled = true;
        }

        public override void ClearData()
        {
            tmtDataGridViewMain.DataSourceTable.Clear();
            base.ClearData();
        }

        public override void ColumnManager()
        {
            List<TMTColumnManagerDialog.ColumnData> colList = new List<TMTColumnManagerDialog.ColumnData>();
            foreach (DataGridViewColumn viewCol in tmtDataGridViewMain.Columns)
            {
                if ((viewCol is DataGridViewButtonColumn) == false)
                {
                    colList.Add(new TMTColumnManagerDialog.ColumnData(viewCol.HeaderText, viewCol.Visible));
                }
            }

            ColumnManagerDialog.SetColumnList(colList);
            if (ColumnManagerDialog.ShowDialog(this) == DialogResult.OK)
            {
                Dictionary<string, TMTColumnManagerDialog.ColumnData> colDictionary = ColumnManagerDialog.GetColumnList().ToDictionary(c => c.Name);

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
            this.SearchDialog.EntityList.AddRange(tmtDataGridViewMain.InitializeTableControls());
        }

        public override void AddData()
        {
            if (tmtDataGridViewMain.AddDataAllowed)
            {
                DataRow newRow = tmtDataGridViewMain.DataSourceTable.NewRow();
                tmtDataGridViewMain.DataSourceTable.Rows.Add(newRow);
                base.AddData();
            }
        }

        public override void LoadData()
        {
            if (backgroundWorkerMain.IsBusy == false)
            {
                progressBarMain.Visible = true;
                this.UseWaitCursor = true;

                PanelBackgroundWorkeArg arg = new PanelBackgroundWorkeArg();
                arg.Type = PanelBackgroundWorkeType.Load;
                arg.HeaderSearchConditionTable = this.SearchDialog.GetSearchCondition();
                arg.IsCaseSensitive = this.SearchDialog.IsCaseSensitive;
                arg.HeaderViewName = tmtDataGridViewMain.ViewName;
                arg.HeaderViewColumnDbNameList = tmtDataGridViewMain.GetViewColumnDbNameDictionary().Keys.ToList();
                arg.ViewRowCount = tmtDataGridViewMain.ViewRowCount;

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

        public override void OnValidateBeforeSave(TMTUserControl.DataValidatingEventArgs dataToBeSaved)
        {
            base.OnValidateBeforeSave(dataToBeSaved);

            if (dataToBeSaved.DataToBeSaved.Tables.Contains(tmtDataGridViewMain.TableName))
            {
                List<DataGridViewColumn> mandatoryViewColumnList = tmtDataGridViewMain.GetMandatoryViewColumnList();

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
            if (backgroundWorkerMain.IsBusy == false)
            {
                this.Validate();
                DataTable changedData = tmtDataGridViewMain.GetDataSourceTableChanges();

                if (changedData != null && changedData.Rows.Count > 0)
                {
                    PanelBackgroundWorkeArg arg = new PanelBackgroundWorkeArg();
                    arg.Type = PanelBackgroundWorkeType.Save;
                    arg.HeaderViewName = tmtDataGridViewMain.TableName;
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

        private void backgroundWorkerMain_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                // Do not access the form's BackgroundWorker reference directly.
                // Instead, use the reference provided by the sender parameter.
                BackgroundWorker worker = sender as BackgroundWorker;

                if (worker.CancellationPending == false)
                {
                    PanelBackgroundWorkeArg arg = e.Argument as PanelBackgroundWorkeArg;

                    this.OnDataManager(arg);

                    e.Result = arg;
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

        private void backgroundWorkerMain_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    MessageBox.Show(this, e.Error.Message, Properties.Resources.ERROR_BW_Issue, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (e.Result != null && e.Result is PanelBackgroundWorkeArg)
                    {
                        PanelBackgroundWorkeArg arg = e.Result as PanelBackgroundWorkeArg;

                        if (arg.Type == PanelBackgroundWorkeType.Load)
                        {
                            tmtDataGridViewMain.DataSourceTable = arg.ChangedDataSet.Tables[arg.HeaderViewName];
                            base.LoadData();
                            tmtDataGridViewMain.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                        }
                        else if (arg.Type == PanelBackgroundWorkeType.Save)
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
                    else
                    {
                        MessageBox.Show(this, Properties.Resources.MSG_SAVE_Null, Properties.Resources.MSG_HEADER_Save, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Properties.Resources.ERROR_BW_Issue, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                PanelBackgroundWorkeArg arg = new PanelBackgroundWorkeArg();
                arg.Type = PanelBackgroundWorkeType.Load;
                arg.HeaderSearchConditionTable = this.SearchDialog.GetSearchCondition();
                arg.IsCaseSensitive = this.SearchDialog.IsCaseSensitive;
                if (this.SearchDialog.DialogResult != DialogResult.OK)
                {
                    List<DataGridViewColumn> keyViewColumnList = tmtDataGridViewMain.GetKeyViewColumnList();
                    this.SetSearchConditionTable(arg.HeaderSearchConditionTable, savedData, keyViewColumnList);
                }

                arg.HeaderViewName = tmtDataGridViewMain.ViewName;

                backgroundWorkerMain.RunWorkerAsync(arg);
            }
        }

        private void SetSearchConditionTable(DataTable searchConditionTable, DataTable dataTable, List<DataGridViewColumn> keyViewColumnList)
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

                    DataRow searchCondition = searchConditionTable.Rows.Cast<DataRow>().SingleOrDefault(r => r["COLUMN"].ToString() == keyViewColumn.DataPropertyName);
                    if (searchCondition != null)
                    {
                        searchCondition["VALUE"] += "; " + idConcatList;
                    }
                    else
                    {
                        searchConditionTable.Rows.Add(keyViewColumn.DataPropertyName, idConcatList, keyViewColumn.ValueType.FullName);
                    }
                }
            }
        }

        private bool ShowEmptyExclamation(object oValue, string columnHeaderText)
        {
            bool cancelSave = false;
            if (oValue == null || string.IsNullOrWhiteSpace(oValue.ToString()))
            {
                MessageBox.Show(this,
                    string.Format(Properties.Resources.Exclamation_MandatoryValueEmpty, columnHeaderText),
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
    }
}
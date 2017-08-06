using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TMTControls.TMTDataGrid;

namespace TMTControls.TMTPanels
{
    [DefaultEvent("LovLoading")]
    public partial class TMTPanelForm : TMTPanelV2
    {
        private TMTDataGridView _focusedChildDataGridView;
        private bool _ignoreNavigatorSelectedIndexChange = false;
        private bool _isDataGridViewDataClear = false;
        private bool _isHeaderDataSourceTableLoading = false;
        private bool _isChildDataSourceTablesLoading = false;

        public TMTPanelForm()
        {
            InitializeComponent();

            tmtNavigator.DataSource = this.DataSourceTable;
            this.AddDataAllowed = true;
            this.DeleteDataAllowed = true;
        }

        private void TMTPanelForm_Load(object sender, EventArgs e)
        {
            this.DataSourceTable = new DataTable(this.ViewName);

            this.InitializeTMTControls();

            foreach (TMTDataGridView childDataGridView in this.panelControlContainer.GetChildDataGridViewList())
            {
                childDataGridView.InitializeTableControls();
            }

            tmtButtonAdd.Enabled = this.AddDataAllowed;
            tmtButtonDuplicate.Enabled = this.AddDataAllowed;
            tmtButtonDelete.Enabled = this.DeleteDataAllowed;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataTable DataSourceTable { get; set; }

        [Category("TMT Data")]
        public string TableName { get; set; }

        [Category("TMT Data")]
        public string ViewName { get; set; }

        [Category("TMT Data")]
        public string DefaultWhereStatment { get; set; }

        [Category("TMT Data")]
        public string DefaultOrderByStatment { get; set; }

        [Category("TMT Data"), DefaultValue(true)]
        public bool AddDataAllowed { get; set; }

        [Category("TMT Data"), DefaultValue(true)]
        public bool DeleteDataAllowed { get; set; }

        [Category("TMT Data"), DefaultValue(false)]
        public bool LoadDataWhenActive { get; set; }

        public override void AddData()
        {
            if (this._focusedChildDataGridView != null)
            {
                this._focusedChildDataGridView.AddNewRow();
            }
            else
            {
                foreach (DataColumn aColumn in this.DataSourceTable.Columns)
                {
                    if (aColumn.AllowDBNull == false)
                    {
                        aColumn.AllowDBNull = true;
                    }
                }
                DataRow newRow = this.DataSourceTable.NewRow();
                this.DataSourceTable.Rows.Add(newRow);
                if (tmtNavigator.DataSourceTable != null)
                {
                    tmtNavigator.SelectedIndex = this.DataSourceTable.Rows.IndexOf(newRow);
                }

                foreach (Control tmtControl in this.panelControlContainer.Controls)
                {
                    if (tmtControl is TextBox)
                    {
                        tmtControl.Text = string.Empty;
                    }
                    else if (tmtControl is CheckBox)
                    {
                        (tmtControl as CheckBox).Checked = false;
                    }
                    else if (tmtControl is ComboBox)
                    {
                        (tmtControl as ComboBox).SelectedIndex = -1;
                    }
                    else if (tmtControl is NumericUpDown)
                    {
                        (tmtControl as NumericUpDown).Value = 0;
                    }
                    else if (tmtControl is DateTimePicker)
                    {
                        (tmtControl as DateTimePicker).Value = DateTime.Now;
                    }
                    else if (tmtControl is TMTTextButtonBox)
                    {
                        tmtControl.Text = string.Empty;
                    }
                }

                this.ClearChildDataGridViews(true);
            }

            base.AddData();
        }

        public override void ClearData()
        {
            this.DataSourceTable.Clear();
            tmtNavigator.SelectedIndex = -1;

            foreach (Control tmtControl in this.panelControlContainer.Controls)
            {
                if (tmtControl is TextBox)
                {
                    tmtControl.Text = string.Empty;
                }
                else if (tmtControl is CheckBox)
                {
                    (tmtControl as CheckBox).Checked = false;
                }
                else if (tmtControl is ComboBox)
                {
                    (tmtControl as ComboBox).SelectedIndex = -1;
                }
                else if (tmtControl is NumericUpDown)
                {
                    (tmtControl as NumericUpDown).Value = 0;
                }
                else if (tmtControl is DateTimePicker)
                {
                    (tmtControl as DateTimePicker).Value = DateTime.Now;
                }
                else if (tmtControl is TMTTextButtonBox)
                {
                    tmtControl.Text = string.Empty;
                }
            }

            this.ClearChildDataGridViews(false);
            base.ClearData();
        }

        public override void ColumnManager()
        {
            if (this._focusedChildDataGridView != null)
            {
                List<TMTColumnManagerDialog.ColumnData> colList = new List<TMTColumnManagerDialog.ColumnData>();

                foreach (DataGridViewColumn viewCol in this._focusedChildDataGridView.Columns)
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

                    foreach (DataGridViewColumn viewCol in this._focusedChildDataGridView.Columns)
                    {
                        if (colDictionary.ContainsKey(viewCol.HeaderText))
                        {
                            viewCol.Visible = colDictionary[viewCol.HeaderText].Visibility;
                        }
                    }
                }
            }
            base.ColumnManager();
        }

        public void InitializeTMTControls()
        {
            this.SearchDialog.EntityList.Clear();
            TMTSearchDialog.SearchEntity searchEntity;
            TMTDataSourceInformation sourceInfor;
            var tmtControlList = this.panelControlContainer.Controls.Cast<Control>().OrderBy(c => c.TabIndex);
            foreach (Control tmtControl in tmtControlList)
            {
                sourceInfor = tmtControl.GetDataSourceInformation();

                if (sourceInfor != null &&
                    string.IsNullOrWhiteSpace(sourceInfor.DbColumnName) == false)
                {
                    if (tmtControl.Visible)
                    {
                        searchEntity = new TMTSearchDialog.SearchEntity()
                        {
                            Caption = sourceInfor.DbLabelText,
                            ColumnName = sourceInfor.DbColumnName,
                            DataType = sourceInfor.GetDbColumnType(),
                            LovView = sourceInfor.LovViewName
                        };

                        if (tmtControl is TMTCheckBox myTmtCheckBox)
                        {
                            searchEntity.IsCheckBox = true;
                            searchEntity.FalseValue = myTmtCheckBox.FalseValue;
                            searchEntity.TrueValue = myTmtCheckBox.TrueValue;
                            searchEntity.IndeterminateValue = myTmtCheckBox.IndeterminateValue;
                        }

                        this.SearchDialog.EntityList.Add(searchEntity);
                    }

                    this.DataSourceTable.Columns.Add(sourceInfor.DbColumnName, sourceInfor.GetDbColumnType());

                    if (tmtControl is TMTCheckBox tmtCheckBox)
                    {
                        tmtCheckBox.CheckedChanged += tmtControl_CheckedChanged;
                    }
                    else
                    {
                        tmtControl.TextChanged += TmtControl_TextChanged;
                    }
                    tmtControl.GotFocus += TmtControl_GotFocus;
                }
            }

            this.SetTMTControlDataBindings();

            this.SetTMTControlLovEvents();
        }

        private void tmtControl_CheckedChanged(object sender, EventArgs e)
        {
            if (this._isHeaderDataSourceTableLoading == false &&
                this._ignoreNavigatorSelectedIndexChange == false)
            {
                this.OnDataChanged();
            }
        }

        private void TmtControl_TextChanged(object sender, EventArgs e)
        {
            if (this._isHeaderDataSourceTableLoading == false &&
                this._ignoreNavigatorSelectedIndexChange == false)
            {
                this.OnDataChanged();
            }
        }

        private void TmtControl_GotFocus(object sender, EventArgs e)
        {
            this._focusedChildDataGridView = null;
            tmtButtonColumnManager.Enabled = false;
            tmtButtonAdd.Enabled = this.AddDataAllowed;
        }

        public bool IsChildDataGridViewFocused()
        {
            return (this._focusedChildDataGridView != null);
        }

        public virtual void LoadChildTableListData(DataRow primarySelectedRow)
        {
            if (backgroundWorkerMain.IsBusy == false)
            {
                PanelBackgroundWorkeArg arg = new PanelBackgroundWorkeArg();
                arg.Type = PanelBackgroundWorkeType.LoadChild;

                var childDataGridViewList = this.panelControlContainer.GetChildDataGridViewList();
                if (childDataGridViewList.Count > 0)
                {
                    foreach (TMTDataGridView childDataGridView in childDataGridViewList)
                    {
                        arg.ChildViewList.Add(new PanelBackgroundWorkeArg()
                        {
                            HeaderViewName = childDataGridView.ViewName,
                            HeaderSearchConditionTable = TMTExtendard.GetSearchConditionTable(),
                            LimitLoad = this.SearchDialog.LimitLoad,
                            IsCaseSensitive = true,
                            HeaderViewColumnDbNameList = childDataGridView.GetViewColumnDbNameDictionary().Keys.ToList(),
                            DefaultWhereStatment = childDataGridView.DefaultWhereStatment,
                            DefaultOrderByStatment = childDataGridView.DefaultOrderByStatment
                        });
                    }

                    this.SetChildTableListSearchConditionTables(primarySelectedRow, arg.ChildViewList);
                }

                backgroundWorkerMain.RunWorkerAsync(arg);
            }
        }

        public virtual void SetChildTableListSearchConditionTables(DataRow primarySelectedRow, List<PanelBackgroundWorkeArg> childViewList)
        {
            List<string> keyFieldListDbColumnList = new List<string>();

            TMTDataSourceInformation sourceInfor;
            var tmtControlList = this.panelControlContainer.Controls.Cast<Control>().OrderBy(c => c.TabIndex);
            foreach (Control tmtControl in tmtControlList)
            {
                sourceInfor = tmtControl.GetDataSourceInformation();

                if (sourceInfor != null &&
                    string.IsNullOrWhiteSpace(sourceInfor.DbColumnName) == false)
                {
                    if (sourceInfor.KeyColumn)
                    {
                        keyFieldListDbColumnList.Add(sourceInfor.DbColumnName);
                    }
                }
            }

            object searchValue;
            foreach (string keyFieldDbColumnName in keyFieldListDbColumnList)
            {
                searchValue = primarySelectedRow[keyFieldDbColumnName];

                foreach (PanelBackgroundWorkeArg childView in childViewList)
                {
                    childView.HeaderSearchConditionTable.Rows.Add(keyFieldDbColumnName, searchValue, searchValue.GetType().FullName, false);
                }
            }
        }

        public void LoadWindowWithDataWhenActive()
        {
            if (this.LoadDataWhenActive)
            {
                this.SearchDialog.DialogResult = DialogResult.Ignore;
                this.LoadData();
            }
        }

        public override void LoadData()
        {
            if (backgroundWorkerMain.IsBusy == false)
            {
                progressBarMain.Visible = true;
                this.UseWaitCursor = true;

                this.ClearChildDataGridViews(true);

                Dictionary<string, bool> viewColumnDbNameDictionary = this.GetViewColumnDbNameDictionary();

                PanelBackgroundWorkeArg arg = new PanelBackgroundWorkeArg()
                {
                    Type = PanelBackgroundWorkeType.Load,
                    HeaderSearchConditionTable = this.SearchDialog.GetSearchCondition(),
                    IsCaseSensitive = this.SearchDialog.IsCaseSensitive,
                    LimitLoad = this.SearchDialog.LimitLoad,
                    HeaderViewName = this.ViewName,
                    HeaderViewColumnDbNameList = viewColumnDbNameDictionary.Keys.ToList(),
                    DefaultWhereStatment = this.DefaultWhereStatment,
                    DefaultOrderByStatment = this.DefaultOrderByStatment
                };
                string dbColumnName;
                foreach (DataRow searchRow in arg.HeaderSearchConditionTable.Rows)
                {
                    dbColumnName = searchRow["COLUMN"].ToString();
                    if (viewColumnDbNameDictionary.ContainsKey(dbColumnName))
                    {
                        searchRow["IS_FUNCTION"] = viewColumnDbNameDictionary[dbColumnName];
                    }
                }

                backgroundWorkerMain.RunWorkerAsync(arg);
            }
        }

        public Dictionary<string, bool> GetViewColumnDbNameDictionary()
        {
            TMTDataSourceInformation sourceInfor;
            var tmtControlList = this.panelControlContainer.Controls.Cast<Control>().OrderBy(c => c.TabIndex);
            List<TMTDataSourceInformation> sourceInforList = new List<TMTDataSourceInformation>();
            foreach (Control tmtControl in tmtControlList)
            {
                sourceInfor = tmtControl.GetDataSourceInformation();

                if (sourceInfor != null &&
                    string.IsNullOrWhiteSpace(sourceInfor.DbColumnName) == false)
                {
                    sourceInforList.Add(sourceInfor);
                }
            }
            if (sourceInforList != null && sourceInforList.Count() > 0)
            {
                return sourceInforList.ToDictionary(c => c.DbColumnName, c => c.IsFuntion);
            }
            return null;
        }

        public override void OnValidateBeforeSave(TMTUserControl.DataValidatingEventArgs dataToBeSaved)
        {
            TMTDataSourceInformation sourceInfor;
            string connectedLabelText = string.Empty;
            if (dataToBeSaved.DataToBeSaved.Tables.Contains(this.TableName))
            {
                List<Control> mandatoryFieldList = new List<Control>();
                var tmtControlList = this.panelControlContainer.Controls.Cast<Control>().OrderBy(c => c.TabIndex);
                foreach (Control tmtControl in tmtControlList)
                {
                    sourceInfor = tmtControl.GetDataSourceInformation();
                    if (sourceInfor != null &&
                        sourceInfor.MandatoryColumn)
                    {
                        if (((tmtControl is NumericUpDown) && (tmtControl as NumericUpDown).ReadOnly == false) ||
                            ((tmtControl is TextBox) && (tmtControl as TextBox).ReadOnly == false))
                        {
                            mandatoryFieldList.Add(tmtControl);
                        }
                    }
                }

                if (mandatoryFieldList != null &&
                    mandatoryFieldList.Count > 0)
                {
                    DataRowCollection rowCollection = dataToBeSaved.DataToBeSaved.Tables[this.TableName].Rows;
                    foreach (DataRow row in rowCollection)
                    {
                        if (row.RowState != DataRowState.Deleted)
                        {
                            foreach (Control mandatoryField in mandatoryFieldList)
                            {
                                sourceInfor = mandatoryField.GetDataSourceInformation();

                                if (sourceInfor != null)
                                {
                                    if (mandatoryField is TMTTextBox mandatoryTMTTextBox)
                                    {
                                        connectedLabelText = mandatoryTMTTextBox.ConnectedLabel.Text;
                                    }
                                    if (mandatoryField is TMTTextButtonBox mandatoryTMTTextButtonBox)
                                    {
                                        connectedLabelText = mandatoryTMTTextButtonBox.ConnectedLabel.Text;
                                    }
                                    else if (mandatoryField is TMTNumericUpDown mandatoryTMTNumericUpDown)
                                    {
                                        connectedLabelText = mandatoryTMTNumericUpDown.ConnectedLabel.Text;
                                    }
                                    else if (mandatoryField is TMTComboBox mandatoryTMTComboBox)
                                    {
                                        connectedLabelText = mandatoryTMTComboBox.ConnectedLabel.Text;
                                    }
                                    else if (mandatoryField is TMTDateTimePicker mandatoryTMTDateTimePicker)
                                    {
                                        connectedLabelText = mandatoryTMTDateTimePicker.ConnectedLabel.Text;
                                    }
                                    else if (mandatoryField is TMTCheckBox mandatoryTMTCheckBox)
                                    {
                                        connectedLabelText = mandatoryTMTCheckBox.Text;
                                    }

                                    dataToBeSaved.CancelSave = this.ShowEmptyExclamation(row[sourceInfor.DbColumnName], connectedLabelText);
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

            var childDataGridViewList = this.panelControlContainer.GetChildDataGridViewList();
            if (childDataGridViewList.Count > 0)
            {
                foreach (TMTDataGridView childDataView in childDataGridViewList)
                {
                    if (dataToBeSaved.DataToBeSaved.Tables.Contains(childDataView.TableName))
                    {
                        List<DataGridViewColumn> mandatoryViewColumnList = childDataView.GetMandatoryViewColumnList();

                        if (mandatoryViewColumnList != null &&
                            mandatoryViewColumnList.Count > 0)
                        {
                            DataRowCollection rowCollection = dataToBeSaved.DataToBeSaved.Tables[childDataView.TableName].Rows;
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
            }

            base.OnValidateBeforeSave(dataToBeSaved);
        }

        public override void RemoveData()
        {
            if (this._focusedChildDataGridView != null && this._focusedChildDataGridView.DeleteDataAllowed)
            {
                if (this._focusedChildDataGridView.SelectedRows != null && this._focusedChildDataGridView.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show(this, Properties.Resources.QUESTION_DeleteConfirmAll,
                                              Properties.Resources.QUESTION_Delete,
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow deleteRow in this._focusedChildDataGridView.SelectedRows)
                        {
                            this._focusedChildDataGridView.Rows.RemoveAt(deleteRow.Index);
                        }
                    }
                }
            }
            else
            {
                if (tmtNavigator.SelectedValue != null && string.IsNullOrWhiteSpace(tmtNavigator.SelectedValue.ToString()) == false)
                {
                    int foundIndex = bindingSourceMain.Find(tmtNavigator.ValueMember, tmtNavigator.SelectedValue);
                    if (foundIndex > -1)
                    {
                        if (MessageBox.Show(this, string.Format(Properties.Resources.QUESTION_DeleteConfirm, tmtNavigator.SelectedValue), Properties.Resources.QUESTION_Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.DataSourceTable.Rows[foundIndex].Delete();
                        }
                    }
                }
            }
            base.RemoveData();
        }

        public override void SaveData()
        {
            if (backgroundWorkerMain.IsBusy == false)
            {
                this._ignoreNavigatorSelectedIndexChange = true;
                if (tmtNavigator.SelectedValue != null)
                {
                    tmtNavigator.Tag = tmtNavigator.SelectedValue;
                }

                PanelBackgroundWorkeArg arg = new PanelBackgroundWorkeArg()
                {
                    Type = PanelBackgroundWorkeType.Save
                };

                this.Validate();
                this.bindingSourceMain.EndEdit();
                DataTable changedData = this.DataSourceTable.GetDataSourceTableChanges(this.TableName);
                if (changedData != null && changedData.Rows.Count > 0)
                {
                    List<string> readOnlyColumnsToRemove = new List<string>();
                    HashSet<string> columnInView = new HashSet<string>();
                    TMTDataSourceInformation sourceInfor;
                    foreach (Control tmtControl in this.panelControlContainer.Controls)
                    {
                        sourceInfor = tmtControl.GetDataSourceInformation();

                        if (sourceInfor != null &&
                            string.IsNullOrWhiteSpace(sourceInfor.DbColumnName) == false)
                        {
                            columnInView.Add(sourceInfor.DbColumnName);
                            if (sourceInfor.KeyColumn == false && sourceInfor.MandatoryColumn == false)
                            {
                                if (((tmtControl is NumericUpDown) && (tmtControl as NumericUpDown).ReadOnly) ||
                                    ((tmtControl is TextBox) && (tmtControl as TextBox).ReadOnly))
                                {
                                    readOnlyColumnsToRemove.Add(sourceInfor.DbColumnName);
                                }
                            }
                        }
                    }

                    var columnNameList = changedData.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
                    foreach (string colName in columnNameList)
                    {
                        if (columnInView.Contains(colName) == false)
                        {
                            changedData.Columns.Remove(colName);
                        }
                    }

                    foreach (string readOnlyColumnName in readOnlyColumnsToRemove)
                    {
                        changedData.Columns.Remove(readOnlyColumnName);
                    }

                    arg.ChangedDataSet.Tables.Add(changedData);
                }

                var childDataGridViewList = this.panelControlContainer.GetChildDataGridViewList();
                if (childDataGridViewList.Count > 0)
                {
                    foreach (TMTDataGridView childDataGridView in childDataGridViewList)
                    {
                        changedData = childDataGridView.GetDataSourceTableChanges();
                        if (changedData != null && changedData.Rows.Count > 0)
                        {
                            arg.ChangedDataSet.Tables.Add(changedData);
                        }
                    }
                }

                if (arg.ChangedDataSet.Tables.Count > 0)
                {
                    DataValidatingEventArgs validateArg = new DataValidatingEventArgs();
                    validateArg.DataToBeSaved = arg.ChangedDataSet;
                    validateArg.CancelSave = false;
                    this.OnValidateBeforeSave(validateArg);

                    if (validateArg.CancelSave == false)
                    {
                        progressBarMain.Visible = true;
                        this.UseWaitCursor = true;

                        backgroundWorkerMain.RunWorkerAsync(arg);
                    }
                }
                else
                {
                    MessageBox.Show(this, Properties.Resources.MSG_SAVE_Null,
                                          Properties.Resources.MSG_HEADER_Save,
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Asterisk);
                }

                this._ignoreNavigatorSelectedIndexChange = false;
            }
        }

        protected DataRow GetDataSourceSelectedRow()
        {
            this._ignoreNavigatorSelectedIndexChange = true;
            DataRow selectedRow = null;

            if (tmtNavigator.SelectedValue != null)
            {
                int foundIndex = bindingSourceMain.Find(tmtNavigator.ValueMember, tmtNavigator.SelectedValue);
                if (foundIndex > -1)
                {
                    bindingSourceMain.Position = foundIndex;

                    if (string.IsNullOrWhiteSpace(tmtNavigator.SelectedValue.ToString()) == false)
                    {
                        DataRow primarySelectedRow = this.DataSourceTable.Rows[foundIndex];
                        if (primarySelectedRow.RowState != DataRowState.Deleted)
                        {
                            selectedRow = primarySelectedRow;
                        }
                    }
                }
            }
            this._ignoreNavigatorSelectedIndexChange = false;
            return selectedRow;
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
                    if (e.Result != null && e.Result is PanelBackgroundWorkeArg)
                    {
                        PanelBackgroundWorkeArg arg = e.Result as PanelBackgroundWorkeArg;

                        if (arg.Type == PanelBackgroundWorkeType.Load)
                        {
                            this._isHeaderDataSourceTableLoading = true;

                            this.DataSourceTable = arg.ChangedDataSet.Tables[arg.HeaderViewName];

                            bindingSourceMain.SuspendBinding();
                            bindingSourceMain.DataSource = this.DataSourceTable;
                            bindingSourceMain.ResumeBinding();

                            tmtNavigator.DataSourceTable = this.DataSourceTable;

                            base.LoadData();

                            this._isHeaderDataSourceTableLoading = false;
                            tmtButtonDuplicate.Enabled = this.AddDataAllowed;
                            tmtButtonDelete.Enabled = this.DeleteDataAllowed;
                        }
                        else if (arg.Type == PanelBackgroundWorkeType.LoadChild)
                        {
                            this._isChildDataSourceTablesLoading = true;

                            var childDataGridViewList = this.panelControlContainer.GetChildDataGridViewList();
                            if (childDataGridViewList.Count > 0)
                            {
                                foreach (TMTDataGridView childDataGridView in childDataGridViewList)
                                {
                                    childDataGridView.DataSourceTable = arg.ChangedDataSet.Tables[childDataGridView.ViewName];
                                    childDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                                }
                            }

                            if (tmtNavigator.Tag != null)
                            {
                                int foundIndex = bindingSourceMain.Find(tmtNavigator.ValueMember, tmtNavigator.Tag);
                                if (foundIndex > -1)
                                {
                                    tmtNavigator.SelectedValue = tmtNavigator.Tag;
                                }
                                else
                                {
                                    tmtNavigator.SelectedIndex = 0;
                                }

                                tmtNavigator.Tag = null;
                            }

                            progressBarMain.Visible = false;
                            this.UseWaitCursor = false;

                            this._isChildDataSourceTablesLoading = false;
                        }
                        else if (arg.Type == PanelBackgroundWorkeType.Save)
                        {
                            if (arg.SaveResults.All(i => i > 0))
                            {
                                base.SaveData();
                                this.LoadData();
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

        private void childDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                this.OnDataChanged();
            }
        }

        private void childDataGridView_GotFocus(object sender, EventArgs e)
        {
            if (sender is TMTDataGridView gridView)
            {
                if (gridView.Visible)
                {
                    this._focusedChildDataGridView = gridView;
                    tmtButtonColumnManager.Enabled = true;
                    tmtButtonAdd.Enabled = this._focusedChildDataGridView.AddDataAllowed && this.DataSourceTable.Rows.Count > 0;
                }
            }
        }

        private void ChildDataGridView_VisibleChanged(object sender, EventArgs e)
        {
            if (sender is TMTDataGridView gridView)
            {
                if (gridView.Visible)
                {
                    this._focusedChildDataGridView = gridView;
                    tmtButtonColumnManager.Enabled = true;
                    tmtButtonAdd.Enabled = this._focusedChildDataGridView.AddDataAllowed && this.DataSourceTable.Rows.Count > 0;
                }
            }
        }

        private void childDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowCount > 0 && this._isChildDataSourceTablesLoading == false && (sender as TMTDataGridView).AddDataAllowed)
            {
                this.OnDataChanged();
            }
        }

        private void childDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (e.RowCount > 0 && this._isDataGridViewDataClear == false && (sender as TMTDataGridView).DeleteDataAllowed)
            {
                this.OnDataChanged();
            }
        }

        private void ClearChildDataGridViews(bool enable)
        {
            this._isDataGridViewDataClear = true;
            var childDataGridViewList = this.panelControlContainer.GetChildDataGridViewList();
            if (childDataGridViewList.Count > 0)
            {
                foreach (TMTDataGridView childDataGridView in childDataGridViewList)
                {
                    if (childDataGridView.DataSourceTable != null)
                    {
                        childDataGridView.DataSourceTable.Clear();
                    }
                    childDataGridView.Enabled = enable;
                }
            }
            this._isDataGridViewDataClear = false;
        }

        private void SetTMTControlDataBindings()
        {
            bindingSourceMain.DataSource = this.DataSourceTable;
            //tmtNavigator.DataSource = this.DataSourceTable;

            TMTDataSourceInformation sourceInfor;
            string propetyName;
            foreach (Control tmtControl in this.panelControlContainer.Controls)
            {
                sourceInfor = tmtControl.GetDataSourceInformation();

                if (sourceInfor != null &&
                    string.IsNullOrWhiteSpace(sourceInfor.DbColumnName) == false)
                {
                    if (tmtControl is NumericUpDown ||
                        tmtControl is DateTimePicker)
                    {
                        propetyName = "Value";
                    }
                    else if (tmtControl is TMTCheckBox)
                    {
                        propetyName = "DbValue";
                    }
                    else if (tmtControl is ComboBox)
                    {
                        propetyName = "SelectedValue";
                    }
                    else
                    {
                        propetyName = "Text";
                    }
                    tmtControl.DataBindings.Add(propetyName, this.bindingSourceMain, sourceInfor.DbColumnName, true, DataSourceUpdateMode.OnValidation);
                }
            }
        }

        private void SetTMTControlLovEvents()
        {
            var childDataGridViewList = this.panelControlContainer.GetChildDataGridViewList();
            if (childDataGridViewList.Count > 0)
            {
                foreach (TMTDataGridView childDataGridView in childDataGridViewList)
                {
                    childDataGridView.CellValueChanged += childDataGridView_CellValueChanged;
                    childDataGridView.RowsRemoved += childDataGridView_RowsRemoved;
                    childDataGridView.RowsAdded += childDataGridView_RowsAdded;
                    childDataGridView.GotFocus += childDataGridView_GotFocus;
                    childDataGridView.VisibleChanged += ChildDataGridView_VisibleChanged;
                }
            }
        }

        private bool ShowEmptyExclamation(object oValue, string fieldLabelText)
        {
            bool cancelSave = false;
            if (oValue == null || string.IsNullOrWhiteSpace(oValue.ToString()))
            {
                MessageBox.Show(this,
                    string.Format(Properties.Resources.Exclamation_MandatoryValueEmpty, fieldLabelText),
                    Properties.Resources.Exclamation_MandatoryEmpty,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                cancelSave = true;
            }
            return cancelSave;
        }

        private void tmtNavigator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._ignoreNavigatorSelectedIndexChange == false)
            {
                bool childLoaded = false;
                DataRow primarySelectedRow = this.GetDataSourceSelectedRow();
                if (primarySelectedRow != null)
                {
                    if (progressBarMain.Visible == false)
                    {
                        progressBarMain.Visible = true;
                        this.UseWaitCursor = true;
                    }

                    this.LoadChildTableListData(primarySelectedRow);
                    childLoaded = true;
                }

                if (childLoaded == false)
                {
                    this.ClearChildDataGridViews(false);

                    progressBarMain.Visible = false;
                    this.UseWaitCursor = false;
                }
            }
        }

        private void TMTPanelForm_DataLoaded(object sender, EventArgs e)
        {
            if (this.DataSourceTable == null || this.DataSourceTable.Rows.Count == 0)
            {
                MessageBox.Show(this, Properties.Resources.Exclamation_NoDataFoundText,
                                      Properties.Resources.Exclamation_NoDataFound,
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Asterisk);
            }
        }
    }
}
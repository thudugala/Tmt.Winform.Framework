using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using TMTControls.TMTDatabaseUI;
using TMTControls.TMTDataGrid;
using TMTControls.TMTDialogs;

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

            this.AddDataAllowed = true;
            this.DeleteDataAllowed = true;
        }

        private void TMTPanelForm_Load(object sender, EventArgs e)
        {
            this.DataSourceTable = new DataTable(this.ViewName)
            {
                Locale = CultureInfo.InvariantCulture
            };

            this.InitializeTMTControls();

            foreach (TMTDataGridView childDataGridView in this.panelControlContainer.GetChildDataGridViewList())
            {
                childDataGridView.InitializeTableControls();
            }

            tmtButtonAdd.Enabled = this.AddDataAllowed;
            tmtButtonDuplicate.Enabled = this.AddDataAllowed;
            tmtButtonDelete.Enabled = this.DeleteDataAllowed;

            tmtNavigator.DataSourceTable = this.DataSourceTable;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataTable DataSourceTable { get; set; }

        [Category("TMT Data")]
        public string TableName { get; set; }

        [Category("TMT Data")]
        public string ViewName { get; set; }

        [Category("TMT Data")]
        public string DefaultWhereStatement { get; set; }

        [Category("TMT Data")]
        public string DefaultOrderByStatement { get; set; }

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

                this.ClearControlValues();

                this.ClearChildDataGridViews(true);
            }

            base.AddData();
        }

        private void ClearControlValues()
        {
            foreach (var tmtControl in this.panelControlContainer.Controls)
            {
                switch (tmtControl)
                {
                    case TextBox myTextBox:
                        myTextBox.Text = string.Empty;
                        break;

                    case CheckBox myCheckBox:
                        myCheckBox.Checked = false;
                        break;

                    case ComboBox myComboBox:
                        myComboBox.SelectedIndex = -1;
                        break;

                    case NumericUpDown myNumericUpDown:
                        myNumericUpDown.Value = 0;
                        break;

                    case DateTimePicker myDateTimePicker:
                        myDateTimePicker.Value = DateTime.Now;
                        break;

                    case TMTTextButtonBox myTMTTextButtonBox:
                        myTMTTextButtonBox.Text = string.Empty;
                        break;
                }
            }
        }

        public override void ClearData()
        {
            this.DataSourceTable.Clear();
            tmtNavigator.SelectedIndex = -1;

            this.ClearControlValues();

            this.ClearChildDataGridViews(false);
            base.ClearData();
        }

        public override void ColumnManager()
        {
            if (this._focusedChildDataGridView != null)
            {
                var colList = new List<ColumnData>();

                foreach (DataGridViewColumn viewCol in this._focusedChildDataGridView.Columns)
                {
                    if ((viewCol is DataGridViewButtonColumn) == false)
                    {
                        colList.Add(new ColumnData(viewCol.HeaderText, viewCol.Visible));
                    }
                }

                ColumnManagerDialog.SetColumnList(colList);
                if (ColumnManagerDialog.ShowDialog(this) == DialogResult.OK)
                {
                    var colDictionary = ColumnManagerDialog.GetColumnList().ToDictionary(c => c.Name);

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
            var tmtControlList = this.panelControlContainer.Controls.Cast<Control>().Where(c => c is ITMTDatabaseUIControl).OrderBy(c => c.TabIndex);
            foreach (var tmtControl in tmtControlList)
            {
                var sourceInfor = tmtControl as ITMTDatabaseUIControl;

                if (string.IsNullOrWhiteSpace(sourceInfor.DbColumnName) == false)
                {
                    var myTmtCheckBox = tmtControl as TMTCheckBox;

                    if (tmtControl.Visible)
                    {
                        var myTMTTextButtonBox = tmtControl as TMTTextButtonBox;

                        var searchEntity = new SearchEntity()
                        {
                            Caption = sourceInfor.GetLableText(),
                            ColumnName = sourceInfor.DbColumnName,
                            DataType = sourceInfor.GetDbColumnSystemType()
                        };
                        if (myTMTTextButtonBox != null)
                        {
                            searchEntity.ListOfValueView = myTMTTextButtonBox.ListOfValueViewName;
                        }

                        if (myTmtCheckBox != null)
                        {
                            searchEntity.IsCheckBox = true;
                            searchEntity.FalseValue = myTmtCheckBox.FalseValue;
                            searchEntity.TrueValue = myTmtCheckBox.TrueValue;
                            searchEntity.IndeterminateValue = myTmtCheckBox.IndeterminateValue;
                        }

                        this.SearchDialog.EntityList.Add(searchEntity);
                    }

                    this.DataSourceTable.Columns.Add(sourceInfor.DbColumnName, sourceInfor.GetDbColumnSystemType());

                    if (myTmtCheckBox != null)
                    {
                        myTmtCheckBox.CheckedChanged += TmtControl_CheckedChanged;
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

        private void TmtControl_CheckedChanged(object sender, EventArgs e)
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
                var arg = new PanelBackgroundWorkEventArgs()
                {
                    WorkType = PanelBackgroundWorkType.LoadChild
                };
                var childDataGridViewList = this.panelControlContainer.GetChildDataGridViewList();
                if (childDataGridViewList.Count > 0)
                {
                    foreach (var childDataGridView in childDataGridViewList)
                    {
                        var childViewArg = new PanelBackgroundWorkEventArgs()
                        {
                            HeaderViewName = childDataGridView.ViewName,
                            HeaderSearchConditionTable = TMTExtend.GetSearchConditionTable(),
                            LimitLoad = this.SearchDialog.LimitLoad,
                            IsCaseSensitive = true,
                            DefaultWhereStatement = childDataGridView.DefaultWhereStatement,
                            DefaultOrderByStatement = childDataGridView.DefaultOrderByStatement,
                            LoadSchema = childDataGridView.LoadSchema
                        };
                        childViewArg.HeaderViewColumnDbNameListAddRange(childDataGridView.GetViewColumnDbNameDictionary().Keys.ToList());
                        arg.ChildViewList.Add(childViewArg);
                    }

                    this.SetChildTableListSearchConditionTables(primarySelectedRow, arg.ChildViewList);
                }

                backgroundWorkerMain.RunWorkerAsync(arg);
            }
        }

        public virtual void SetChildTableListSearchConditionTables(DataRow primarySelectedRow, IList<PanelBackgroundWorkEventArgs> childViewList)
        {
            if (primarySelectedRow == null)
            {
                throw new ArgumentNullException(nameof(primarySelectedRow));
            }

            if (childViewList == null)
            {
                throw new ArgumentNullException(nameof(childViewList));
            }

            var keyFieldListDbColumnList = new List<string>();

            var controlList = this.panelControlContainer.Controls.Cast<Control>().OrderBy(c => c.TabIndex);
            var tmtControlList = controlList.Where(c => c is ITMTDatabaseUIControl).Cast<ITMTDatabaseUIControl>();
            foreach (var tmtControl in tmtControlList)
            {
                if (string.IsNullOrWhiteSpace(tmtControl.DbColumnName) == false)
                {
                    if (tmtControl.KeyColumn)
                    {
                        keyFieldListDbColumnList.Add(tmtControl.DbColumnName);
                    }
                }
            }

            object searchValue;
            foreach (var keyFieldDbColumnName in keyFieldListDbColumnList)
            {
                searchValue = primarySelectedRow[keyFieldDbColumnName];

                foreach (var childViewEventArgs in childViewList)
                {
                    childViewEventArgs.HeaderSearchConditionTable.Rows.Add(keyFieldDbColumnName, searchValue, searchValue.GetType().FullName, false);
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
                this._ignoreNavigatorSelectedIndexChange = true;
                tmtNavigator.DataSourceTable.Clear();
                this.ClearChildDataGridViews(true);

                var viewColumnDbNameDictionary = this.GetViewColumnDbNameDictionary();

                var arg = new PanelBackgroundWorkEventArgs()
                {
                    WorkType = PanelBackgroundWorkType.Load,
                    HeaderSearchConditionTable = this.SearchDialog.GetSearchCondition(),
                    IsCaseSensitive = this.SearchDialog.IsCaseSensitive,
                    LimitLoad = this.SearchDialog.LimitLoad,
                    HeaderViewName = this.ViewName,
                    DefaultWhereStatement = this.DefaultWhereStatement,
                    DefaultOrderByStatement = this.DefaultOrderByStatement
                };
                arg.HeaderViewColumnDbNameListAddRange(viewColumnDbNameDictionary.Keys.ToList());
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

                this._ignoreNavigatorSelectedIndexChange = false;
            }
        }

        public Dictionary<string, bool> GetViewColumnDbNameDictionary()
        {
            var controlList = this.panelControlContainer.Controls.Cast<Control>().OrderBy(c => c.TabIndex);
            var tmtControlList = controlList.Where(c => c is ITMTDatabaseUIControl).Cast<ITMTDatabaseUIControl>();
            var sourceInforDictionary = new Dictionary<string, bool>();
            foreach (var tmtControl in tmtControlList)
            {
                if (string.IsNullOrWhiteSpace(tmtControl.DbColumnName) == false)
                {
                    sourceInforDictionary.Add(tmtControl.DbColumnName, false); //IsFuntoion == false
                }
            }
            return sourceInforDictionary;
        }

        public override void OnValidateBeforeSave(DataValidatingEventArgs dataToBeSaved)
        {
            if (dataToBeSaved == null)
            {
                throw new ArgumentNullException(nameof(dataToBeSaved));
            }

            try
            {
                if (dataToBeSaved.DataToBeSaved.Tables.Contains(this.TableName))
                {
                    var mandatoryFieldList = this.GetMandatoryControlList();

                    if (mandatoryFieldList.Count > 0)
                    {
                        var rowCollection = dataToBeSaved.DataToBeSaved.Tables[this.TableName].Rows
                                                                                              .Cast<DataRow>()
                                                                                              .Where(r => r.RowState != DataRowState.Deleted);
                        foreach (DataRow row in rowCollection)
                        {
                            foreach (var mandatoryField in mandatoryFieldList)
                            {
                                dataToBeSaved.CancelSave = this.ShowEmptyExclamation(mandatoryField, row[mandatoryField.DbColumnName], mandatoryField.GetLableText());
                                if (dataToBeSaved.CancelSave)
                                {
                                    return;
                                }
                            }
                        }
                    }
                }

                var childDataGridViewList = this.panelControlContainer.GetChildDataGridViewList();
                if (childDataGridViewList.Count > 0)
                {
                    var childDataGridViewSaveList = childDataGridViewList.Where(t => dataToBeSaved.DataToBeSaved.Tables.Contains(t.TableName));
                    foreach (var childDataView in childDataGridViewSaveList)
                    {
                        var mandatoryViewColumnList = childDataView.GetMandatoryViewColumnList();

                        if (mandatoryViewColumnList != null &&
                            mandatoryViewColumnList.Count > 0)
                        {
                            var rowCollection = dataToBeSaved.DataToBeSaved.Tables[childDataView.TableName].Rows.Cast<DataRow>().Where(r => r.RowState != DataRowState.Deleted);
                            foreach (var row in rowCollection)
                            {
                                foreach (var mandatoryViewColumn in mandatoryViewColumnList)
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

                base.OnValidateBeforeSave(dataToBeSaved);
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_SavingDataValidation);
            }
        }

        private IList<ITMTDatabaseUIControl> GetMandatoryControlList()
        {
            var mandatoryFieldList = new List<ITMTDatabaseUIControl>();
            var controlList = this.panelControlContainer.Controls.Cast<Control>()
                                                                 .Where(c => c is ITMTDatabaseUIControl uiControl && uiControl.MandatoryColumn)
                                                                 .OrderBy(c => c.TabIndex)
                                                                 .Cast<ITMTDatabaseUIControl>();
            foreach (var tmtControl in controlList)
            {
                if (tmtControl is NumericUpDown myNumericUpDown)
                {
                    if (myNumericUpDown.ReadOnly == false)
                    {
                        mandatoryFieldList.Add(tmtControl);
                    }
                }
                else if (tmtControl is TextBox myTextBox)
                {
                    if (myTextBox.ReadOnly == false)
                    {
                        mandatoryFieldList.Add(tmtControl);
                    }
                }
                else
                {
                    mandatoryFieldList.Add(tmtControl);
                }
            }

            return mandatoryFieldList;
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
                        if (MessageBox.Show(this, string.Format(CultureInfo.InvariantCulture, Properties.Resources.QUESTION_DeleteConfirm, tmtNavigator.SelectedValue), Properties.Resources.QUESTION_Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
            try
            {
                if (backgroundWorkerMain.IsBusy)
                {
                    return;
                }

                this._ignoreNavigatorSelectedIndexChange = true;
                tmtNavigator.Tag = tmtNavigator.SelectedValue;

                var arg = new PanelBackgroundWorkEventArgs
                {
                    WorkType = PanelBackgroundWorkType.Save
                };

                this.Validate();
                this.bindingSourceMain.EndEdit();
                this.GetFormChangedDataTable(arg);
                this.GetChildTableChangedDataTable(arg);

                if (arg.ChangedDataSet.Tables.Count > 0)
                {
                    var validateArg = new DataValidatingEventArgs
                    {
                        DataToBeSaved = arg.ChangedDataSet,
                        CancelSave = false
                    };
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
            catch
            {
                throw;
            }
        }

        private void GetFormChangedDataTable(PanelBackgroundWorkEventArgs arg)
        {
            var changedData = this.DataSourceTable.GetDataSourceTableChanges(this.TableName);
            if (changedData != null && changedData.Rows.Count > 0)
            {
                var myControls = this.panelControlContainer.Controls.Cast<Control>()
                                                                     .Where(c => c is ITMTDatabaseUIControl myUiControl && string.IsNullOrWhiteSpace(myUiControl.DbColumnName) == false)
                                                                     .Cast<ITMTDatabaseUIControl>();

                var columnInView = new HashSet<string>(myControls.Select(c => c.DbColumnName));
                var readOnlyColumnsToRemove = new List<string>();
                foreach (var tmtControl in myControls)
                {
                    if (tmtControl.KeyColumn == false && tmtControl.MandatoryColumn == false)
                    {
                        if ((tmtControl is NumericUpDown myNumericUpDown && myNumericUpDown.ReadOnly) ||
                            (tmtControl is TextBox myTextBox && myTextBox.ReadOnly))
                        {
                            readOnlyColumnsToRemove.Add(tmtControl.DbColumnName);
                        }
                    }
                }

                var columnNameList = changedData.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
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
        }

        private void GetChildTableChangedDataTable(PanelBackgroundWorkEventArgs arg)
        {
            var childDataGridViewList = this.panelControlContainer.GetChildDataGridViewList();
            if (childDataGridViewList.Count > 0)
            {
                foreach (var childDataGridView in childDataGridViewList)
                {
                    var changedChildData = childDataGridView.GetDataSourceTableChanges();
                    if (changedChildData != null && changedChildData.Rows.Count > 0)
                    {
                        arg.ChangedDataSet.Tables.Add(changedChildData);
                    }
                }
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
                        if (this.DataSourceTable.Rows.Count > foundIndex)
                        {
                            var primarySelectedRow = this.DataSourceTable.Rows[foundIndex];
                            if (primarySelectedRow.RowState != DataRowState.Deleted)
                            {
                                selectedRow = primarySelectedRow;
                            }
                        }
                    }
                }
            }
            this._ignoreNavigatorSelectedIndexChange = false;
            return selectedRow;
        }

        private void BackgroundWorkerMain_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Do not access the form's BackgroundWorker reference directly.
                // Instead, use the reference provided by the sender parameter.
                var worker = sender as BackgroundWorker;

                if (worker.CancellationPending == false)
                {
                    if (e.Argument is PanelBackgroundWorkEventArgs arg)
                    {
                        this.OnDataManager(arg);

                        e.Result = arg;
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

        private void BackgroundWorkerMain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    TMTErrorDialog.Show(this, e.Error, Properties.Resources.ERROR_BW_Issue);
                }
                else
                {
                    if (e.Result != null && e.Result is PanelBackgroundWorkEventArgs arg)
                    {
                        if (arg.WorkType == PanelBackgroundWorkType.Load)
                        {
                            this._isHeaderDataSourceTableLoading = true;

                            this.DataSourceTable = arg.ChangedDataSet.Tables[arg.HeaderViewName];

                            bindingSourceMain.SuspendBinding();
                            bindingSourceMain.DataSource = this.DataSourceTable;
                            bindingSourceMain.ResumeBinding();

                            tmtNavigator.DataSourceTable = this.DataSourceTable;

                            base.LoadData();

                            tmtButtonDuplicate.Enabled = this.AddDataAllowed;
                            tmtButtonDelete.Enabled = this.DeleteDataAllowed;

                            this._isHeaderDataSourceTableLoading = false;
                        }
                        else if (arg.WorkType == PanelBackgroundWorkType.LoadChild)
                        {
                            this._isChildDataSourceTablesLoading = true;

                            var childDataGridViewList = this.panelControlContainer.GetChildDataGridViewList();
                            if (childDataGridViewList.Count > 0)
                            {
                                foreach (var childDataGridView in childDataGridViewList)
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
                        else if (arg.WorkType == PanelBackgroundWorkType.Save)
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

        private void ChildDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                this.OnDataChanged();
            }
        }

        private void ChildDataGridView_GotFocus(object sender, EventArgs e)
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

        private void ChildDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowCount > 0 && this._isChildDataSourceTablesLoading == false && (sender as TMTDataGridView).AddDataAllowed)
            {
                this.OnDataChanged();
            }
        }

        private void ChildDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
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
                foreach (var childDataGridView in childDataGridViewList)
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

            string propetyName;
            foreach (var tmtControl in this.panelControlContainer.Controls.Cast<Control>().Where(c => c is ITMTDatabaseUIControl))
            {
                var dbColumnName = (tmtControl as ITMTDatabaseUIControl).DbColumnName;

                if (string.IsNullOrWhiteSpace(dbColumnName) == false)
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
                    tmtControl.DataBindings.Add(propetyName, this.bindingSourceMain, dbColumnName, true, DataSourceUpdateMode.OnValidation);
                }
            }
        }

        private void SetTMTControlLovEvents()
        {
            var childDataGridViewList = this.panelControlContainer.GetChildDataGridViewList();
            if (childDataGridViewList.Count > 0)
            {
                foreach (var childDataGridView in childDataGridViewList)
                {
                    childDataGridView.CellValueChanged += ChildDataGridView_CellValueChanged;
                    childDataGridView.RowsRemoved += ChildDataGridView_RowsRemoved;
                    childDataGridView.RowsAdded += ChildDataGridView_RowsAdded;
                    childDataGridView.GotFocus += ChildDataGridView_GotFocus;
                    childDataGridView.VisibleChanged += ChildDataGridView_VisibleChanged;
                }
            }
        }

        private bool ShowEmptyExclamation(ITMTDatabaseUIControl control, object oValue, string fieldLabelText)
        {
            bool cancelSave = false;
            if (oValue == null || string.IsNullOrWhiteSpace(oValue.ToString()))
            {
                string message = string.Format(CultureInfo.InvariantCulture, Properties.Resources.Exclamation_MandatoryValueEmpty, fieldLabelText);

                errorProviderMain.SetError(control as Control, message);

                MessageBox.Show(this,
                    message,
                    Properties.Resources.Exclamation_MandatoryEmpty,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                cancelSave = true;
            }
            else
            {
                errorProviderMain.SetError(control as Control, string.Empty);
            }
            return cancelSave;
        }

        private bool ShowEmptyExclamation(object oValue, string fieldLabelText)
        {
            bool cancelSave = false;
            if (oValue == null || string.IsNullOrWhiteSpace(oValue.ToString()))
            {
                MessageBox.Show(this,
                    string.Format(CultureInfo.InvariantCulture, Properties.Resources.Exclamation_MandatoryValueEmpty, fieldLabelText),
                    Properties.Resources.Exclamation_MandatoryEmpty,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                cancelSave = true;
            }
            return cancelSave;
        }

        private void TmtNavigator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._ignoreNavigatorSelectedIndexChange == false)
            {
                bool childLoaded = false;
                var primarySelectedRow = this.GetDataSourceSelectedRow();
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TMTControls.TMTDataGrid;

namespace TMTControls.TMTPanels
{
    [DefaultEvent("LovLoading")]
    public partial class TMTPanelForm : TMTPanelV2
    {
        private List<TMTDataGridView> _childDataGridViewList;
        private TMTDataGridView _focusedChildDataGridView;
        private bool _ignoreNavigatorSelectedIndexChange = false;
        private bool _isDataGridViewDataClear = false;
        private bool _isHeaderDataSourceTableLoading = false;
        private bool _isChildDataSourceTablesLoading = false;
        private List<string> _keyFieldListDbColumnList;
        private TMTLOVDialog _lovDialog;
        private List<Control> _mandatoryFieldList;

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

            foreach (TMTDataGridView childDataGridView in this._childDataGridViewList)
            {
                childDataGridView.InitializeTableControls();
            }

            tmtButtonAdd.Enabled = this.AddDataAllowed;
            tmtButtonDuplicate.Enabled = this.AddDataAllowed;
            tmtButtonDelete.Enabled = this.DeleteDataAllowed;
        }

        [Category("TMT Data")]
        public event LovLoadedEventHandler LovLoaded;

        [Category("TMT Data")]
        public event LovLoadingEventHandler LovLoading;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataTable DataSourceTable { get; set; }

        [Category("TMT Data")]
        public string TableName { get; set; }

        [Category("TMT Data")]
        public string ViewName { get; set; }

        [Category("TMT Data")]
        public string DefaultWhereStatment { get; set; }

        [Category("TMT Data"), DefaultValue(true)]
        public bool AddDataAllowed { get; set; }

        [Category("TMT Data"), DefaultValue(true)]
        public bool DeleteDataAllowed { get; set; }

        [Category("TMT Data"), DefaultValue(false)]
        public bool LoadDataWhenActive { get; set; }

        public override void AddData()
        {
            if (this._focusedChildDataGridView != null && this._focusedChildDataGridView.AddDataAllowed)
            {
                DataRow newRow = this._focusedChildDataGridView.DataSourceTable.NewRow();
                this._focusedChildDataGridView.DataSourceTable.Rows.Add(newRow);
            }
            else
            {
                DataRow newRow = this.DataSourceTable.NewRow();
                this.DataSourceTable.Rows.Add(newRow);
                tmtNavigator.SelectedIndex = this.DataSourceTable.Rows.IndexOf(newRow);

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

        public void AddHiddenKeyFieldDbColumn(string dbColumnName)
        {
            this._keyFieldListDbColumnList.Add(dbColumnName);
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
            if (this._childDataGridViewList.Count > 0)
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
            }
            base.ColumnManager();
        }

        public List<TMTDataGridView> GetChildDataGridViewList()
        {
            return this._childDataGridViewList;
        }

        public void InitializeTMTControls()
        {
            this._keyFieldListDbColumnList = new List<string>();
            this._mandatoryFieldList = new List<Control>();

            this.SearchDialog.EntityList.Clear();
            TMTSearchDialog.SearchEntity searchEntity;
            TMTDataSourceInformation sourceInfor;
            var tmtControlList = this.panelControlContainer.Controls.Cast<Control>().OrderBy(c => c.TabIndex);
            foreach (Control tmtControl in tmtControlList)
            {
                sourceInfor = tmtControl.GetDataSourceInformation();

                if (string.IsNullOrWhiteSpace(sourceInfor.DbColumnName) == false)
                {
                    if (tmtControl.Visible)
                    {
                        searchEntity = new TMTSearchDialog.SearchEntity();
                        searchEntity.Caption = sourceInfor.DbLabelText;
                        searchEntity.ColumnName = sourceInfor.DbColumnName;
                        searchEntity.DataType = sourceInfor.GetDbColumnType();

                        this.SearchDialog.EntityList.Add(searchEntity);
                    }

                    this.DataSourceTable.Columns.Add(sourceInfor.DbColumnName, sourceInfor.GetDbColumnType());

                    tmtControl.TextChanged += tmtControl_TextChanged;
                    tmtControl.GotFocus += tmtControl_GotFocus;

                    if (sourceInfor.KeyColum)
                    {
                        this._keyFieldListDbColumnList.Add(sourceInfor.DbColumnName);
                    }
                    if (sourceInfor.MandatoryColum)
                    {
                        this._mandatoryFieldList.Add(tmtControl);
                    }
                }
            }

            this.SetTMTControlDataBindings();

            this.SetTMTControlLovEvents();
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

                if (this._childDataGridViewList.Count > 0)
                {
                    foreach (TMTDataGridView childDataGridView in this._childDataGridViewList)
                    {
                        arg.ChildViewList.Add(new PanelBackgroundWorkeArg()
                        {
                            HeaderViewName = childDataGridView.ViewName,
                            HeaderSearchConditionTable = TMTExtendard.GetSearchConditionTable(),
                            DefaultWhereStatment = childDataGridView.DefaultWhereStatment
                        });
                    }

                    this.SetChildTableListSearchConditionTables(primarySelectedRow, arg.ChildViewList);
                }

                backgroundWorkerMain.RunWorkerAsync(arg);
            }
        }

        public virtual void SetChildTableListSearchConditionTables(DataRow primarySelectedRow, List<PanelBackgroundWorkeArg> childViewList)
        {
            object searchValue;
            foreach (string keyFieldDbColumnName in this._keyFieldListDbColumnList)
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

                PanelBackgroundWorkeArg arg = new PanelBackgroundWorkeArg();
                arg.Type = PanelBackgroundWorkeType.Load;
                arg.HeaderSearchConditionTable = this.SearchDialog.GetSearchCondition();
                arg.IsCaseSensitive = this.SearchDialog.IsCaseSensitive;
                arg.HeaderViewName = this.ViewName;
                arg.DefaultWhereStatment = this.DefaultWhereStatment;

                backgroundWorkerMain.RunWorkerAsync(arg);
            }
        }

        public override void OnValidateBeforeSave(TMTUserControl.DataValidatingEventArgs dataToBeSaved)
        {
            TMTDataSourceInformation sourceInfor;
            string connectedLabelText = string.Empty;
            if (dataToBeSaved.DataToBeSaved.Tables.Contains(this.TableName))
            {
                if (this._mandatoryFieldList != null &&
                    this._mandatoryFieldList.Count > 0)
                {
                    DataRowCollection rowCollection = dataToBeSaved.DataToBeSaved.Tables[this.TableName].Rows;
                    foreach (DataRow row in rowCollection)
                    {
                        if (row.RowState != DataRowState.Deleted)
                        {
                            foreach (Control mandatoryField in this._mandatoryFieldList)
                            {
                                sourceInfor = mandatoryField.GetDataSourceInformation();

                                if (mandatoryField is TMTTextBox)
                                {
                                    connectedLabelText = (mandatoryField as TMTTextBox).ConnectedLabel.Text;
                                }
                                if (mandatoryField is TMTTextButtonBox)
                                {
                                    connectedLabelText = (mandatoryField as TMTTextButtonBox).ConnectedLabel.Text;
                                }
                                else if (mandatoryField is TMTCheckBox)
                                {
                                    connectedLabelText = (mandatoryField as TMTCheckBox).Text;
                                }
                                else if (mandatoryField is TMTNumericUpDown)
                                {
                                    connectedLabelText = (mandatoryField as TMTNumericUpDown).ConnectedLabel.Text;
                                }
                                else if (mandatoryField is TMTComboBox)
                                {
                                    connectedLabelText = (mandatoryField as TMTComboBox).ConnectedLabel.Text;
                                }
                                else if (mandatoryField is TMTDateTimePicker)
                                {
                                    connectedLabelText = (mandatoryField as TMTDateTimePicker).ConnectedLabel.Text;
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

            if (this._childDataGridViewList.Count > 0)
            {
                foreach (TMTDataGridView childDataView in this._childDataGridViewList)
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

                PanelBackgroundWorkeArg arg = new PanelBackgroundWorkeArg();
                arg.Type = PanelBackgroundWorkeType.Save;

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

                        if (string.IsNullOrWhiteSpace(sourceInfor.DbColumnName) == false)
                        {
                            columnInView.Add(sourceInfor.DbColumnName);
                            if (sourceInfor.KeyColum == false)
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

                foreach (TMTDataGridView childDataGridView in this._childDataGridViewList)
                {
                    changedData = childDataGridView.GetDataSourceTableChanges();
                    if (changedData != null && changedData.Rows.Count > 0)
                    {
                        arg.ChangedDataSet.Tables.Add(changedData);
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
                    MessageBox.Show(this, Properties.Resources.MSG_SAVE_Null, Properties.Resources.MSG_HEADER_Save, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

                            if (this._childDataGridViewList.Count > 0)
                            {
                                foreach (TMTDataGridView childDataGridView in this._childDataGridViewList)
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
            this._focusedChildDataGridView = sender as TMTDataGridView;
            tmtButtonColumnManager.Enabled = true;
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
            if (this._childDataGridViewList.Count > 0)
            {
                foreach (TMTDataGridView childDataGridView in this._childDataGridViewList)
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

        private void GetLovSelectedRow(LovLoadingEventArgs rowEvent)
        {
            try
            {
                if (LovLoading != null)
                {
                    LovLoading(this, rowEvent);
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.MSG_LOV_LoadError);
            }
        }

        private void SetLovSelectedRow(LovLoadedEventArgs rowEvent)
        {
            try
            {
                if (LovLoaded != null)
                {
                    LovLoaded(this, rowEvent);
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.MSG_LOV_SetError);
            }
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

                if (string.IsNullOrWhiteSpace(sourceInfor.DbColumnName) == false)
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
            this._childDataGridViewList = this.panelControlContainer.GetChildDataGridViewList();
            if (this._childDataGridViewList.Count > 0)
            {
                foreach (TMTDataGridView childDataGridView in this._childDataGridViewList)
                {
                    childDataGridView.CellValueChanged += childDataGridView_CellValueChanged;
                    childDataGridView.RowsRemoved += childDataGridView_RowsRemoved;
                    childDataGridView.RowsAdded += childDataGridView_RowsAdded;
                    childDataGridView.GotFocus += childDataGridView_GotFocus;
                }
            }

            TMTDataSourceInformation sourceInfor;
            foreach (Control tmtControl in this.panelControlContainer.Controls)
            {
                if (tmtControl.Visible)
                {
                    if (tmtControl is NumericUpDown ||
                        tmtControl is TextBox)
                    {
                        sourceInfor = tmtControl.GetDataSourceInformation();

                        if (string.IsNullOrWhiteSpace(sourceInfor.LovViewName) == false)
                        {
                            tmtControl.Validated += tmtControl_Validated;
                        }
                    }
                    else if (tmtControl is TMTButtonLov)
                    {
                        tmtControl.Click += TMTButtonLov_Click;
                    }
                    else if (tmtControl is TMTTextButtonBox)
                    {
                        (tmtControl as TMTTextButtonBox).ButtonClick += TMTTextButtonBox_ButtonClick;
                        sourceInfor = tmtControl.GetDataSourceInformation();

                        if (string.IsNullOrWhiteSpace(sourceInfor.LovViewName) == false)
                        {
                            tmtControl.Validated += tmtControl_Validated;
                        }
                    }
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

        private void TMTButtonLov_Click(object sender, EventArgs e)
        {
            try
            {
                TMTButtonLov tmtButton = sender as TMTButtonLov;
                if (tmtButton.ConnectedBox != null)
                {
                    if (this._lovDialog == null)
                    {
                        this._lovDialog = new TMTLOVDialog();
                    }

                    TMTDataSourceInformation sourceInfor = tmtButton.ConnectedBox.GetDataSourceInformation();

                    LovLoadingEventArgs lovEvent = new LovLoadingEventArgs();
                    lovEvent.IsValidate = false;
                    lovEvent.PrimaryColumnName = sourceInfor.DbColumnName;
                    lovEvent.SearchConditionTable = TMTExtendard.GetSearchConditionTable();
                    lovEvent.LovViewName = sourceInfor.LovViewName;

                    this.GetLovSelectedRow(lovEvent);

                    this._lovDialog.SetDataSourceTable(lovEvent.LovDataTable);

                    if (this._lovDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        LovLoadedEventArgs lovLoaded = new LovLoadedEventArgs();
                        lovLoaded.SelectedRow = this._lovDialog.SelectedRow;
                        lovLoaded.PrimaryColumnName = sourceInfor.DbColumnName;

                        if (tmtButton.ConnectedBox is TMTTextBox)
                        {
                            TMTTextBox textBox = tmtButton.ConnectedBox as TMTTextBox;
                            textBox.Text = lovLoaded.SelectedRow[lovEvent.PrimaryColumnName].ToString();
                            textBox.LovText = textBox.Text;
                        }
                        else if (tmtButton.ConnectedBox is NumericUpDown)
                        {
                            (tmtButton.ConnectedBox as NumericUpDown).Value = Convert.ToDecimal(lovLoaded.SelectedRow[lovEvent.PrimaryColumnName]);
                        }

                        tmtButton.ConnectedBox.CausesValidation = true;
                        tmtButton.ConnectedBox.Focus();

                        this.SetLovSelectedRow(lovLoaded);
                    }
                }
                else
                {
                    MessageBox.Show(this, Properties.Resources.DESIN_ERROR_NoConnectedControl, Properties.Resources.DESIN_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.MSG_LOV_SetError);
            }
        }

        private void TMTTextButtonBox_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                TMTTextButtonBox tmtTextButton = sender as TMTTextButtonBox;

                if (this._lovDialog == null)
                {
                    this._lovDialog = new TMTLOVDialog();
                }

                TMTDataSourceInformation sourceInfor = tmtTextButton.GetDataSourceInformation();

                LovLoadingEventArgs lovEvent = new LovLoadingEventArgs();
                lovEvent.IsValidate = false;
                lovEvent.PrimaryColumnName = sourceInfor.DbColumnName;
                lovEvent.SearchConditionTable = TMTExtendard.GetSearchConditionTable();
                lovEvent.LovViewName = sourceInfor.LovViewName;

                this.GetLovSelectedRow(lovEvent);

                this._lovDialog.SetDataSourceTable(lovEvent.LovDataTable);

                if (this._lovDialog.ShowDialog(this) == DialogResult.OK)
                {
                    LovLoadedEventArgs lovLoaded = new LovLoadedEventArgs();
                    lovLoaded.SelectedRow = this._lovDialog.SelectedRow;
                    lovLoaded.PrimaryColumnName = sourceInfor.DbColumnName;

                    tmtTextButton.Text = lovLoaded.SelectedRow[lovEvent.PrimaryColumnName].ToString();
                    tmtTextButton.LovText = tmtTextButton.Text;

                    tmtTextButton.CausesValidation = true;
                    tmtTextButton.Focus();

                    this.SetLovSelectedRow(lovLoaded);
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.MSG_LOV_SetError);
            }
        }

        private void tmtControl_GotFocus(object sender, EventArgs e)
        {
            this._focusedChildDataGridView = null;
            tmtButtonColumnManager.Enabled = false;
        }

        private void tmtControl_TextChanged(object sender, EventArgs e)
        {
            if (this._isHeaderDataSourceTableLoading == false &&
                this._ignoreNavigatorSelectedIndexChange == false)
            {
                this.OnDataChanged();
            }
        }

        private void tmtControl_Validated(object sender, EventArgs e)
        {
            try
            {
                Control tmtControl = sender as Control;
                TMTDataSourceInformation sourceInfor = tmtControl.GetDataSourceInformation();

                if (string.IsNullOrWhiteSpace(sourceInfor.LovViewName) == false)
                {
                    if (sourceInfor.LovText != tmtControl.Text)
                    {
                        LovLoadingEventArgs lovEvent = new LovLoadingEventArgs();
                        lovEvent.IsValidate = true;
                        lovEvent.PrimaryColumnName = sourceInfor.DbColumnName;
                        lovEvent.SearchConditionTable = TMTExtendard.GetSearchConditionTable();
                        lovEvent.LovViewName = sourceInfor.LovViewName;

                        this.GetLovSelectedRow(lovEvent);

                        bool dataFound = (lovEvent.LovDataTable != null && lovEvent.LovDataTable.Rows.Count == 1);
                        if (dataFound)
                        {
                            LovLoadedEventArgs lovLoaded = new LovLoadedEventArgs();

                            DataRow selectedDataRow = lovEvent.LovDataTable.Rows[0];
                            Dictionary<string, Object> selectedRow = new Dictionary<string, object>();
                            foreach (DataColumn col in lovEvent.LovDataTable.Columns)
                            {
                                selectedRow.Add(col.ColumnName, selectedDataRow[col]);
                            }
                            lovLoaded.SelectedRow = selectedRow;
                            lovLoaded.PrimaryColumnName = lovEvent.PrimaryColumnName;

                            this.SetLovSelectedRow(lovLoaded);
                        }

                        this.BackColor = (dataFound) ? Color.Empty : Color.Red;
                    }
                    else
                    {
                        this.BackColor = Color.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.MSG_LOV_SetError);
            }
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
    }
}
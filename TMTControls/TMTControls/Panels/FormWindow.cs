using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMT.Controls.WinForms.DatabaseUI;
using TMT.Controls.WinForms.DataGrid;
using TMT.Controls.WinForms.Dialogs;

namespace TMT.Controls.WinForms.Panels
{
    public partial class FormWindow : TMT.Controls.WinForms.Panels.BaseWindow
    {
        private DbDataGridView _focusedChildDataGridView;
        private bool _isDataSourceTableLoading = false;
        private int _recordSelector_previousSelectedIndex = -1;

        public FormWindow()
        {
            InitializeComponent();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataTable DataSourceTable
        {
            get => bindingSourceForm.DataSource as DataTable;
            set
            {
                this.bindingSourceForm.SuspendBinding();
                this.bindingSourceForm.DataSource = value;
                //recordSelector.DataSourceTable = value;
                this.bindingSourceForm.ResumeBinding();
            }
        }

        [Category("Data")]
        public string DefaultOrderByStatement { get; set; }

        [Category("Data")]
        public string DefaultWhereStatement { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool DonotReloadAfterSave { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsChildDataGridViewFocused => this._focusedChildDataGridView != null;

        [Category("Data"), DefaultValue(false)]
        public override bool IsDeleteAllowed
        {
            get => (_focusedChildDataGridView != null && _focusedChildDataGridView.ContainsFocus) ?
                    _focusedChildDataGridView.IsDeleteAllowed : base.IsDeleteAllowed;
            set => base.IsDeleteAllowed = value;
        }

        [Category("Data"), DefaultValue(false)]
        public bool LoadDataWhenActive { get; set; }

        [Category("Data")]
        public string TableName { get; set; }

        [Category("Data")]
        public string ViewName { get; set; }

        internal async override Task LoadIfActive()
        {
            try
            {
                if (this.LoadDataWhenActive == false)
                {
                    return;
                }
                this.SearchDialog.DialogResult = DialogResult.Ignore;
                await this.DataSearch();
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
            }
        }

        protected internal override void DataClear()
        {
            panelForm.Enabled = false;
            this.DataSourceTable.Clear();
            this._recordSelector_previousSelectedIndex = recordSelector.SelectedIndex = -1;

            this.ClearControlValues();

            this.ClearChildDataGridViews(false);

            labelRecordCount.Text = $"Record Count is {this.DataSourceTable.Rows.Count}";
        }

        protected internal override void DataDelete()
        {
            if (this._focusedChildDataGridView != null &&
                this._focusedChildDataGridView.IsDeleteAllowed)
            {
                if (this._focusedChildDataGridView.DeleteSelectedRows())
                {
                    this.DataChanged();
                }
            }
            else
            {
                //if (recordSelector.SelectedValue == null ||
                //    string.IsNullOrWhiteSpace(recordSelector.SelectedValue.ToString()))
                //{
                //    return;
                //}
                var foundIndex = bindingSourceForm.Find(recordSelector.ValueMember, recordSelector.SelectedValue);
                if (foundIndex <= -1)
                {
                    return;
                }
                if (MessageDialog.ShowQuestion(this,
                                    string.Format(CultureInfo.InvariantCulture, Properties.Resources.QUESTION_DeleteConfirm, recordSelector.SelectedValue),
                                    Properties.Resources.QUESTION_Delete) == DialogResult.Yes)
                {
                    this.DataSourceTable.Rows[foundIndex].Delete();

                    this.DataChanged();
                }
            }
        }

        protected internal override void DataDuplicate()
        {
            if (this._focusedChildDataGridView != null)
            {
                if (this._focusedChildDataGridView.DuplicateSelectedRows())
                {
                    this.DataChanged();
                }
            }
            else
            {
                if (recordSelector.SelectedValue == null ||
                    string.IsNullOrWhiteSpace(recordSelector.SelectedValue.ToString()))
                {
                    return;
                }
                var foundIndex = bindingSourceForm.Find(recordSelector.ValueMember, recordSelector.SelectedValue);
                if (foundIndex <= -1)
                {
                    return;
                }
                this.DataSourceTable.Columns.Cast<DataColumn>().ToList().ForEach(c => c.AllowDBNull = true);
                this.DataSourceTable.Rows.Add(this.DataSourceTable.Rows[foundIndex].ItemArray);

                this.DataChanged();
            }
        }

        protected internal override async Task<bool> DataSave()
        {
            try
            {
                if (this.DonotReloadAfterSave == false)
                {
                    recordSelector.PreviousSelectedValue = recordSelector.SelectedValue;
                }
                var arg = new DataSaveEventArgs();

                this.Validate();
                this.bindingSourceForm.EndEdit();

                this.GetFormChangedDataTable(arg);
                this.GetChildTableChangedDataTable(arg);

                if (arg.ChangedDataSet.Tables.Count > 0)
                {
                    if (this.DataValidateBeforeSave(arg.ChangedDataSet) == false)
                    {
                        var saveResults = await Task.Run(async () =>
                        {
                            return await this.SaveDataToDatabase(arg);
                        });

                        if (saveResults.Any(i => i > 0))
                        {
                            if ((recordSelector.PreviousSelectedValue == null || string.IsNullOrWhiteSpace(recordSelector.PreviousSelectedValue.ToString())) &&
                               arg.GeneratedKey != null &&
                                string.IsNullOrWhiteSpace(arg.GeneratedKey.ToString()) == false)
                            {
                                recordSelector.PreviousSelectedValue = arg.GeneratedKey;
                            }
                            if (this.DonotReloadAfterSave == false)
                            {
                                await this.DataSearch();
                            }
                            else
                            {
                                DataSourceTable.AcceptChanges();

                                var childDataGridViewList = this.panelForm.GetChildDataGridViewList();
                                if (childDataGridViewList.Count > 0)
                                {
                                    foreach (var childDataGridView in childDataGridViewList)
                                    {
                                        childDataGridView.DataSourceTable.AcceptChanges();
                                    }
                                }
                            }
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
            try
            {
                this._isDataSourceTableLoading = true;

                if (recordSelector.DataSourceTable != null)
                {
                    recordSelector.DataSourceTable.Clear();
                }
                this.ClearChildDataGridViews(true);

                var controlList = this.panelForm.Controls.Cast<Control>().OrderBy(c => c.TabIndex);
                var viewColumnDbNameList = controlList.Where(c => c is IDbControl)
                                  .Cast<IDbControl>()
                                  .Where(c => !string.IsNullOrWhiteSpace(c.DbColumnName))
                                  .Select(c => c.DbColumnName);

                var arg = new DataLoadArg
                {
                    IsCaseSensitive = this.SearchDialog.IsCaseSensitive,
                    LimitLoad = this.SearchDialog.LimitLoad,
                    ViewName = this.ViewName,
                    DefaultWhereStatement = this.DefaultWhereStatement,
                    DefaultOrderByStatement = this.DefaultOrderByStatement,
                    //LoadSchema = this.LoadSchema
                };
                arg.SearchConditionList.AddRange(this.SearchDialog.EntityList);
                arg.ColumnDbNameList.AddRange(viewColumnDbNameList);

                var args = new DataLoadEventArgs();
                args.DataLoadArgList.Add(arg);

                await Task.Run(async () => await this.DataPopulateAllRecords(args));

                this.DataSourceTable = args.ChangedDataSet.Tables[arg.ViewName];
                recordSelector.DataSourceTable = this.DataSourceTable;

                if (recordSelector.PreviousSelectedValue != null)
                {
                    var foundIndex = bindingSourceForm.Find(recordSelector.ValueMember, recordSelector.PreviousSelectedValue);
                    recordSelector.SelectedValue = (foundIndex > -1) ? recordSelector.PreviousSelectedValue : 0;
                    this._recordSelector_previousSelectedIndex = recordSelector.SelectedIndex;
                    recordSelector.PreviousSelectedValue = null;
                }

                this.SetNewButtonEnabled(this.IsNewAllowed);
                this.SetDeleteButtonEnabled(this.IsDeleteAllowed);

                if (this.DataSourceTable == null ||
                    this.DataSourceTable.Rows.Count == 0)
                {
                    MessageDialog.Show(this, Properties.Resources.Exclamation_NoDataFoundText,
                                          Properties.Resources.Exclamation_NoDataFound,
                                          MessageDialogIcon.Asterisk);
                }

                labelRecordCount.Text = $"Record Count is {this.DataSourceTable.Rows.Count}";
                panelForm.Enabled = true;

                await this.LoadAllChildGridViewsData();

                return true;
            }
            finally
            {
                this._isDataSourceTableLoading = false;
            }
        }

        protected override void ChangeSelectedDataGridViewColumnVisibility()
        {
            if (this._focusedChildDataGridView != null)
            {
                var colList = this._focusedChildDataGridView.Columns.Cast<DataGridViewColumn>()
                                                      .Where(c => (c is DataGridViewButtonColumn) == false)
                                                      .Select(c => new ColumnData(c.HeaderText, c.Visible)).ToList();

                this.ColumnManagerDialog.SetColumnList(colList);
                if (this.ColumnManagerDialog.ShowDialog(this) == DialogResult.OK)
                {
                    var colDictionary = this.ColumnManagerDialog.GetColumnList().ToDictionary(c => c.Name);

                    this._focusedChildDataGridView.Columns.Cast<DataGridViewColumn>()
                                                          .Where(c => colDictionary.ContainsKey(c.HeaderText))
                                                          .ToList()
                                                          .ForEach(c => c.Visible = colDictionary[c.HeaderText].Visibility);
                }
            }
        }

        protected override void DataNew()
        {
            if (this._focusedChildDataGridView != null)
            {
                // Check if Head has record
                if (this.DataSourceTable != null &&
                    this.DataSourceTable.Rows.Count > 0)
                {
                    this._focusedChildDataGridView.AddNewRow();
                    this.DataChanged();
                }
            }
            else
            {
                this.DataSourceTable.Columns.Cast<DataColumn>().ToList().ForEach(c => c.AllowDBNull = true);

                var newRow = this.DataSourceTable.NewRow();
                this.DataSourceTable.Rows.Add(newRow);

                // this is required, so that UI Heared UI change when recordSelector value change.
                bindingSourceForm.Position = this.DataSourceTable.Rows.IndexOf(newRow);
                if (recordSelector.DataSourceTable == null)
                {
                    recordSelector.DataSourceTable = this.DataSourceTable;
                }
                this._recordSelector_previousSelectedIndex = recordSelector.SelectedIndex = bindingSourceForm.Position;

                this.ClearControlValues();
                this.ClearChildDataGridViews(true);
                this.DataChanged();
            }
            panelForm.Enabled = true;
        }

        protected override bool DataValidateBeforeSave(DataSet dataToBeSaved)
        {
            if (dataToBeSaved == null)
            {
                throw new ArgumentNullException(nameof(dataToBeSaved));
            }

            if (dataToBeSaved.Tables.Contains(this.TableName))
            {
                var mandatoryFieldList = this.GetMandatoryControlList();
                if (mandatoryFieldList.Count > 0)
                {
                    var rowCollection = dataToBeSaved.Tables[this.TableName].Rows
                                                                            .Cast<DataRow>()
                                                                            .Where(r => r.RowState != DataRowState.Deleted);
                    foreach (var row in rowCollection)
                    {
                        foreach (var mandatoryField in mandatoryFieldList)
                        {
                            var cancelSave = this.ShowEmptyExclamation(mandatoryField,
                                                                       row[mandatoryField.DbColumnName]);
                            if (cancelSave)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            var childDataGridViewList = this.panelForm.GetChildDataGridViewList();
            if (childDataGridViewList.Count <= 0)
            {
                return false;
            }
            var childDataGridViewSaveList = childDataGridViewList.Where(t => dataToBeSaved.Tables.Contains(t.TableName));
            foreach (var childDataView in childDataGridViewSaveList)
            {
                var cancelSave = childDataView.DataValidateBeforeSave(dataToBeSaved);
                if (cancelSave)
                {
                    return true;
                }
            }
            return false;
        }

        protected DataRow GetDataSourceSelectedRow()
        {
            if (recordSelector.SelectedValue == null ||
                string.IsNullOrWhiteSpace(recordSelector.SelectedValue.ToString()))
            {
                return null;
            }
            var foundIndex = bindingSourceForm.Find(recordSelector.ValueMember, recordSelector.SelectedValue);
            if (foundIndex <= -1 || this.DataSourceTable.Rows.Count <= foundIndex)
            {
                return null;
            }
            // this is required, so that UI Heared UI change when recordSelector value change.
            bindingSourceForm.Position = foundIndex;
            var primarySelectedRow = this.DataSourceTable.Rows[foundIndex];
            return primarySelectedRow.RowState != DataRowState.Deleted ? primarySelectedRow : null;
        }

        protected async Task LoadAllChildGridViewsData()
        {
            var primarySelectedRow = this.GetDataSourceSelectedRow();
            if (primarySelectedRow != null)
            {
                await this.LoadAllChildGridViewsData(primarySelectedRow);
            }
            else
            {
                this.ClearChildDataGridViews(false);
            }
        }

        protected async Task LoadAllChildGridViewsData(DataRow headerSelectedRow)
        {
            var childDataGridViewList = this.panelForm.GetChildDataGridViewList();
            if (childDataGridViewList.Count <= 0)
            {
                return;
            }

            var args = new DataLoadEventArgs();
            foreach (var childDataGridView in childDataGridViewList)
            {
                var arg = new DataLoadArg
                {
                    IsCaseSensitive = this.SearchDialog.IsCaseSensitive,
                    LimitLoad = this.SearchDialog.LimitLoad,
                    ViewName = childDataGridView.ViewName,
                    DefaultWhereStatement = childDataGridView.DefaultWhereStatement,
                    DefaultOrderByStatement = childDataGridView.DefaultOrderByStatement,
                    LoadSchema = childDataGridView.LoadSchema
                };
                var viewColumnsDictionary = childDataGridView.GetViewColumnDbNameDictionary();

                arg.ColumnDbNameList.AddRange(viewColumnsDictionary.Keys.ToList());

                foreach (var searchCondition in arg.SearchConditionList)
                {
                    if (viewColumnsDictionary.ContainsKey(searchCondition.ColumnName))
                    {
                        searchCondition.IsFuntion = viewColumnsDictionary[searchCondition.ColumnName];
                    }
                }

                args.DataLoadArgList.Add(arg);
            }
            this.SetChildTableListSearchConditionTables(headerSelectedRow, args);

            await Task.Run(async () => await this.DataPopulateAllRecords(args));

            foreach (var childDataGridView in childDataGridViewList)
            {
                childDataGridView.DataSourceTable = args.ChangedDataSet.Tables[childDataGridView.ViewName];
                childDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
        }

        protected async override Task SearchDialogListOfValuesLoading(ListOfValueLoadingEventArgs e)
        {
            e.FillSearchConditionTable();
            await base.SearchDialogListOfValuesLoading(e);
        }

        protected virtual void SetChildTableListSearchConditionTables(DataRow headerSelectedRow, DataLoadEventArgs args)
        {
            if (headerSelectedRow == null)
            {
                throw new ArgumentNullException(nameof(headerSelectedRow));
            }

            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            var controlList = this.panelForm.Controls.Cast<Control>().OrderBy(c => c.TabIndex);
            var keyFieldListDbColumnList = controlList.Where(c => c is IDbControl)
                                            .Cast<IDbControl>()
                                            .Where(c => string.IsNullOrWhiteSpace(c.DbColumnName) == false && c.KeyColumn)
                                            .Select(c => c.DbColumnName)
                                            .Where(c => headerSelectedRow[c] != null &&
                                                        headerSelectedRow[c] != DBNull.Value);

            foreach (var keyFieldDbColumnName in keyFieldListDbColumnList)
            {
                var searchValue = headerSelectedRow[keyFieldDbColumnName].ToString();
                foreach (var arg in args.DataLoadArgList)
                {
                    arg.SearchConditionList.Add(new SearchEntity
                    {
                        ColumnName = keyFieldDbColumnName,
                        Value = searchValue,
                        DataType = searchValue.GetType()
                    });
                }
            }
        }

        private void ChildDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                this.DataChanged();
            }
        }

        private void ChildDataGridView_GotFocus(object sender, EventArgs e)
        {
            if (!(sender is DbDataGridView gridView && gridView.Visible))
            {
                return;
            }
            this._focusedChildDataGridView = gridView;
            this._focusedChildDataGridView.BorderStyle = BorderStyle.FixedSingle;
            this.SetChangeColumnButtonEnabled(true);
            if (this.DataSourceTable.Rows.Count > 0)
            {
                this.SetNewButtonEnabled(this._focusedChildDataGridView.IsNewAllowed && this.WindowRecordState != WindowRecordState.New);
                if (this._focusedChildDataGridView.DataSourceTable.Rows.Count <= 0)
                {
                    return;
                }
                this.SetDeleteButtonEnabled(this._focusedChildDataGridView.IsDeleteAllowed);
            }
            else
            {
                this.SetNewButtonEnabled(false);
                this.SetDeleteButtonEnabled(false);
            }
        }

        private void ChildDataGridView_VisibleChanged(object sender, EventArgs e)
        {
            if (!(sender is DbDataGridView gridView && gridView.Visible))
            {
                return;
            }
            this._focusedChildDataGridView = gridView;
            this.SetChangeColumnButtonEnabled(true);
            if (this.DataSourceTable.Rows.Count <= 0)
            {
                return;
            }
            this.SetNewButtonEnabled(this._focusedChildDataGridView.IsNewAllowed && this.WindowRecordState != WindowRecordState.New);
            if (this._focusedChildDataGridView.DataSourceTable.Rows.Count <= 0)
            {
                return;
            }
            this.SetDeleteButtonEnabled(this._focusedChildDataGridView.IsDeleteAllowed);
        }

        private void ClearChildDataGridViews(bool enableDataGrids)
        {
            var childDataGridViewList = this.panelForm.GetChildDataGridViewList();
            if (childDataGridViewList.Count <= 0)
            {
                return;
            }
            foreach (var childDataGridView in childDataGridViewList)
            {
                if (childDataGridView.DataSourceTable != null)
                {
                    childDataGridView.DataSourceTable.Clear();
                }
                childDataGridView.Enabled = enableDataGrids;
            }
        }

        private void ClearControlValues()
        {
            foreach (var uiControl in this.panelForm.Controls)
            {
                switch (uiControl)
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

                    case DbTextButtonBox textButtonBox:
                        textButtonBox.Text = string.Empty;
                        break;
                }
            }
        }

        private void FormWindow_Load(object sender, EventArgs e)
        {
            try
            {
                this.InitializeControls();

                this.SetNewButtonEnabled(this.IsNewAllowed);
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
            }
        }

        private void GetChildTableChangedDataTable(DataSaveEventArgs arg)
        {
            var childDataGridViewList = this.panelForm.GetChildDataGridViewList();
            if (childDataGridViewList.Count <= 0)
            {
                return;
            }
            foreach (var childDataGridView in childDataGridViewList)
            {
                var changedChildData = childDataGridView.GetDataSourceTableChanges();
                if (changedChildData != null &&
                    changedChildData.Rows.Count > 0)
                {
                    arg.ChangedDataSet.Tables.Add(changedChildData);
                }
            }
        }

        private void GetFormChangedDataTable(DataSaveEventArgs arg)
        {
            var changedData = this.DataSourceTable.GetDataSourceTableChanges(this.TableName);
            if (changedData == null || changedData.Rows.Count <= 0)
            {
                return;
            }
            var myControls = this.panelForm.Controls.Cast<Control>()
                                                    .Where(c => c is IDbControl myUiControl &&
                                                                string.IsNullOrWhiteSpace(myUiControl.DbColumnName) == false)
                                                    .Cast<IDbControl>();

            var columnInView = new HashSet<string>(myControls.Select(c => c.DbColumnName));
            var columnNameList = changedData.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
            foreach (var colName in columnNameList)
            {
                if (columnInView.Contains(colName) == false)
                {
                    changedData.Columns.Remove(colName);
                }
            }

            var readOnlyColumnsToRemove = new List<string>();
            foreach (var uiControl in myControls)
            {
                if (uiControl.KeyColumn == false && uiControl.MandatoryColumn == false)
                {
                    if ((uiControl is NumericUpDown myNumericUpDown && myNumericUpDown.ReadOnly) ||
                        (uiControl is TextBox myTextBox && myTextBox.ReadOnly))
                    {
                        readOnlyColumnsToRemove.Add(uiControl.DbColumnName);
                    }
                }
            }
            foreach (var readOnlyColumnName in readOnlyColumnsToRemove)
            {
                changedData.Columns.Remove(readOnlyColumnName);
            }
            arg.ChangedDataSet.Tables.Add(changedData);
        }

        private IList<IDbControl> GetMandatoryControlList()
        {
            var mandatoryFieldList = new List<IDbControl>();
            var controlList = this.panelForm.Controls.Cast<Control>()
                                                    .Where(c => c is IDbControl uiControl && uiControl.MandatoryColumn)
                                                    .OrderBy(c => c.TabIndex)
                                                    .Cast<IDbControl>();
            foreach (var uiControl in controlList)
            {
                if (uiControl is NumericUpDown myNumericUpDown)
                {
                    if (myNumericUpDown.ReadOnly == false)
                    {
                        mandatoryFieldList.Add(uiControl);
                    }
                }
                else if (uiControl is TextBox myTextBox)
                {
                    if (myTextBox.ReadOnly == false)
                    {
                        mandatoryFieldList.Add(uiControl);
                    }
                }
                else
                {
                    mandatoryFieldList.Add(uiControl);
                }
            }
            return mandatoryFieldList;
        }

        private void InitializeChildGridViewsControls()
        {
            var childDataGridViewList = this.panelForm.GetChildDataGridViewList();
            if (childDataGridViewList.Count <= 0)
            {
                return;
            }
            foreach (var childDataGridView in childDataGridViewList)
            {
                childDataGridView.CellValueChanged += ChildDataGridView_CellValueChanged;
                childDataGridView.GotFocus += ChildDataGridView_GotFocus;
                childDataGridView.VisibleChanged += ChildDataGridView_VisibleChanged;

                childDataGridView.InitializeTableControls();
            }
        }

        private void InitializeControls()
        {
            var dataTable = new DataTable(this.ViewName)
            {
                Locale = CultureInfo.InvariantCulture
            };

            var dbControlList = this.panelForm.Controls.OfType<Control>()
                                                        .Where(c => c is IDbControl)
                                                        .OrderBy(c => c.TabIndex);
            foreach (var uiControl in dbControlList)
            {
                var sourceInfor = uiControl as IDbControl;

                if (string.IsNullOrWhiteSpace(sourceInfor.DbColumnName))
                {
                    continue;
                }
                dataTable.Columns.Add(sourceInfor.DbColumnName, sourceInfor.GetDbColumnSystemType());

                var myCheckBox = uiControl as DbCheckBox;

                if (myCheckBox != null)
                {
                    myCheckBox.CheckedChanged += UiControl_CheckedChanged;
                }
                else
                {
                    //uiControl.TextChanged += UiControl_TextChanged;
                    uiControl.Validated += UiControl_TextChanged;
                }
                uiControl.GotFocus += UiControl_GotFocus;

                if (uiControl.Visible == false)
                {
                    continue;
                }
                var searchEntity = new SearchEntity()
                {
                    Caption = sourceInfor.GetLableText(),
                    ColumnName = sourceInfor.DbColumnName,
                    DataType = sourceInfor.GetDbColumnSystemType()
                };
                if (uiControl is DbTextButtonBox myTextButtonBox)
                {
                    searchEntity.ListOfValueView = myTextButtonBox.ListOfValueViewName;
                }

                if (myCheckBox != null)
                {
                    searchEntity.IsCheckBox = true;
                    searchEntity.FalseValue = myCheckBox.FalseValue;
                    searchEntity.TrueValue = myCheckBox.TrueValue;
                    searchEntity.IndeterminateValue = myCheckBox.IndeterminateValue;
                }

                this.SearchDialog.EntityList.Add(searchEntity);
            }

            this.DataSourceTable = dataTable;

            this.SetControlDataBindings(dbControlList);

            this.InitializeChildGridViewsControls();
        }

        private async void RecordSelector_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowRecordState == WindowRecordState.New)
                {
                    recordSelector.SelectedIndex = this._recordSelector_previousSelectedIndex;
                    MessageDialog.Show(this, "Please Save the New record first",
                                          "Record selection not allowed",
                                          MessageDialogIcon.Asterisk);
                    return;
                }

                this.UseWaitCursor = true;
                progressBarBase.Visible = true;

                this._recordSelector_previousSelectedIndex = recordSelector.SelectedIndex;

                await this.LoadAllChildGridViewsData();
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_ReLoadingData);
            }
            finally
            {
                this.UseWaitCursor = false;
                progressBarBase.Visible = false;
            }
        }

        private void SetControlDataBindings(IEnumerable<Control> uiControlList)
        {
            var controls = uiControlList.Where(c => c is IDbControl dbControl && string.IsNullOrWhiteSpace(dbControl.DbColumnName) == false);

            string propetyName;
            foreach (var dbControl in controls)
            {
                var dbColumnName = (dbControl as IDbControl).DbColumnName;

                switch (dbControl)
                {
                    case NumericUpDown _:
                    case DateTimePicker _:
                        propetyName = "Value";
                        break;

                    case DbCheckBox dbCheckBox:
                        propetyName = nameof(dbCheckBox.DbValue);
                        break;

                    case ComboBox comboBox:
                        propetyName = nameof(comboBox.SelectedValue);
                        break;

                    default:
                        propetyName = "Text";
                        break;
                }
                dbControl.DataBindings.Add(propetyName, this.bindingSourceForm, dbColumnName,
                                            true, DataSourceUpdateMode.OnValidation);
            }
        }

        private bool ShowEmptyExclamation(IDbControl control, object oValue)
        {
            var cancelSave = false;
            if (oValue == null || string.IsNullOrWhiteSpace(oValue.ToString()))
            {
                var message = string.Format(CultureInfo.InvariantCulture, Properties.Resources.Exclamation_MandatoryValueEmpty,
                                              control.GetLableText());

                errorProviderForm.SetError(control as Control, message);

                MessageDialog.Show(this,
                    message,
                    Properties.Resources.Exclamation_MandatoryEmpty,
                    MessageDialogIcon.Exclamation);
                cancelSave = true;
            }
            else
            {
                errorProviderForm.SetError(control as Control, string.Empty);
            }
            return cancelSave;
        }

        private void UiControl_CheckedChanged(object sender, EventArgs e)
        {
            this.UiControlValueChanged(sender);
        }

        private void UiControl_GotFocus(object sender, EventArgs e)
        {
            if (this._focusedChildDataGridView != null)
            {
                this._focusedChildDataGridView.BorderStyle = BorderStyle.None;
            }

            this._focusedChildDataGridView = null;
            this.SetChangeColumnButtonEnabled(false);
            this.SetNewButtonEnabled(this.IsNewAllowed && this.WindowRecordState != WindowRecordState.New);
            if (this.DataSourceTable.Rows.Count > 0)
            {
                this.SetDeleteButtonEnabled(this.IsDeleteAllowed);
            }
        }

        private void UiControl_TextChanged(object sender, EventArgs e)
        {
            this.UiControlValueChanged(sender);
        }

        private void UiControlValueChanged(object sender)
        {
            if (this._isDataSourceTableLoading)
            {
                return;
            }
            if (!(this.bindingSourceForm.Current is DataRowView dataRowView))
            {
                return;
            }
            if (!(sender is IDbControl dbControl))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(dbControl.DbColumnName))
            {
                return;
            }
            
            object senderValue = null;
            switch (sender)
            {
                case NumericUpDown senderNumericUpDown:
                    senderValue = senderNumericUpDown.Value;
                    break;

                case DateTimePicker senderDateTimePicker:
                    senderValue = senderDateTimePicker.Value;
                    break;

                case DbCheckBox senderCheckBox:
                    senderValue = senderCheckBox.DbValue;
                    break;

                case ComboBox senderComboBox:
                    senderValue = senderComboBox.SelectedValue;
                    break;

                default:
                    senderValue = (sender as Control).Text;
                    break;
            }
           
            var orginalValue = dataRowView.Row[dbControl.DbColumnName, DataRowVersion.Current];
            if ((senderValue == null && orginalValue != null) ||
                (senderValue != null && orginalValue == null) ||
                (senderValue.ToString() != orginalValue.ToString()))
            {
                this.DataChanged();
            }
        }
    }
}
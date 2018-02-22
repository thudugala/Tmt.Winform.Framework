using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMTControls.TMTDatabaseUI;
using TMTControls.TMTDataGrid;
using TMTControls.TMTDialogs;

namespace TMTControls.TMTPanels
{
    public partial class FormWindow : TMTControls.TMTPanels.BaseWindow
    {
        private TMTDataGridView _focusedChildDataGridView;
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
            get
            {
                return this.bindingSourceForm.DataSource as DataTable;
            }
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
            get
            {
                return (this._focusedChildDataGridView != null && this._focusedChildDataGridView.ContainsFocus) ? this._focusedChildDataGridView.IsDeleteAllowed : base.IsDeleteAllowed;
            }
            set => base.IsDeleteAllowed = value;
        }

        [Category("Data"), DefaultValue(false)]
        public bool LoadDataWhenActive { get; set; }

        [Category("Data")]
        public string TableName { get; set; }

        [Category("Data")]
        public string ViewName { get; set; }

        internal async override void LoadIfActive()
        {
            try
            {
                if (this.LoadDataWhenActive)
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
                int foundIndex = bindingSourceForm.Find(recordSelector.ValueMember, recordSelector.SelectedValue);
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
                int foundIndex = bindingSourceForm.Find(recordSelector.ValueMember, recordSelector.SelectedValue);
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
                var arg = new DataSaveArg();

                this.Validate();
                this.bindingSourceForm.EndEdit();

                this.GetFormChangedDataTable(arg);
                this.GetChildTableChangedDataTable(arg);

                if (arg.ChangedDataSet.Tables.Count > 0)
                {
                    if (this.DataValidateBeforeSave(arg.ChangedDataSet) == false)
                    {
                        await Task.Run(async () => await this.SaveDataToDatabase(arg));

                        if (arg.SaveResults.Any(i => i > 0))
                        {
                            if ((recordSelector.PreviousSelectedValue == null || string.IsNullOrWhiteSpace(recordSelector.PreviousSelectedValue.ToString())) &&
                               (arg.GeneratedKey != null &&
                                string.IsNullOrWhiteSpace(arg.GeneratedKey.ToString()) == false))
                            {
                                recordSelector.PreviousSelectedValue = arg.GeneratedKey;
                            }
                            if (this.DonotReloadAfterSave == false)
                            {
                                await this.DataSearch();
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
                var viewColumnDbNameList = controlList.Where(c => c is ITMTDatabaseUIControl)
                                  .Cast<ITMTDatabaseUIControl>()
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

                var args = new DataLoadArgs();
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
                    foreach (DataRow row in rowCollection)
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
            int foundIndex = bindingSourceForm.Find(recordSelector.ValueMember, recordSelector.SelectedValue);
            if (foundIndex <= -1 || this.DataSourceTable.Rows.Count <= foundIndex)
            {
                return null;
            }
            // this is required, so that UI Heared UI change when recordSelector value change.
            bindingSourceForm.Position = foundIndex;
            var primarySelectedRow = this.DataSourceTable.Rows[foundIndex];
            if (primarySelectedRow.RowState != DataRowState.Deleted)
            {
                return primarySelectedRow;
            }
            return null;
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

            var args = new DataLoadArgs();
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
            this.FillSearchConditionTable(e);
            await base.SearchDialogListOfValuesLoading(e);
        }

        protected virtual void SetChildTableListSearchConditionTables(DataRow headerSelectedRow, DataLoadArgs args)
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
            var keyFieldListDbColumnList = controlList.Where(c => c is ITMTDatabaseUIControl)
                                            .Cast<ITMTDatabaseUIControl>()
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
            if (sender is TMTDataGridView gridView && gridView.Visible)
            {
                this._focusedChildDataGridView = gridView;
                this._focusedChildDataGridView.BorderStyle = BorderStyle.FixedSingle;
                this.SetChangeColumnButtonEnabled(true);
                if (this.DataSourceTable.Rows.Count > 0)
                {
                    this.SetNewButtonEnabled(this._focusedChildDataGridView.IsNewAllowed && this.WindowRecordState != WindowRecordState.New);
                    if (this._focusedChildDataGridView.DataSourceTable.Rows.Count > 0)
                    {
                        this.SetDeleteButtonEnabled(this._focusedChildDataGridView.IsDeleteAllowed);
                    }
                }
                else
                {
                    this.SetNewButtonEnabled(false);
                    this.SetDeleteButtonEnabled(false);
                }
            }
        }

        private void ChildDataGridView_VisibleChanged(object sender, EventArgs e)
        {
            if (sender is TMTDataGridView gridView && gridView.Visible)
            {
                this._focusedChildDataGridView = gridView;
                this.SetChangeColumnButtonEnabled(true);
                if (this.DataSourceTable.Rows.Count > 0)
                {
                    this.SetNewButtonEnabled(this._focusedChildDataGridView.IsNewAllowed && this.WindowRecordState != WindowRecordState.New);
                    if (this._focusedChildDataGridView.DataSourceTable.Rows.Count > 0)
                    {
                        this.SetDeleteButtonEnabled(this._focusedChildDataGridView.IsDeleteAllowed);
                    }
                }
            }
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
            foreach (var tmtControl in this.panelForm.Controls)
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

        private void FormWindow_Load(object sender, EventArgs e)
        {
            try
            {
                this.InitializeControls();

                this.SetNewButtonEnabled(this.IsNewAllowed);
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
            }
        }

        private void GetChildTableChangedDataTable(DataSaveArg arg)
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

        private void GetFormChangedDataTable(DataSaveArg arg)
        {
            var changedData = this.DataSourceTable.GetDataSourceTableChanges(this.TableName);
            if (changedData == null || changedData.Rows.Count <= 0)
            {
                return;
            }
            var myControls = this.panelForm.Controls.Cast<Control>()
                                                    .Where(c => c is ITMTDatabaseUIControl myUiControl &&
                                                                string.IsNullOrWhiteSpace(myUiControl.DbColumnName) == false)
                                                    .Cast<ITMTDatabaseUIControl>();

            var columnInView = new HashSet<string>(myControls.Select(c => c.DbColumnName));
            var columnNameList = changedData.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
            foreach (string colName in columnNameList)
            {
                if (columnInView.Contains(colName) == false)
                {
                    changedData.Columns.Remove(colName);
                }
            }

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
            foreach (string readOnlyColumnName in readOnlyColumnsToRemove)
            {
                changedData.Columns.Remove(readOnlyColumnName);
            }
            arg.ChangedDataSet.Tables.Add(changedData);
        }

        private IList<ITMTDatabaseUIControl> GetMandatoryControlList()
        {
            var mandatoryFieldList = new List<ITMTDatabaseUIControl>();
            var controlList = this.panelForm.Controls.Cast<Control>()
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

            var tmtControlList = this.panelForm.Controls.Cast<Control>()
                                                        .Where(c => c is ITMTDatabaseUIControl)
                                                        .OrderBy(c => c.TabIndex);
            foreach (var tmtControl in tmtControlList)
            {
                var sourceInfor = tmtControl as ITMTDatabaseUIControl;

                if (string.IsNullOrWhiteSpace(sourceInfor.DbColumnName) == false)
                {
                    var myTmtCheckBox = tmtControl as TMTCheckBox;

                    if (tmtControl.Visible)
                    {
                        var searchEntity = new SearchEntity()
                        {
                            Caption = sourceInfor.GetLableText(),
                            ColumnName = sourceInfor.DbColumnName,
                            DataType = sourceInfor.GetDbColumnSystemType()
                        };
                        if (tmtControl is TMTTextButtonBox myTMTTextButtonBox)
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

                    dataTable.Columns.Add(sourceInfor.DbColumnName, sourceInfor.GetDbColumnSystemType());

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

            this.DataSourceTable = dataTable;

            this.SetControlDataBindings(tmtControlList);

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
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_ReLoadingData);
            }
            finally
            {
                this.UseWaitCursor = false;
                progressBarBase.Visible = false;
            }
        }

        private void SetControlDataBindings(IEnumerable<Control> tmtControlList)
        {
            string propetyName;
            foreach (var tmtControl in tmtControlList)
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
                    tmtControl.DataBindings.Add(propetyName, this.bindingSourceForm, dbColumnName,
                                                true, DataSourceUpdateMode.OnValidation);
                }
            }
        }

        private bool ShowEmptyExclamation(ITMTDatabaseUIControl control, object oValue)
        {
            bool cancelSave = false;
            if (oValue == null || string.IsNullOrWhiteSpace(oValue.ToString()))
            {
                string message = string.Format(CultureInfo.InvariantCulture, Properties.Resources.Exclamation_MandatoryValueEmpty,
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

        private void TmtControl_CheckedChanged(object sender, EventArgs e)
        {
            this.TmtControlValueChanged(sender);
        }

        private void TmtControl_GotFocus(object sender, EventArgs e)
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

        private void TmtControl_TextChanged(object sender, EventArgs e)
        {
            this.TmtControlValueChanged(sender);
        }

        private void TmtControlValueChanged(object sender)
        {
            if (this._isDataSourceTableLoading)
            {
                return;
            }
            if (sender is ITMTDatabaseUIControl tmtControl)
            {
                if (string.IsNullOrWhiteSpace(tmtControl.DbColumnName))
                {
                    return;
                }

                object senderValue = null;
                if (sender is NumericUpDown senderNumericUpDown)
                {
                    senderValue = senderNumericUpDown.Value;
                }
                else if (sender is DateTimePicker senderDateTimePicker)
                {
                    senderValue = senderDateTimePicker.Value;
                }
                else if (sender is TMTCheckBox senderTMTCheckBox)
                {
                    senderValue = senderTMTCheckBox.DbValue;
                }
                else if (sender is ComboBox senderComboBox)
                {
                    senderValue = senderComboBox.SelectedValue;
                }
                else
                {
                    senderValue = (sender as Control).Text;
                }

                var dataRowView = this.bindingSourceForm.Current as DataRowView;
                if (dataRowView == null)
                {
                    return;
                }
                var orginalValue = dataRowView.Row[tmtControl.DbColumnName, DataRowVersion.Current];
                if ((senderValue == null && orginalValue != null) ||
                    (senderValue != null && orginalValue == null) ||
                    (senderValue.ToString() != orginalValue.ToString()))
                {
                    this.DataChanged();
                }
            }
        }
    }
}
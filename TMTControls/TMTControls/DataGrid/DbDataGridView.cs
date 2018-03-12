using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using TMT.Controls.WinForms.Dialogs;

namespace TMT.Controls.WinForms.DataGrid
{
    public enum NewRowPossition
    {
        After = 0,
        Before = 1
    }

    [ToolboxBitmap(typeof(DataGridView))]
    [Designer(typeof(ControlDesigner)), DefaultEvent("LovLoading")]
    public class DbDataGridView : DataGridView
    {
        public DbDataGridView()
        {
            InitializeComponent();

            this.SuspendLayout();

            this.AutoGenerateColumns = false;
            this.SetTheme();
            this.LoadSchema = true;
            this.DoubleBuffered = true;

            this.ResumeLayout(false);
        }

        [Category("Data")]
        public event EventHandler<ListOfValueLoadedEventArgs> ListOfValueLoaded;

        [Category("Data")]
        public event EventHandler<ListOfValueLoadingEventArgs> ListOfValueLoading;

        [Category("Behavior"), DefaultValue(NewRowPossition.After)]
        public NewRowPossition AddNewRowPossition { get; set; }

        [DefaultValue(false)]
        public new bool AllowUserToAddRows
        {
            get
            {
                return base.AllowUserToAddRows;
            }
            set
            {
                base.AllowUserToAddRows = value;
            }
        }

        [DefaultValue(false)]
        public new bool AllowUserToDeleteRows
        {
            get
            {
                return base.AllowUserToDeleteRows;
            }
            set
            {
                base.AllowUserToDeleteRows = value;
            }
        }

        [DefaultValue(false)]
        public new bool AllowUserToOrderColumns
        {
            get
            {
                return base.AllowUserToOrderColumns;
            }
            set
            {
                base.AllowUserToOrderColumns = value;
            }
        }

        [DefaultValue(BorderStyle.None)]
        public new BorderStyle BorderStyle
        {
            get
            {
                return base.BorderStyle;
            }
            set
            {
                base.BorderStyle = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataTable DataSourceTable
        {
            get
            {
                return this.DataSource as DataTable;
            }
            set
            {
                this.DataSource = value;
            }
        }

        [Category("Data")]
        public string DefaultOrderByStatement { get; set; }

        [Category("Data")]
        public string DefaultWhereStatement { get; set; }

        [DefaultValue(false)]
        public new bool EnableHeadersVisualStyles
        {
            get
            {
                return base.EnableHeadersVisualStyles;
            }
            set
            {
                base.EnableHeadersVisualStyles = value;
            }
        }

        [Category("Data"), DefaultValue(false)]
        public bool IsDeleteAllowed { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool IsNewAllowed { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool LoadDataWhenActive { get; set; }

        [Category("Data"), DefaultValue(true)]
        public bool LoadSchema { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<int> SelectedRowIndexList
        {
            get
            {
                var selectedCellsRowIndexes = this.SelectedCells.Cast<DataGridViewCell>().Select(c => c.RowIndex);
                var selectedRowIndexes = selectedCellsRowIndexes.Distinct();
                return selectedRowIndexes.ToList();
            }
        }

        [Category("Data")]
        public string TableName { get; set; }

        [Category("Data")]
        public string ViewName { get; set; }

        public bool AddNewRow()
        {
            if (this.IsNewAllowed == false)
            {
                return false;
            }

            this.DataSourceTable.Columns.Cast<DataColumn>().ToList().ForEach(c => c.AllowDBNull = true);

            int location = this.DataSourceTable.Rows.Count;
            if (this.SelectedRowIndexList.Count > 0)
            {
                if (this.AddNewRowPossition == NewRowPossition.After)
                {
                    location = this.SelectedRowIndexList.Last() + 1;
                }
                else if (this.AddNewRowPossition == NewRowPossition.Before)
                {
                    location = this.SelectedRowIndexList.First();
                    if (location < 0)
                    {
                        location = 0;
                    }
                }
            }
            var newRow = this.DataSourceTable.NewRow();
            this.DataSourceTable.Rows.InsertAt(newRow, location);

            return true;
        }

        public bool DeleteSelectedRows()
        {
            if (this.IsDeleteAllowed == false)
            {
                return false;
            }
            if (this.SelectedRows == null ||
                this.SelectedRows.Count <= 0)
            {
                return false;
            }
            if (MessageDialog.ShowQuestion(this, Properties.Resources.QUESTION_DeleteConfirmAll,
                                        Properties.Resources.QUESTION_Delete) == DialogResult.Yes)
            {
                foreach (DataGridViewRow deleteRow in this.SelectedRows)
                {
                    this.Rows.RemoveAt(deleteRow.Index);
                }
                return true;
            }
            return false;
        }

        public bool DuplicateSelectedRows()
        {
            if (this.IsNewAllowed == false)
            {
                return false;
            }
            if (this.SelectedRows == null ||
                this.SelectedRows.Count <= 0)
            {
                return false;
            }
            this.DataSourceTable.Columns.Cast<DataColumn>().ToList().ForEach(c => c.AllowDBNull = true);
            bool rowsDuplicated = false;
            foreach (DataGridViewRow selectedViewRow in this.SelectedRows)
            {
                if (selectedViewRow.Cells.Cast<DataGridViewCell>().All(cell => cell.Value == null &&
                                                                               string.IsNullOrWhiteSpace(cell.ValueString())))
                {
                    continue;
                }

                var dupliacteRow = this.DataSourceTable.NewRow();
                var columnList = this.Columns.Cast<DataGridViewColumn>()
                                            .Where(c => c.Visible &&
                                                        c.ReadOnly == false &&
                                                        string.IsNullOrWhiteSpace(c.DataPropertyName) == false &&
                                                        (c is IDbColumn myColum && myColum.DataPropertyPrimaryKey == false));
                foreach (var col in columnList)
                {
                    dupliacteRow[col.DataPropertyName] = selectedViewRow.Cells[col.Name].Value;
                }

                int location = this.DataSourceTable.Rows.Count;
                if (this.SelectedRowIndexList.Count > 0)
                {
                    location = this.SelectedRowIndexList.Last() + 1;
                }
                this.DataSourceTable.Rows.InsertAt(dupliacteRow, location);
                rowsDuplicated = true;
            }
            return rowsDuplicated;
        }

        public void SetTheme()
        {
            this.SuspendLayout();

            this.BackgroundColor = Properties.Settings.Default.DataGridViewBackgroundColor;
            this.DefaultCellStyle.BackColor = Properties.Settings.Default.DataGridViewDefaultCellBackColor;
            this.DefaultCellStyle.ForeColor = Properties.Settings.Default.DataGridViewDefaultCellForeColor;
            this.DefaultCellStyle.SelectionBackColor = Properties.Settings.Default.DataGridViewDefaultCellSelectionBackColor;
            this.DefaultCellStyle.SelectionForeColor = Properties.Settings.Default.DataGridViewDefaultCellSelectionForeColor;

            this.ResumeLayout(false);
        }

        internal bool DataValidateBeforeSave(DataSet dataToBeSaved)
        {
            if (dataToBeSaved == null)
            {
                throw new ArgumentNullException(nameof(dataToBeSaved));
            }

            if (dataToBeSaved.Tables.Contains(this.TableName))
            {
                var mandatoryViewColumnList = this.GetMandatoryViewColumnList();

                if (mandatoryViewColumnList != null &&
                    mandatoryViewColumnList.Count > 0)
                {
                    var rowCollection = dataToBeSaved.Tables[this.TableName].Rows;
                    foreach (DataRow row in rowCollection)
                    {
                        if (row.RowState != DataRowState.Deleted)
                        {
                            foreach (var mandatoryViewColumn in mandatoryViewColumnList)
                            {
                                var cancelSave = this.ShowEmptyExclamation(row[mandatoryViewColumn.DataPropertyName],
                                                                           mandatoryViewColumn.HeaderText);
                                if (cancelSave)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        internal DataTable GetDataSourceTableChanges()
        {
            if (this.DataSourceTable != null)
            {
                this.EndEdit();

                var changedData = this.DataSourceTable.GetDataSourceTableChanges(this.TableName);

                if (changedData != null && changedData.Rows.Count > 0 &&
                    this.Columns != null && this.Columns.Count > 0)
                {
                    var readOnlyColumns = this.Columns.Cast<DataGridViewColumn>().Where(c => c.ReadOnly &&
                                                                                        string.IsNullOrWhiteSpace(c.DataPropertyName) == false &&
                                                                                        (c is IDbColumn myColumn &&
                                                                                         myColumn.DataPropertyMandatory == false &&
                                                                                         myColumn.DataPropertyPrimaryKey == false));
                    var readOnlyColumnNames = readOnlyColumns.Select(c => c.DataPropertyName).ToList();
                    foreach (var readOnlyColumn in readOnlyColumns)
                    {
                        if (readOnlyColumn is DbReadOnlyTextBoxColumn myReadOnlyTextBoxColumn)
                        {
                            if (myReadOnlyTextBoxColumn.DataPropertyEditAllowed)
                            {
                                readOnlyColumnNames.Remove(myReadOnlyTextBoxColumn.DataPropertyName);
                            }
                        }
                        else if (readOnlyColumn is DbLinkColumn myLinkColumn)
                        {
                            if (myLinkColumn.DataPropertyEditAllowed)
                            {
                                readOnlyColumnNames.Remove(myLinkColumn.DataPropertyName);
                            }
                        }
                    }

                    foreach (string readOnlyColumnName in readOnlyColumnNames)
                    {
                        changedData.Columns.Remove(readOnlyColumnName);
                    }
                }
                return changedData;
            }
            return null;
        }

        internal IReadOnlyCollection<DataGridViewColumn> GetKeyViewColumnList()
        {
            return this.Columns.Cast<DataGridViewColumn>()
                               .Where(c => (c is IDbColumn myCol && myCol.DataPropertyPrimaryKey)).ToList();
        }

        internal async Task GetListOfValueSelectedRow(ListOfValueLoadingEventArgs e)
        {
            ListOfValueLoading?.Invoke(this, e);
            if (e.Handled == false)
            {
                var basewindow = this.FindParentBaseWindow();
                if (basewindow != null)
                {
                    basewindow.FillSearchConditionTable(e);
                    await basewindow.DataPopulateAllListOfValueRecords(e);
                }
            }
        }

        internal IReadOnlyCollection<DataGridViewColumn> GetMandatoryViewColumnList()
        {
            return this.Columns.Cast<DataGridViewColumn>()
                               .Where(c => c.ReadOnly == false &&
                                          (c is IDbColumn myCol && myCol.DataPropertyMandatory)).ToList();
        }

        internal Dictionary<string, bool> GetViewColumnDbNameDictionary()
        {
            var viewAllColumnList = this.Columns.Cast<DataGridViewColumn>().Where(c => string.IsNullOrWhiteSpace(c.DataPropertyName) == false);
            if (viewAllColumnList.Count() > 0)
            {
                var isFuntionDictionary = new Dictionary<string, bool>();
                foreach (var viewAllColumn in viewAllColumnList)
                {
                    if (viewAllColumn is DbReadOnlyTextBoxColumn myReadOnlyTextBoxColumn)
                    {
                        isFuntionDictionary.Add(viewAllColumn.DataPropertyName, myReadOnlyTextBoxColumn.DataPropertyIsFuntion);
                    }
                    else if (viewAllColumn is DbLinkColumn myLinkColumn)
                    {
                        isFuntionDictionary.Add(viewAllColumn.DataPropertyName, myLinkColumn.DataPropertyIsFuntion);
                    }
                    else
                    {
                        isFuntionDictionary.Add(viewAllColumn.DataPropertyName, false);
                    }
                }

                return isFuntionDictionary;
            }
            return null;
        }

        internal IReadOnlyCollection<SearchEntity> InitializeTableControls()
        {
            this.DataSourceTable = new DataTable(this.ViewName)
            {
                Locale = CultureInfo.InvariantCulture
            };

            var searchEntityList = new List<SearchEntity>();
            foreach (DataGridViewColumn viewCol in this.Columns.Cast<DataGridViewColumn>().Where(c => c is IDbColumn))
            {
                if (viewCol.ValueType == null)
                {
                    MessageDialog.Show(this,
                        string.Format(CultureInfo.InvariantCulture, Properties.Resources.ERROR_ColumValueTypeMissing, viewCol.HeaderText),
                        Properties.Resources.DESIN_ERROR,
                        MessageDialogIcon.Error);
                }
                else
                {
                    if (viewCol.Visible && (viewCol is DbImageColumn) == false)
                    {
                        var searchEntity = new SearchEntity()
                        {
                            Caption = viewCol.HeaderText,
                            ColumnName = viewCol.DataPropertyName,
                            DataType = viewCol.ValueType
                        };

                        if (viewCol is DataGridViewCheckBoxColumn viewCheckBoxCol)
                        {
                            searchEntity.IsCheckBox = true;
                            searchEntity.FalseValue = viewCheckBoxCol.FalseValue;
                            searchEntity.TrueValue = viewCheckBoxCol.TrueValue;
                            searchEntity.IndeterminateValue = viewCheckBoxCol.IndeterminateValue;
                        }
                        else if (viewCol is DbTextButtonBoxColumn viewTextButtonCol)
                        {
                            searchEntity.ListOfValueView = viewTextButtonCol.ListOfValueViewName;
                        }
                        searchEntityList.Add(searchEntity);
                    }

                    this.DataSourceTable.Columns.Add(viewCol.DataPropertyName, viewCol.ValueType);
                }
            }

            this.SetTheme();

            return searchEntityList;
        }

        internal void SetListOfValueSelectedRow(ListOfValueLoadedEventArgs e)
        {
            ListOfValueLoaded?.Invoke(this, e);
        }

        internal bool ShowEmptyExclamation(object oValue, string columnHeaderText)
        {
            bool cancelSave = false;
            if (oValue == null || string.IsNullOrWhiteSpace(oValue.ToString()))
            {
                MessageDialog.Show(this,
                    string.Format(CultureInfo.InvariantCulture, Properties.Resources.Exclamation_MandatoryValueEmpty, columnHeaderText),
                    Properties.Resources.Exclamation_MandatoryEmpty,
                    MessageDialogIcon.Exclamation);
                cancelSave = true;
            }
            return cancelSave;
        }

        protected bool MyProcessTabKey()
        {
            bool retValue = false;
            try
            {
                if (this.CurrentRow.Index == (this.Rows.Count - 1))
                {
                    if (this.CurrentCell.ColumnIndex == (this.ColumnCount - 2))
                    {
                        var nextColumnIndex = this.CurrentCell.ColumnIndex + 1;
                        if (this.CurrentRow.Cells[nextColumnIndex].ReadOnly)
                        {
                            return false;
                        }

                        if (this.Columns[nextColumnIndex] is IDbColumn nextDbColumn)
                        {
                            if (nextDbColumn.TabStop == false)
                            {
                                return false;
                            }
                        }
                    }
                }

                retValue = base.ProcessTabKey(Keys.Tab);
                while ((this.CurrentCell.ReadOnly ||
                        (this.Columns[this.CurrentCell.ColumnIndex] as IDbColumn)?.TabStop == false) &&
                       retValue)
                {
                    retValue = base.ProcessTabKey(Keys.Tab);
                }
            }
            catch
            {
            }
            return retValue;
        }

        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            if (e.KeyCode == Keys.Tab)
            {
                return MyProcessTabKey();
            }
            return base.ProcessDataGridViewKey(e);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                return MyProcessTabKey();
            }
            return base.ProcessDialogKey(keyData);
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            //
            // Db
            //
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = true;
            this.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RowTemplate.Height = 24;
            this.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.Db_CellValidated);
            this.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.Db_DataError);
            this.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Db_EditingControlShowing);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }

        private void Db_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex <= -1 ||
                    e.RowIndex <= -1)
                {
                    return;
                }
                if (this.Columns[e.ColumnIndex] is DbTextBoxColumn textBoxCol)
                {
                    var myCell = this.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (myCell.Value == null || string.IsNullOrWhiteSpace(myCell.Value.ToString()))
                    {
                        return;
                    }
                    try
                    {
                        if (textBoxCol.ValidateType == MaskValidateType.Email)
                        {
                            myCell.Value = new MailAddress(myCell.Value.ToString()).Address;
                            //email address is valid since the above line has not thrown an exception
                        }
                        else if (textBoxCol.ValidateType == MaskValidateType.Phone)
                        {
                            myCell.Value = PhoneNumberUtil.GetInstance().Parse(myCell.Value.ToString(), textBoxCol.CountryCode).NationalNumber.ToString();
                        }
                        myCell.Style.BackColor = Color.Empty;
                    }
                    catch
                    {
                        myCell.Style.BackColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_CellValidation);
            }
        }

        private void Db_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            ErrorDialog.Show(this, e.Exception, string.Format(CultureInfo.InvariantCulture, Properties.Resources.ERROR_CellDataError, e.ColumnIndex, e.RowIndex));
        }

        private void Db_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox editControlTextBox)
            {
                if (this.CurrentCell.OwningColumn is DbTextBoxColumn dbTextBoxColumn)
                {
                    editControlTextBox.CharacterCasing = dbTextBoxColumn.CharacterCasing;
                }
            }
            else if (e.Control is TextButtonBoxBase editControlTextButtonBox)
            {
                if (this.CurrentCell.OwningColumn is DbTextButtonBoxColumn dbTextButtonBoxColumn)
                {
                    editControlTextButtonBox.CharacterCasing = dbTextButtonBoxColumn.CharacterCasing;
                }
            }
        }
    }
}
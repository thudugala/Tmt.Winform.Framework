using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace TMTControls.TMTDataGrid
{
    [ToolboxBitmap(typeof(DataGridView))]
    [Designer(typeof(ControlDesigner)), DefaultEvent("LovLoading")]
    public class TMTDataGridView : DataGridView
    {
        public TMTDataGridView()
        {
            InitializeComponent();

            this.SuspendLayout();

            this.AutoGenerateColumns = false;
            this.SetTheme();

            this.ResumeLayout(false);
        }

        [Category("TMT Data")]
        public event LovLoadedEventHandler LovLoaded;

        [Category("TMT Data")]
        public event LovLoadingEventHandler LovLoading;

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

        [Category("TMT Data"), DefaultValue(false)]
        public bool AddDataAllowed { get; set; }

        [Category("TMT Data"), DefaultValue(false)]
        public bool DeleteDataAllowed { get; set; }

        [Category("TMT Data"), DefaultValue(false)]
        public bool LoadDataWhenActive { get; set; }

        [Category("TMT Data")]
        public string TableName { get; set; }

        [Category("TMT Data")]
        public string ViewName { get; set; }

        [Category("TMT Data")]
        public string DefaultWhereStatment { get; set; }

        [Category("TMT Data")]
        public string DefaultOrderByStatment { get; set; }

        public DataTable GetDataSourceTableChanges()
        {
            if (this.DataSourceTable != null)
            {
                this.EndEdit();

                DataTable changedData = this.DataSourceTable.GetDataSourceTableChanges(this.TableName);

                if (changedData != null && changedData.Rows.Count > 0 &&
                    this.Columns != null && this.Columns.Count > 0)
                {
                    var readOnlyColumnNames = this.Columns.Cast<DataGridViewColumn>().Where(c => c.ReadOnly &&
                                                                                                 string.IsNullOrWhiteSpace(c.DataPropertyName) == false &&
                                                                                                 c.GetDataSourceInformation().KeyColumn == false &&
                                                                                                 c.GetDataSourceInformation().MandatoryColumn == false &&
                                                                                                 c.GetDataSourceInformation().EditAllowed == false).Select(c => c.DataPropertyName);
                    foreach (string readOnlyColumnName in readOnlyColumnNames)
                    {
                        changedData.Columns.Remove(readOnlyColumnName);
                    }
                }
                return changedData;
            }
            return null;
        }

        public List<DataGridViewColumn> GetKeyViewColumnList()
        {
            return this.Columns.Cast<DataGridViewColumn>().Where(c => c.GetDataSourceInformation().KeyColumn).ToList();
        }

        public List<DataGridViewColumn> GetMandatoryViewColumnList()
        {
            return this.Columns.Cast<DataGridViewColumn>().Where(c => c.ReadOnly == false && c.GetDataSourceInformation().MandatoryColumn).ToList();
        }

        public Dictionary<string, bool> GetViewColumnDbNameDictionary()
        {
            var viewAllColumnList = this.Columns.Cast<DataGridViewColumn>().Where(c => string.IsNullOrWhiteSpace(c.DataPropertyName) == false);
            if (viewAllColumnList != null && viewAllColumnList.Count() > 0)
            {
                var viewColumnList = viewAllColumnList.Select(c => new { DbColumnName = c.DataPropertyName, IsFuntion = c.GetDataSourceInformation().IsFuntion });

                return viewColumnList.ToDictionary(c => c.DbColumnName, c => c.IsFuntion);
            }
            return null;
        }

        public List<TMTSearchDialog.SearchEntity> InitializeTableControls()
        {
            this.DataSourceTable = new DataTable(this.ViewName);

            List<TMTSearchDialog.SearchEntity> searchEntityList = new List<TMTSearchDialog.SearchEntity>();
            TMTSearchDialog.SearchEntity searchEntity;
            foreach (DataGridViewColumn viewCol in this.Columns)
            {
                if (viewCol is ITMTDataGridViewColumn)
                {
                    if (viewCol.ValueType == null)
                    {
                        MessageBox.Show(this, "[" + viewCol.HeaderText + "] does not have a ValueType", "TMT Design Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (viewCol.Visible && (viewCol is TMTDataGridViewImageColumn) == false)
                        {
                            searchEntity = new TMTSearchDialog.SearchEntity()
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
                            else if (viewCol is TMTDataGridViewTextButtonBoxColumn viewTextButtonCol)
                            {
                                searchEntity.LovView = viewTextButtonCol.LovViewName;
                            }
                            searchEntityList.Add(searchEntity);
                        }

                        this.DataSourceTable.Columns.Add(viewCol.DataPropertyName, viewCol.ValueType);
                    }
                }
            }

            this.SetTheme();

            return searchEntityList;
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

        protected bool MyProcessTabKey()
        {
            bool retValue = false;
            try
            {
                retValue = base.ProcessTabKey(Keys.Tab);
                while (this.CurrentCell.ReadOnly && this.CurrentCell.ColumnIndex != (this.ColumnCount - 1))
                {
                    retValue = base.ProcessTabKey(Keys.Tab);
                }
            }
            catch { }
            return retValue;
        }

        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
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

        public void GetLovSelectedRow(LovLoadingEventArgs rowEvent)
        {
            LovLoading?.Invoke(this, rowEvent);
        }

        public void SetLovSelectedRow(LovLoadedEventArgs rowEvent)
        {
            LovLoaded?.Invoke(this, rowEvent);
        }

        private void TMTDataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex > -1 &&
                    e.RowIndex > -1)
                {
                    if (this.Columns[e.ColumnIndex] is TMTDataGridViewTextBoxColumn textBoxCol)
                    {
                        DataGridViewCell myCell = this.Rows[e.RowIndex].Cells[e.ColumnIndex];

                        if (myCell.Value != null && string.IsNullOrWhiteSpace(myCell.Value.ToString()) == false)
                        {
                            try
                            {
                                if (textBoxCol.ValidateType == MaskValidateType.Email)
                                {
                                    myCell.Value = new MailAddress(myCell.Value.ToString()).Address;
                                    //email address is valid since the above line has not thrown an exception
                                }
                                else if (textBoxCol.ValidateType == MaskValidateType.Phone)
                                {
                                    myCell.Value = PhoneNumberFormatter.PhoneNumberFormatter.Format(myCell.Value.ToString(), textBoxCol.CountryCode);
                                }
                                myCell.Style.BackColor = Color.Empty;
                            }
                            catch
                            {
                                myCell.Style.BackColor = Color.Red;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_CellValidation);
            }
        }

        public void FillSearchConditionTable(LovLoadingEventArgs e, params KeyValuePair<string, string>[] filterColumns)
        {
            if (e.IsValidate)
            {
                e.SearchConditionTable.Rows.Add(e.PrimaryColumnName, e.PrimaryColumnValue, e.PrimaryColumnType, false);
            }
            else
            {
                foreach (KeyValuePair<string, string> filterColumn in filterColumns)
                {
                    if (e.Row.Cells[filterColumn.Value].Value != null)
                    {
                        e.SearchConditionTable.Rows.Add(filterColumn.Key,
                                                        e.Row.Cells[filterColumn.Value].Value,
                                                        this.Columns[filterColumn.Value].ValueType.FullName,
                                                        false);
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            //
            // TMTDataGridView
            //
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EnableHeadersVisualStyles = false;
            this.RowTemplate.Height = 24;
            this.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.TMTDataGridView_CellValidated);
            this.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.TMTDataGridView_DataError);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }

        private void TMTDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            TMTErrorDialog.Show(this, e.Exception, string.Format(Properties.Resources.ERROR_CellDataError, e.ColumnIndex, e.RowIndex));
        }

        public void AddNewRow()
        {
            if (this.AddDataAllowed)
            {
                foreach (DataColumn aColumn in this.DataSourceTable.Columns)
                {
                    if (aColumn.AllowDBNull == false)
                    {
                        aColumn.AllowDBNull = true;
                    }
                }
                int location = this.DataSourceTable.Rows.Count;
                if (this.SelectedRowIndexList.Count > 0)
                {
                    location = this.SelectedRowIndexList.Last() + 1;
                }
                DataRow newRow = this.DataSourceTable.NewRow();
                this.DataSourceTable.Rows.InsertAt(newRow, location);
            }
        }

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
    }
}
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
        private List<DataGridViewColumn> _keyViewColumnList;
        private Dictionary<string, string> _lovColumnDictionary;
        private List<DataGridViewColumn> _mandatoryViewColumnList;
        private TMTLOVDialog LovDialog;

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

        [Category("TMT Data")]
        public bool AddDataAllowed { get; set; }

        [Category("TMT Data")]
        public bool DeleteDataAllowed { get; set; }

        [Category("TMT Data")]
        public string TableName { get; set; }

        [Category("TMT Data")]
        public string ViewName { get; set; }

        [Category("TMT Data")]
        public int? ViewRowCount { get; set; }

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
                                                                                                 c.GetDataSourceInformation().KeyColum == false).Select(c => c.DataPropertyName);
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
            return this._keyViewColumnList;
        }

        public List<DataGridViewColumn> GetMandatoryViewColumnList()
        {
            return this._mandatoryViewColumnList;
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
            this._mandatoryViewColumnList = new List<DataGridViewColumn>();
            this._keyViewColumnList = new List<DataGridViewColumn>();

            List<TMTSearchDialog.SearchEntity> searchEntityList = new List<TMTSearchDialog.SearchEntity>();
            TMTSearchDialog.SearchEntity searchEntity;
            TMTDataGridViewDataSourceInformation dataSourceInfor;
            foreach (DataGridViewColumn viewCol in this.Columns)
            {
                if ((viewCol is TMTDataGridViewButtonColumn) == false)
                {
                    if (viewCol.ValueType == null)
                    {
                        MessageBox.Show(this, "[" + viewCol.HeaderText + "] does not have a ValueType", "Design Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (viewCol.Visible && (viewCol is TMTDataGridViewImageColumn) == false)
                        {
                            searchEntity = new TMTSearchDialog.SearchEntity();
                            searchEntity.Caption = viewCol.HeaderText;
                            searchEntity.ColumnName = viewCol.DataPropertyName;
                            searchEntity.DataType = viewCol.ValueType;

                            searchEntityList.Add(searchEntity);
                        }

                        this.DataSourceTable.Columns.Add(viewCol.DataPropertyName, viewCol.ValueType);

                        dataSourceInfor = viewCol.GetDataSourceInformation();
                        if (dataSourceInfor.MandatoryColum)
                        {
                            this._mandatoryViewColumnList.Add(viewCol);
                        }
                        if (dataSourceInfor.KeyColum)
                        {
                            this._keyViewColumnList.Add(viewCol);
                        }

                        if (viewCol is TMTDataGridViewTextButtonBoxColumn)
                        {
                            TMTDataGridViewTextButtonBoxColumn viewTextButtonCol = viewCol as TMTDataGridViewTextButtonBoxColumn;
                            if (string.IsNullOrWhiteSpace(dataSourceInfor.LovViewName) == false)
                            {
                                viewTextButtonCol.ButtonClick += new System.EventHandler(viewTextButtonCol_ButtonClick);
                            }
                        }
                    }
                }
            }

            this.SetLovColumnDictionary();

            this.SetTheme();

            return searchEntityList;
        }

        private void viewTextButtonCol_ButtonClick(object sender, EventArgs e)
        {
            this.ShowLovDialog(this.CurrentCell.OwningColumn.Name, this.CurrentCell.ColumnIndex, this.CurrentCell.RowIndex);
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
                MessageBox.Show(this, ex.Message, Properties.Resources.MSG_LOV_LoadError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetLovColumnDictionary()
        {
            this._lovColumnDictionary = new Dictionary<string, string>();

            TMTDataGridViewDataSourceInformation dataSourceInfor;
            foreach (DataGridViewColumn viewCol in this.Columns)
            {
                dataSourceInfor = viewCol.GetDataSourceInformation();

                if (string.IsNullOrWhiteSpace(dataSourceInfor.LovViewName) == false &&
                    (viewCol is TMTDataGridViewButtonColumn) == false)
                {
                    this._lovColumnDictionary.Add(dataSourceInfor.LovViewName, viewCol.Name);
                }
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
                MessageBox.Show(this, ex.Message, Properties.Resources.MSG_LOV_SetError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TMTDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    TMTDataGridViewDataSourceInformation dataSourceInfor = this.Columns[e.ColumnIndex].GetDataSourceInformation();
                    if (string.IsNullOrWhiteSpace(dataSourceInfor.LovViewName) == false &&
                        this.Columns[e.ColumnIndex] is TMTDataGridViewButtonColumn)
                    {
                        string primaryColumnName = this._lovColumnDictionary[dataSourceInfor.LovViewName];

                        this.ShowLovDialog(primaryColumnName, e.ColumnIndex, e.RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Properties.Resources.ERROR_CellClick, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowLovDialog(string primaryColumnName, int columnIndex, int rowIndex)
        {
            if (this.LovDialog == null)
            {
                this.LovDialog = new TMTLOVDialog();
            }

            LovLoadingEventArgs lovEvent = new LovLoadingEventArgs();
            lovEvent.RowIndex = rowIndex;
            lovEvent.LoadAll = true;
            lovEvent.PrimaryColumnName = primaryColumnName;

            this.GetLovSelectedRow(lovEvent);

            this.LovDialog.SetDataSourceTable(lovEvent.LovDataTable);

            if (this.LovDialog.ShowDialog(this) == DialogResult.OK)
            {
                DataGridViewRow currectRow = this.Rows[rowIndex];

                LovLoadedEventArgs lovLoaded = new LovLoadedEventArgs();
                lovLoaded.RowIndex = rowIndex;
                lovLoaded.PrimaryColumnName = primaryColumnName;
                lovLoaded.SelectedRow = this.LovDialog.SelectedRow;
                this.SetLovSelectedRow(lovLoaded);

                currectRow.Cells[primaryColumnName].Selected = true;
                this.CurrentCell.Tag = this.CurrentCell.Value;
                this.CurrentCell.Style.BackColor = Color.Empty;
                //this.NotifyCurrentCellDirty(true);
                this.EndEdit();
                //this.NotifyCurrentCellDirty(false);
                //this.AutoResizeColumn(columnIndex);
            }
        }

        private void TMTDataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex > -1 &&
                    e.RowIndex > -1)
                {
                    DataGridViewColumn validatingViewColumn = this.Columns[e.ColumnIndex];
                    TMTDataGridViewDataSourceInformation dataSourceInfor = validatingViewColumn.GetDataSourceInformation();
                    if (string.IsNullOrWhiteSpace(dataSourceInfor.LovViewName) == false &&
                        (validatingViewColumn is TMTDataGridViewButtonColumn ||
                         validatingViewColumn is TMTDataGridViewTextButtonBoxColumn))
                    {
                        DataGridViewRow currectRow = this.Rows[e.RowIndex];
                        string primaryColumnName = string.Empty;
                        if (validatingViewColumn is TMTDataGridViewButtonColumn)
                        {
                            primaryColumnName = this._lovColumnDictionary[dataSourceInfor.LovViewName];
                        }
                        else if (validatingViewColumn is TMTDataGridViewTextButtonBoxColumn)
                        {
                            primaryColumnName = validatingViewColumn.Name;
                        }

                        DataGridViewCell validatingPrimaryViewCell = currectRow.Cells[primaryColumnName];
                        object oValue = validatingPrimaryViewCell.Value;
                        string sValue = string.Empty;
                        if (oValue != null)
                        {
                            sValue = oValue.ToString();

                            object oldOValue = validatingPrimaryViewCell.Tag;
                            if (oldOValue != null && sValue == oldOValue.ToString())
                            {
                                return;
                            }
                        }

                        if (string.IsNullOrWhiteSpace(sValue) == false)
                        {
                            LovLoadingEventArgs lovEvent = new LovLoadingEventArgs();
                            lovEvent.RowIndex = e.RowIndex;
                            lovEvent.LoadAll = false;
                            lovEvent.PrimaryColumnName = primaryColumnName;

                            this.GetLovSelectedRow(lovEvent);

                            if (lovEvent.LovDataTable != null)
                            {
                                bool dataFound = (lovEvent.LovDataTable.Rows.Count == 1);

                                if (dataFound)
                                {
                                    LovLoadedEventArgs lovLoaded = new LovLoadedEventArgs();
                                    lovLoaded.RowIndex = e.RowIndex;
                                    lovLoaded.PrimaryColumnName = primaryColumnName;
                                    lovLoaded.SelectedRow = new Dictionary<string, object>();

                                    DataRow row = lovEvent.LovDataTable.Rows[0];
                                    foreach (DataColumn col in lovEvent.LovDataTable.Columns)
                                    {
                                        lovLoaded.SelectedRow.Add(col.ColumnName, row[col]);
                                    }
                                    this.SetLovSelectedRow(lovLoaded);
                                }

                                validatingPrimaryViewCell.Style.BackColor = (dataFound) ? Color.Empty : Color.Red;
                                //this.AutoResizeColumn(e.ColumnIndex);
                            }
                            else
                            {
                                validatingPrimaryViewCell.Style.BackColor = Color.Empty;
                            }
                        }
                    }

                    if (this.Columns[e.ColumnIndex] is TMTDataGridViewTextBoxColumn)
                    {
                        TMTDataGridViewTextBoxColumn textBoxCol = this.Columns[e.ColumnIndex] as TMTDataGridViewTextBoxColumn;
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
                MessageBox.Show(this, ex.Message, Properties.Resources.ERROR_CellValidation, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // TMTDataGridView
            // 
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EnableHeadersVisualStyles = false;
            this.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TMTDataGridView_CellClick);
            this.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.TMTDataGridView_CellValidated);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
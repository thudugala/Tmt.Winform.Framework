using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.Dialogs
{
    public partial class ListOfValueDialog : BaseDialog
    {
        private SearchDialog searchDialog;

        public ListOfValueDialog()
        {
            InitializeComponent();
        }

        public Dictionary<string, object> SelectedRow { get; private set; }

        public void SetDataSourceTable(DataTable table)
        {
            DbMain.SetTheme();

            DbMain.DataSourceTable = table;
            if (DbMain.DataSourceTable != null)
            {
                var colType = this.GetColumnTypeDictionary();

                this.SelectedRow = new Dictionary<string, object>();

                DbMain.Columns.Clear();

                if (searchDialog == null)
                {
                    searchDialog = new SearchDialog();
                }
                searchDialog.EntityList.Clear();

                DataGridViewColumn vCol;
                foreach (DataColumn dCol in DbMain.DataSourceTable.Columns)
                {
                    if (colType[dCol.ColumnName] == typeof(byte[]).FullName)
                    {
                        vCol = new DataGridViewImageColumn();
                    }
                    else if (colType[dCol.ColumnName] == "ENUM_BOOLEAN")
                    {
                        vCol = new DataGridViewCheckBoxColumn();
                        var checkVCol = (vCol as DataGridViewCheckBoxColumn);
                        checkVCol.FalseValue = "FALSE";
                        checkVCol.TrueValue = "TRUE";
                        checkVCol.IndeterminateValue = "FALSE";
                    }
                    else
                    {
                        vCol = new DataGridViewTextBoxColumn();
                    }
                    vCol.Name = "col" + dCol.ColumnName;
                    vCol.DataPropertyName = dCol.ColumnName;
                    vCol.HeaderText = GetHeaderText(dCol.Caption);
                    if (dCol.ColumnName == "HIGHLIGHT_COLOR")
                    {
                        vCol.Visible = false;
                    }

                    this.SelectedRow.Add(dCol.ColumnName, string.Empty);

                    DbMain.Columns.Add(vCol);
                }
                DbMain.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

                buttonSearch.Enabled = (DbMain.DataSourceTable.Rows.Count > 1);
            }
        }

        private static string GetHeaderText(string orginalText)
        {
            orginalText = orginalText.Replace("_", " ").ToLowerInvariant().Trim();
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(orginalText);
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (DbMain.SelectedRows.Count == 1)
                {
                    var row = DbMain.SelectedRows[0];

                    var keyList = this.SelectedRow.Keys.ToList();

                    foreach (string key in keyList)
                    {
                        this.SelectedRow[key] = row.Cells["col" + key].Value;
                    }

                    this.DialogResult = DialogResult.OK;
                }
            }
            catch
            {
                throw;
            }
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (searchDialog.EntityList == null || searchDialog.EntityList.Count == 0)
                {
                    foreach (DataGridViewColumn vCol in DbMain.Columns)
                    {
                        var searchEntity = new SearchEntity()
                        {
                            Caption = vCol.HeaderText,
                            ColumnName = vCol.DataPropertyName,
                            DataType = DbMain.DataSourceTable.Columns[vCol.DataPropertyName].DataType
                        };
                        if (vCol is DataGridViewCheckBoxColumn checkVCol)
                        {
                            searchEntity.IsCheckBox = true;
                            searchEntity.FalseValue = checkVCol.FalseValue;
                            searchEntity.TrueValue = checkVCol.TrueValue;
                            searchEntity.IndeterminateValue = checkVCol.IndeterminateValue;
                        }
                        searchDialog.EntityList.Add(searchEntity);
                    }
                }

                if (searchDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string filter = string.Empty;

                    foreach (var sEntity in searchDialog.EntityList)
                    {
                        if (string.IsNullOrWhiteSpace(sEntity.Value) == false)
                        {
                            var sValueArray = sEntity.Value.ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            if (sValueArray.Length > 0)
                            {
                                for (int i = 0; i < sValueArray.Length; i++)
                                {
                                    if (string.IsNullOrWhiteSpace(filter) == false)
                                    {
                                        filter += " AND ";
                                    }

                                    var sValue = sValueArray[i].Trim();
                                    var operatorSymbol = Extends.GetDatabaseOperator(sValue);
                                    sValue = sValue.Replace(operatorSymbol, string.Empty).Trim();

                                    filter += $" `{sEntity.ColumnName}` {operatorSymbol} '{sValue}' ";
                                }
                            }
                        }
                    }

                    DbMain.DataSourceTable.DefaultView.RowFilter = filter;
                }
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_FilteringLovValues);
            }
        }

        private Dictionary<string, string> GetColumnTypeDictionary()
        {
            var colType = new Dictionary<string, string>();

            var sourceTable = DbMain.DataSourceTable;

            var enumBoolean = new List<string> { "TRUE", "FALSE" };

            foreach (DataColumn dCol in sourceTable.Columns)
            {
                colType.Add(dCol.ColumnName, dCol.DataType.FullName);

                if (typeof(string).FullName == dCol.DataType.FullName)
                {
                    var distinctValueList = sourceTable.Rows.Cast<DataRow>().Select(r => r[dCol.ColumnName]).Where(i => i.GetType() != typeof(DBNull)).Cast<string>().Distinct().ToList();
                    if (distinctValueList.Intersect(enumBoolean).Any())
                    {
                        colType[dCol.ColumnName] = "ENUM_BOOLEAN";
                    }
                }
            }

            return colType;
        }

        private void DbMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                buttonOK.PerformClick();
            }
            catch
            {
            }
        }

        private void DbMain_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (DbMain.DataSourceTable.Columns.Contains("HIGHLIGHT_COLOR"))
                {
                    foreach (DataGridViewRow vRow in DbMain.Rows)
                    {
                        var oColor = vRow.Cells["colHIGHLIGHT_COLOR"].ValueString();
                        if (string.IsNullOrWhiteSpace(oColor) == false)
                        {
                            vRow.DefaultCellStyle.BackColor = Color.FromName(oColor);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void DbMain_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    buttonOK.PerformClick();

                    e.Handled = true;
                }
            }
            catch
            {
            }
        }

        private void DbMain_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left &&
                    e.Clicks == 2)
                {
                    buttonOK.PerformClick();
                }
            }
            catch
            {
            }
        }

        private void ListOfValueDialog_Load(object sender, EventArgs e)
        {
            try
            {               
                this.Image = IconChar.ThList.ToBitmap(72, Color.FromArgb(0, 158, 178));
            }
            catch
            {
            }
        }
    }
}
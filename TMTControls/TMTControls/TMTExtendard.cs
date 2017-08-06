using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using TMTControls.TMTDataGrid;

namespace TMTControls
{
    public static class TMTExtendard
    {
        public static List<TMTDataGridView> GetChildDataGridViewList(this Control parentControl)
        {
            List<TMTDataGridView> childTableList = new List<TMTDataGridView>();

            foreach (Control childControl in parentControl.Controls)
            {
                if (childControl is TMTDataGridView)
                {
                    childTableList.Add(childControl as TMTDataGridView);
                }
                childTableList.AddRange(GetChildDataGridViewList(childControl));
            }

            return childTableList;
        }

        public static TMTDataSourceInformation GetDataSourceInformation(this Control myControl)
        {
            try
            {
                if (myControl is TMTTextBox)
                {
                    var myTextBox = myControl as TMTTextBox;
                    return new TMTDataSourceInformation()
                    {
                        DbLabelText = (myTextBox.ConnectedLabel != null) ? myTextBox.ConnectedLabel.Text : string.Empty,
                        DbColumnName = myTextBox.DbColumnName,
                        DbColumnType = myTextBox.DbColumnType,
                        MandatoryColumn = myTextBox.MandatoryColumn,
                        KeyColumn = myTextBox.KeyColumn
                    };
                }
                else if (myControl is TMTNumericUpDown)
                {
                    var myNumericUpDown = myControl as TMTNumericUpDown;
                    return new TMTDataSourceInformation()
                    {
                        DbLabelText = (myNumericUpDown.ConnectedLabel != null) ? myNumericUpDown.ConnectedLabel.Text : string.Empty,
                        DbColumnName = myNumericUpDown.DbColumnName,
                        DbColumnType = myNumericUpDown.DbColumnType,
                        MandatoryColumn = myNumericUpDown.MandatoryColum,
                        KeyColumn = myNumericUpDown.KeyColum
                    };
                }
                else if (myControl is TMTDateTimePicker)
                {
                    var myDateTimePicker = myControl as TMTDateTimePicker;
                    return new TMTDataSourceInformation()
                    {
                        DbLabelText = (myDateTimePicker.ConnectedLabel != null) ? myDateTimePicker.ConnectedLabel.Text : string.Empty,
                        DbColumnName = myDateTimePicker.DbColumnName,
                        DbColumnType = myDateTimePicker.DbColumnType,
                        MandatoryColumn = myDateTimePicker.MandatoryColum,
                        KeyColumn = myDateTimePicker.KeyColum
                    };
                }
                else if (myControl is TMTComboBox)
                {
                    var myComboBox = myControl as TMTComboBox;
                    return new TMTDataSourceInformation()
                    {
                        DbLabelText = (myComboBox.ConnectedLabel != null) ? myComboBox.ConnectedLabel.Text : string.Empty,
                        DbColumnName = myComboBox.DbColumnName,
                        DbColumnType = myComboBox.DbColumnType,
                        MandatoryColumn = myComboBox.MandatoryColum,
                        KeyColumn = myComboBox.KeyColum
                    };
                }
                else if (myControl is TMTCheckBox)
                {
                    var myCheckBox = myControl as TMTCheckBox;
                    return new TMTDataSourceInformation()
                    {
                        DbLabelText = myCheckBox.Text,
                        DbValue = (myCheckBox.Checked) ? myCheckBox.TrueValue : myCheckBox.FalseValue,
                        DbColumnName = myCheckBox.DbColumnName,
                        DbColumnType = myCheckBox.DbColumnType,
                        MandatoryColumn = myCheckBox.MandatoryColum,
                        KeyColumn = myCheckBox.KeyColum,
                        FalseValue = myCheckBox.FalseValue,
                        IndeterminateValue = myCheckBox.IndeterminateValue,
                        TrueValue = myCheckBox.TrueValue
                    };
                }
                else if (myControl is TMTTextButtonBox)
                {
                    var myTextButtonBox = myControl as TMTTextButtonBox;
                    return new TMTDataSourceInformation()
                    {
                        DbLabelText = (myTextButtonBox.ConnectedLabel != null) ? myTextButtonBox.ConnectedLabel.Text : string.Empty,
                        DbColumnName = myTextButtonBox.DbColumnName,
                        DbColumnType = myTextButtonBox.DbColumnType,
                        MandatoryColumn = myTextButtonBox.MandatoryColum,
                        KeyColumn = myTextButtonBox.KeyColum,
                        LovText = myTextButtonBox.LovText,
                        LovViewName = myTextButtonBox.LovViewName
                    };
                }
                return new TMTDataSourceInformation();
            }
            catch
            {
                throw;
            }
        }

        public static TMTDataSourceInformation GetDataSourceInformation(this DataGridViewColumn myColumn)
        {
            try
            {
                if (myColumn is TMTDataGridViewTextBoxColumn)
                {
                    var myTextBoxColumn = myColumn as TMTDataGridViewTextBoxColumn;
                    return new TMTDataSourceInformation()
                    {
                        DbLabelText = myTextBoxColumn.HeaderText,
                        DbColumnName = myTextBoxColumn.DataPropertyName,
                        DbColumnType = myTextBoxColumn.DataPropertyType,
                        MandatoryColumn = myTextBoxColumn.DataPropertyMandatory,
                        KeyColumn = myTextBoxColumn.DataPropertyPrimaryKey
                    };
                }
                else if (myColumn is TMTDataGridViewTextButtonBoxColumn)
                {
                    var myTextButtonBoxColumn = myColumn as TMTDataGridViewTextButtonBoxColumn;
                    return new TMTDataSourceInformation()
                    {
                        DbLabelText = myTextButtonBoxColumn.HeaderText,
                        DbColumnName = myTextButtonBoxColumn.DataPropertyName,
                        DbColumnType = myTextButtonBoxColumn.DataPropertyType,
                        MandatoryColumn = myTextButtonBoxColumn.DataPropertyMandatory,
                        KeyColumn = myTextButtonBoxColumn.DataPropertyPrimaryKey,
                        LovViewName = myTextButtonBoxColumn.LovViewName
                    };
                }
                else if (myColumn is TMTDataGridViewNumericUpDownColumn)
                {
                    var myNumericUpDownColumn = myColumn as TMTDataGridViewNumericUpDownColumn;
                    return new TMTDataSourceInformation()
                    {
                        DbLabelText = myNumericUpDownColumn.HeaderText,
                        DbColumnName = myNumericUpDownColumn.DataPropertyName,
                        DbColumnType = myNumericUpDownColumn.DataPropertyType,
                        MandatoryColumn = myNumericUpDownColumn.DataPropertyMandatory,
                        KeyColumn = myNumericUpDownColumn.DataPropertyPrimaryKey
                    };
                }
                else if (myColumn is TMTDataGridViewCalendarColumn)
                {
                    var myCalendarColumn = myColumn as TMTDataGridViewCalendarColumn;
                    return new TMTDataSourceInformation()
                    {
                        DbLabelText = myCalendarColumn.HeaderText,
                        DbColumnName = myCalendarColumn.DataPropertyName,
                        DbColumnType = myCalendarColumn.DataPropertyType,
                        MandatoryColumn = myCalendarColumn.DataPropertyMandatory,
                        KeyColumn = myCalendarColumn.DataPropertyPrimaryKey
                    };
                }
                else if (myColumn is TMTDataGridViewComboBoxColumn)
                {
                    var myComboBoxColumn = myColumn as TMTDataGridViewComboBoxColumn;
                    return new TMTDataSourceInformation()
                    {
                        DbLabelText = myComboBoxColumn.HeaderText,
                        DbColumnName = myComboBoxColumn.DataPropertyName,
                        DbColumnType = myComboBoxColumn.DataPropertyType,
                        MandatoryColumn = myComboBoxColumn.DataPropertyMandatory,
                        KeyColumn = myComboBoxColumn.DataPropertyPrimaryKey
                    };
                }
                else if (myColumn is TMTDataGridViewCheckBoxColumn)
                {
                    var myCheckBoxColumn = myColumn as TMTDataGridViewCheckBoxColumn;
                    return new TMTDataSourceInformation()
                    {
                        DbLabelText = myCheckBoxColumn.HeaderText,
                        DbColumnName = myCheckBoxColumn.DataPropertyName,
                        DbColumnType = myCheckBoxColumn.DataPropertyType,
                        MandatoryColumn = myCheckBoxColumn.DataPropertyMandatory
                    };
                }
                else if (myColumn is TMTDataGridViewImageColumn)
                {
                    var myImageColumn = myColumn as TMTDataGridViewImageColumn;
                    return new TMTDataSourceInformation()
                    {
                        DbLabelText = myImageColumn.HeaderText,
                        DbColumnName = myImageColumn.DataPropertyName,
                        MandatoryColumn = myImageColumn.DataPropertyMandatory
                    };
                }
                else if (myColumn is TMTDataGridViewLinkColumn)
                {
                    var myLinkColumn = myColumn as TMTDataGridViewLinkColumn;
                    return new TMTDataSourceInformation()
                    {
                        DbLabelText = myLinkColumn.HeaderText,
                        DbColumnName = myLinkColumn.DataPropertyName,
                        DbColumnType = myLinkColumn.DataPropertyType,
                        MandatoryColumn = myLinkColumn.DataPropertyMandatory,
                        KeyColumn = myLinkColumn.DataPropertyPrimaryKey,
                        EditAllowed = myLinkColumn.DataPropertyEditAllowed,
                        IsFuntion = myLinkColumn.DataPropertyIsFuntion
                    };
                }
                else if (myColumn is TMTDataGridViewReadOnlyTextBoxColumn)
                {
                    var myReadOnlyTextBoxColumn = myColumn as TMTDataGridViewReadOnlyTextBoxColumn;
                    return new TMTDataSourceInformation()
                    {
                        DbLabelText = myReadOnlyTextBoxColumn.HeaderText,
                        DbColumnName = myReadOnlyTextBoxColumn.DataPropertyName,
                        DbColumnType = myReadOnlyTextBoxColumn.DataPropertyType,
                        MandatoryColumn = myReadOnlyTextBoxColumn.DataPropertyMandatory,
                        KeyColumn = myReadOnlyTextBoxColumn.DataPropertyPrimaryKey,
                        EditAllowed = myReadOnlyTextBoxColumn.DataPropertyEditAllowed,
                        IsFuntion = myReadOnlyTextBoxColumn.DataPropertyIsFuntion
                    };
                }
                return new TMTDataSourceInformation();
            }
            catch
            {
                throw;
            }
        }

        public static DataTable GetDataSourceTableChanges(this DataTable table, string tableName)
        {
            table.Constraints.Clear();
            DataTable changedData = table.GetChanges(DataRowState.Added | DataRowState.Modified | DataRowState.Deleted);

            if (changedData != null && changedData.Rows.Count > 0)
            {
                List<DataRow> removeList = new List<DataRow>();
                foreach (DataRow row in changedData.Rows)
                {
                    if (row.RowState != DataRowState.Deleted && row.ItemArray.All(i => i == null || string.IsNullOrWhiteSpace(i.ToString())))
                    {
                        removeList.Add(row);
                    }
                    if (row.RowState == DataRowState.Modified)
                    {
                        bool actualyModified = false;
                        foreach (DataColumn col in changedData.Columns)
                        {
                            if (row[col.ColumnName] != null && row[col.ColumnName, DataRowVersion.Original] == null)
                            {
                                actualyModified = true;
                                break;
                            }
                            else if (row[col.ColumnName] == null && row[col.ColumnName, DataRowVersion.Original] != null)
                            {
                                actualyModified = true;
                                break;
                            }
                            else if (row[col.ColumnName] is byte[])
                            {
                                if ((row[col.ColumnName, DataRowVersion.Original] as byte[]) == null)
                                {
                                    actualyModified = true;
                                    break;
                                }
                                if ((row[col.ColumnName] as byte[]).SequenceEqual<byte>(row[col.ColumnName, DataRowVersion.Original] as byte[]) == false)
                                {
                                    actualyModified = true;
                                    break;
                                }
                            }
                            else if (row[col.ColumnName].ToString() != row[col.ColumnName, DataRowVersion.Original].ToString())
                            {
                                actualyModified = true;
                                break;
                            }
                        }
                        if (actualyModified == false)
                        {
                            removeList.Add(row);
                        }
                    }
                }
                foreach (DataRow removeRow in removeList)
                {
                    changedData.Rows.Remove(removeRow);
                }

                changedData.TableName = tableName;
            }

            return changedData;
        }

        public static DataTable GetSearchConditionTable(this TMTSearchDialog searchDialog)
        {
            return GetSearchConditionTable();
        }

        public static DataTable GetSearchConditionTable()
        {
            DataTable searchConditionTable = new DataTable();
            searchConditionTable.TableName = "search_condition";
            searchConditionTable.Columns.Add("COLUMN");
            searchConditionTable.Columns.Add("VALUE");
            searchConditionTable.Columns.Add("TYPE");
            searchConditionTable.Columns.Add("IS_FUNCTION");

            return searchConditionTable;
        }

        public static System.ServiceModel.EndpointAddress GetURL()
        {
            if (Properties.Settings.Default.ServerURL.EndsWith("/") == false)
            {
                Properties.Settings.Default.ServerURL += "/";
            }
            return new System.ServiceModel.EndpointAddress(Properties.Settings.Default.ServerURL + Properties.Settings.Default.WebAPIName);
        }

        public static int? MaxValue(this DataRowCollection rows, string columnName)
        {
            int temp = 0;
            int? maxValue = null;
            foreach (DataRow row in rows)
            {
                if (row[columnName] != null)
                {
                    if (int.TryParse(row[columnName].ToString(), out temp))
                    {
                        if (maxValue.HasValue == false || maxValue < temp)
                        {
                            maxValue = temp;
                        }
                    }
                }
            }
            return maxValue;
        }

        public static string ValueString(this DataGridViewCell cell)
        {
            object oValue = cell.Value;
            if (oValue != null)
            {
                return oValue.ToString();
            }
            return string.Empty;
        }

        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static List<DataGridViewRow> GetSelectedRowList(this DataGridView table)
        {
            List<DataGridViewRow> rowList = new List<DataGridViewRow>();

            var selectedCellsRowIndexes = table.SelectedCells.Cast<DataGridViewCell>().Select(c => c.RowIndex);
            var selectedRowIndexes = new HashSet<int>(selectedCellsRowIndexes.Distinct());
            if (selectedRowIndexes.Count() > 0)
            {
                rowList = table.Rows.Cast<DataGridViewRow>().Where(r => selectedRowIndexes.Contains(r.Index)).ToList();
            }
            return rowList;
        }

        public static string IsSameRowTypeSelected(this DataGridView table, string columnName)
        {
            string selectedType = string.Empty;

            var selectedRowList = table.GetSelectedRowList();
            if (selectedRowList.Count() > 0)
            {
                var statusGroups = selectedRowList.Select(r => r.Cells[columnName].Value).GroupBy(s => s.ToString());
                if (statusGroups.Count() == 1)
                {
                    selectedType = statusGroups.First().Key;
                }
            }

            return selectedType;
        }
    }
}
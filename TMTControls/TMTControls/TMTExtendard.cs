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
        public static List<TMTDataGrid.TMTDataGridView> GetChildDataGridViewList(this Control parentControl)
        {
            List<TMTDataGrid.TMTDataGridView> childTableList = new List<TMTDataGrid.TMTDataGridView>();

            foreach (Control childControl in parentControl.Controls)
            {
                if (childControl is TMTDataGrid.TMTDataGridView)
                {
                    childTableList.Add(childControl as TMTDataGrid.TMTDataGridView);
                }
                childTableList.AddRange(GetChildDataGridViewList(childControl));
            }

            return childTableList;
        }

        public static TMTDataSourceInformation GetDataSourceInformation(this Control myControl)
        {
            if (myControl.Tag == null)
            {
                myControl.Tag = new TMTDataSourceInformation();
            }
            return (myControl.Tag as TMTDataSourceInformation);
        }

        public static TMTDataGridViewDataSourceInformation GetDataSourceInformation(this DataGridViewColumn myColumn)
        {
            if (myColumn.Tag == null)
            {
                myColumn.Tag = new TMTDataGridViewDataSourceInformation();
            }
            return (myColumn.Tag as TMTDataGridViewDataSourceInformation);
        }

        public static DataTable GetDataSourceTableChanges(this DataTable table, string tableName)
        {
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
    }
}
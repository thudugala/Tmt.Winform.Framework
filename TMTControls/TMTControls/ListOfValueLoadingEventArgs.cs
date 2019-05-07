using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace TMT.Controls.WinForms
{
    public class ListOfValueLoadingEventArgs : EventArgs
    {
        public IList<Tuple<string, string>> FilterColumns;

        public ListOfValueLoadingEventArgs()
        {
            SearchConditionList = new List<SearchEntity>();
            HiddenColumns = new HashSet<string>();
            FilterColumns = new List<Tuple<string, string>>();
        }

        public bool Handled { get; set; }

        public ISet<string> HiddenColumns { get; }

        public bool IsValidate { get; set; }

        public bool LimitLoad { get; set; }

        public DataTable ListOfValueDataTable { get; set; }

        public string ListOfValueHeaderText { get; set; }

        public string ListOfValueViewName { get; set; }

        public string PrimaryColumnName { get; set; }

        public Type PrimaryColumnType { get; set; }

        public string PrimaryColumnValue { get; set; }

        public DataGridViewRow Row { get; set; }

        public int RowIndex { get; set; }

        public IList<SearchEntity> SearchConditionList { get; }

        public void FillSearchConditionTable()
        {
            if (IsValidate)
            {
                SearchConditionList.Add(new SearchEntity
                {
                    ColumnName = PrimaryColumnName,
                    Value = PrimaryColumnValue,
                    DataType = PrimaryColumnType,
                    IsFuntion = false
                });
            }

            if (FilterColumns == null || FilterColumns.Count <= 0)
            {
                return;
            }

            foreach (var filterColumn in FilterColumns)
            {
                if (Row.Cells[filterColumn.Item1].Value != null)
                {
                    SearchConditionList.Add(new SearchEntity
                    {
                        ColumnName = filterColumn.Item1,
                        Value = Row.Cells[filterColumn.Item2].ValueString(),
                        DataType = Row.DataGridView.Columns[filterColumn.Item2].ValueType,
                        IsFuntion = false
                    });
                }
            }
        }
    }
}
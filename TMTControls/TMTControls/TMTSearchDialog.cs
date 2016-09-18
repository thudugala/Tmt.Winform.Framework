using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TMTControls
{
    public partial class TMTSearchDialog : TMTDialog
    {
        public TMTSearchDialog()
        {
            InitializeComponent();

            this.EntityList = new List<SearchEntity>();
        }

        public bool IsCaseSensitive
        {
            get
            {
                return checkBoxCaseSensitive.Checked;
            }
        }

        public List<SearchEntity> EntityList { get; private set; }

        public DataTable GetSearchCondition()
        {
            DataTable searchConditionTable = this.GetSearchConditionTable();

            foreach (TMTSearchDialog.SearchEntity sEntity in this.EntityList)
            {
                if (string.IsNullOrWhiteSpace(sEntity.Value) == false)
                {
                    searchConditionTable.Rows.Add(sEntity.ColumnName, sEntity.Value, sEntity.DataType.FullName, false);
                }
            }

            return searchConditionTable;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SearchEntity sEntity;
            foreach (Control dbControl in tableLayoutPanelMain.Controls)
            {
                if (dbControl is TextBox)
                {
                    sEntity = this.EntityList.SingleOrDefault(en => en.ColumnName == dbControl.Name);
                    if (sEntity != null)
                    {
                        sEntity.Value = dbControl.Text.Trim();
                    }
                }
                else if (dbControl is TMTDateTimePickerForSearch)
                {
                    sEntity = this.EntityList.SingleOrDefault(en => en.ColumnName == dbControl.Name);
                    if (sEntity != null)
                    {
                        sEntity.Value = (dbControl as TMTDateTimePickerForSearch).Text.Trim();
                    }
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void TMTSearchDialog_Load(object sender, EventArgs e)
        {
            try
            {
                int vertScrollWidth = SystemInformation.VerticalScrollBarWidth;
                tableLayoutPanelMain.Padding = new Padding(0, 0, vertScrollWidth, 0);

                tableLayoutPanelMain.Controls.Clear();

                tableLayoutPanelMain.RowCount = EntityList.Count() + 1;

                int rowIndex = 0;

                Label propertyLabel;
                TextBox propertyTextBox;
                TMTDateTimePickerForSearch propertyDateTimePicker;
                Control fistControl = null;
                foreach (SearchEntity pi in EntityList)
                {
                    if (pi.DataType == typeof(string) ||
                        pi.DataType == typeof(int) ||
                        pi.DataType == typeof(Int32) ||
                        pi.DataType == typeof(Int64) ||
                        pi.DataType == typeof(decimal) ||
                        pi.DataType == typeof(DateTime))
                    {
                        propertyLabel = new Label();
                        propertyLabel.Dock = DockStyle.Fill;
                        propertyLabel.TextAlign = ContentAlignment.MiddleLeft;
                        propertyLabel.AutoSize = true;
                        propertyLabel.Name = "propertyLabel" + pi.ColumnName;
                        propertyLabel.Text = (pi.Caption.EndsWith(":")) ? pi.Caption : pi.Caption + ":";

                        tableLayoutPanelMain.SetColumn(propertyLabel, 0);
                        tableLayoutPanelMain.SetRow(propertyLabel, rowIndex);
                        tableLayoutPanelMain.Controls.Add(propertyLabel);

                        if (pi.DataType == typeof(string) ||
                            pi.DataType == typeof(int) ||
                            pi.DataType == typeof(Int32) ||
                            pi.DataType == typeof(Int64) ||
                            pi.DataType == typeof(decimal))
                        {
                            propertyTextBox = new TextBox();
                            propertyTextBox.Dock = DockStyle.Fill;
                            propertyTextBox.Name = pi.ColumnName;
                            object value = pi.Value;
                            if (value != null)
                            {
                                propertyTextBox.Text = value.ToString();
                            }
                            tableLayoutPanelMain.SetColumn(propertyTextBox, 1);
                            tableLayoutPanelMain.SetRow(propertyTextBox, rowIndex);
                            tableLayoutPanelMain.Controls.Add(propertyTextBox);

                            if (fistControl == null)
                            {
                                fistControl = propertyTextBox;
                            }
                        }
                        else if (pi.DataType == typeof(DateTime))
                        {
                            propertyDateTimePicker = new TMTDateTimePickerForSearch();
                            propertyDateTimePicker.Dock = DockStyle.Fill;
                            propertyDateTimePicker.Name = pi.ColumnName;
                            object value = pi.Value;
                            if (value != null && string.IsNullOrWhiteSpace(value.ToString()) == false)
                            {
                                propertyDateTimePicker.Text = value.ToString();
                            }
                            tableLayoutPanelMain.SetColumn(propertyDateTimePicker, 1);
                            tableLayoutPanelMain.SetRow(propertyDateTimePicker, rowIndex);
                            tableLayoutPanelMain.Controls.Add(propertyDateTimePicker);

                            if (fistControl == null)
                            {
                                fistControl = propertyDateTimePicker;
                            }
                        }

                        rowIndex++;
                    }
                }

                if (fistControl != null)
                {
                    fistControl.Focus();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public class SearchEntity
        {
            public string Caption { get; set; }

            public string ColumnName { get; set; }

            public Type DataType { get; set; }

            public string Value { get; set; }
        }
    }
}
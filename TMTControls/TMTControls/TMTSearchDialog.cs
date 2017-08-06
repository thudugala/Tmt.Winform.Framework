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

        public bool LimitLoad
        {
            get
            {
                return checkBoxTop100.Checked;
            }
        }

        public event LovLoadingEventHandler SearchLovLoading;

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
                sEntity = this.EntityList.SingleOrDefault(en => en.ColumnName == dbControl.Name);
                if (sEntity != null)
                {
                    if (dbControl is CheckBox)
                    {
                        var dbCheckControl = dbControl as CheckBox;
                        if (dbCheckControl.CheckState != CheckState.Indeterminate)
                        {
                            sEntity.Value = (dbCheckControl.Checked) ? Boolean.TrueString.ToUpper() : Boolean.FalseString.ToUpper();
                        }
                        else
                        {
                            sEntity.Value = string.Empty;
                        }
                    }
                    else
                    {
                        sEntity.Value = dbControl.Text.Trim();
                    }
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        private void TMTSearchDialog_Load(object sender, EventArgs e)
        {
            try
            {
                tableLayoutPanelMain.Padding = new Padding(0, 0, SystemInformation.VerticalScrollBarWidth, 0);

                tableLayoutPanelMain.Controls.Clear();

                tableLayoutPanelMain.RowCount = EntityList.Count() + 1;

                int rowIndex = 0;

                Control fistControl = null;
                foreach (SearchEntity pi in EntityList)
                {
                    var propertyLabel = new Label()
                    {
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleLeft,
                        AutoSize = true,
                        Name = "propertyLabel" + pi.ColumnName,
                        Text = (pi.Caption.EndsWith(":")) ? pi.Caption : pi.Caption + ":"
                    };
                    tableLayoutPanelMain.SetColumn(propertyLabel, 0);
                    tableLayoutPanelMain.SetRow(propertyLabel, rowIndex);
                    tableLayoutPanelMain.Controls.Add(propertyLabel);

                    if (string.IsNullOrWhiteSpace(pi.LovView) == false)
                    {
                        var propertyTextButtonBox = new TMTTextButtonBox()
                        {
                            Dock = DockStyle.Fill,
                            Name = pi.ColumnName,
                            DbColumnName = pi.ColumnName,
                            ConnectedLabel = propertyLabel,
                            LovViewName = pi.LovView
                        };
                        object value = pi.Value;
                        if (value != null)
                        {
                            propertyTextButtonBox.Text = value.ToString();
                        }
                        propertyTextButtonBox.LovLoading += (lovSender, lovArg) =>
                        {
                            SearchLovLoading?.Invoke(lovSender, lovArg);
                        };

                        tableLayoutPanelMain.SetColumn(propertyTextButtonBox, 1);
                        tableLayoutPanelMain.SetRow(propertyTextButtonBox, rowIndex);
                        tableLayoutPanelMain.Controls.Add(propertyTextButtonBox);

                        if (fistControl == null)
                        {
                            fistControl = propertyTextButtonBox;
                        }
                    }
                    else
                    {
                        if (pi.DataType == typeof(string))
                        {
                            if (pi.IsCheckBox)
                            {
                                var propertyCheckBox = new CheckBox()
                                {
                                    Dock = DockStyle.Fill,
                                    Name = pi.ColumnName,
                                    ThreeState = true
                                };
                                object value = pi.Value;
                                if (value != null && string.IsNullOrWhiteSpace(value.ToString()) == false)
                                {
                                    propertyCheckBox.Checked = Boolean.Parse(value.ToString());
                                }
                                else
                                {
                                    propertyCheckBox.CheckState = CheckState.Indeterminate;
                                }
                                tableLayoutPanelMain.SetColumn(propertyCheckBox, 1);
                                tableLayoutPanelMain.SetRow(propertyCheckBox, rowIndex);
                                tableLayoutPanelMain.Controls.Add(propertyCheckBox);

                                if (fistControl == null)
                                {
                                    fistControl = propertyCheckBox;
                                }
                            }
                            else
                            {
                                var propertyTextBox = new TextBox()
                                {
                                    Dock = DockStyle.Fill,
                                    Name = pi.ColumnName
                                };
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
                        }
                        else if (pi.DataType == typeof(int) ||
                            pi.DataType == typeof(Int32) ||
                            pi.DataType == typeof(Int64) ||
                            pi.DataType == typeof(double) ||
                            pi.DataType == typeof(decimal))
                        {
                            var propertyNumberTextBox = new TMTNumberTextBoxForSearch()
                            {
                                Dock = DockStyle.Fill,
                                Name = pi.ColumnName
                            };
                            object value = pi.Value;
                            if (value != null)
                            {
                                propertyNumberTextBox.Text = value.ToString();
                            }
                            tableLayoutPanelMain.SetColumn(propertyNumberTextBox, 1);
                            tableLayoutPanelMain.SetRow(propertyNumberTextBox, rowIndex);
                            tableLayoutPanelMain.Controls.Add(propertyNumberTextBox);

                            if (fistControl == null)
                            {
                                fistControl = propertyNumberTextBox;
                            }
                        }
                        else if (pi.DataType == typeof(DateTime))
                        {
                            var propertyDateTimePicker = new TMTDateTimePickerForSearch()
                            {
                                Dock = DockStyle.Fill,
                                Name = pi.ColumnName
                            };
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
                    }

                    rowIndex++;
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

            public bool IsCheckBox { get; set; }
            public object FalseValue { get; set; }
            public object TrueValue { get; set; }
            public object IndeterminateValue { get; set; }

            public string LovView { get; set; }
        }
    }
}
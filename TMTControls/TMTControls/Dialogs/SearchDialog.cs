using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMT.Controls.WinForms.DatabaseUI;

namespace TMT.Controls.WinForms.Dialogs
{
    public partial class SearchDialog : BaseDialog
    {
        private static string LABEL_DOT = ":";
        private static string PROPERTY_LABEL_PRIFIX = "propertyLabel{0}";

        public SearchDialog()
        {
            InitializeComponent();

            this.EntityList = new List<SearchEntity>();
        }

        public IList<SearchEntity> EntityList { get; }

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

        public Func<ListOfValueLoadingEventArgs, Task> SearchListOfValueLoading { get; set; }

        public void EntityListAddRange(IReadOnlyCollection<SearchEntity> searchEntityList)
        {
            (this.EntityList as List<SearchEntity>).AddRange(searchEntityList);
        }

        private void AddCheckBox(SearchEntity searchEntity, int rowIndex)
        {
            var propertyCheckBox = new CheckBox()
            {
                Dock = DockStyle.Fill,
                Name = searchEntity.ColumnName,
                ThreeState = true
            };

            object value = searchEntity.Value;
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
        }

        private void AddDateTimePickerForSearch(SearchEntity searchEntity, int rowIndex)
        {
            var propertyDateTimePicker = new DateTimePickerForSearch()
            {
                Dock = DockStyle.Fill,
                Name = searchEntity.ColumnName
            };

            object value = searchEntity.Value;
            if (value != null && string.IsNullOrWhiteSpace(value.ToString()) == false)
            {
                propertyDateTimePicker.Text = value.ToString();
            }
            tableLayoutPanelMain.SetColumn(propertyDateTimePicker, 1);
            tableLayoutPanelMain.SetRow(propertyDateTimePicker, rowIndex);
            tableLayoutPanelMain.Controls.Add(propertyDateTimePicker);
        }

        private Label AddLabel(SearchEntity searchEntity, int rowIndex)
        {
            var propertyLabel = new Label()
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = true,
                Name = string.Format(PROPERTY_LABEL_PRIFIX, searchEntity.ColumnName),
                Text = (searchEntity.Caption.EndsWith(LABEL_DOT, StringComparison.Ordinal)) ? searchEntity.Caption : searchEntity.Caption + LABEL_DOT
            };

            tableLayoutPanelMain.SetColumn(propertyLabel, 0);
            tableLayoutPanelMain.SetRow(propertyLabel, rowIndex);
            tableLayoutPanelMain.Controls.Add(propertyLabel);

            return propertyLabel;
        }

        private void AddListOfValueTextBox(SearchEntity searchEntity, int rowIndex, Label propertyLabel)
        {
            var propertyTextButtonBox = new DbTextButtonBox()
            {
                Dock = DockStyle.Fill,
                Name = searchEntity.ColumnName,
                DbColumnName = searchEntity.ColumnName,
                ConnectedLabel = propertyLabel,
                ListOfValueViewName = searchEntity.ListOfValueView
            };

            object value = searchEntity.Value;
            if (value != null)
            {
                propertyTextButtonBox.Text = value.ToString();
            }
            propertyTextButtonBox.ListOfValueLoading += PropertyTextButtonBox_ListOfValueLoading;

            tableLayoutPanelMain.SetColumn(propertyTextButtonBox, 1);
            tableLayoutPanelMain.SetRow(propertyTextButtonBox, rowIndex);
            tableLayoutPanelMain.Controls.Add(propertyTextButtonBox);
        }

        private void AddNumberTextBoxForSearch(SearchEntity searchEntity, int rowIndex)
        {
            var propertyNumberTextBox = new NumberTextBoxForSearch()
            {
                Dock = DockStyle.Fill,
                Name = searchEntity.ColumnName
            };

            object value = searchEntity.Value;
            if (value != null)
            {
                propertyNumberTextBox.Text = value.ToString();
            }
            tableLayoutPanelMain.SetColumn(propertyNumberTextBox, 1);
            tableLayoutPanelMain.SetRow(propertyNumberTextBox, rowIndex);
            tableLayoutPanelMain.Controls.Add(propertyNumberTextBox);
        }

        private void AddTextBox(SearchEntity searchEntity, int rowIndex)
        {
            var propertyTextBox = new TextBox()
            {
                Dock = DockStyle.Fill,
                Name = searchEntity.ColumnName
            };

            object value = searchEntity.Value;
            if (value != null)
            {
                propertyTextBox.Text = value.ToString();
            }
            tableLayoutPanelMain.SetColumn(propertyTextBox, 1);
            tableLayoutPanelMain.SetRow(propertyTextBox, rowIndex);
            tableLayoutPanelMain.Controls.Add(propertyTextBox);
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            foreach (Control dbControl in tableLayoutPanelMain.Controls)
            {
                var sEntity = this.EntityList.SingleOrDefault(en => en.ColumnName == dbControl.Name);
                if (sEntity != null)
                {
                    if (dbControl is CheckBox dbCheckControl)
                    {
                        if (dbCheckControl.CheckState != CheckState.Indeterminate)
                        {
                            sEntity.Value = (dbCheckControl.Checked) ? Boolean.TrueString.ToUpper(CultureInfo.InvariantCulture) : Boolean.FalseString.ToUpper(CultureInfo.InvariantCulture);
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

        private void PropertyTextButtonBox_ListOfValueLoading(object sender, ListOfValueLoadingEventArgs e)
        {
            SearchListOfValueLoading(e).Wait();
        }

        private void SearchDialog_Load(object sender, EventArgs e)
        {
            try
            {
                this.Image = IconChar.Search.ToBitmap(72, Color.FromArgb(66, 134, 244));
                                
                tableLayoutPanelMain.Padding = new Padding(0, 0, SystemInformation.VerticalScrollBarWidth, 0);

                tableLayoutPanelMain.Controls.Clear();

                tableLayoutPanelMain.RowCount = EntityList.Count() + 1;

                int rowIndex = 0;

                foreach (SearchEntity pi in EntityList)
                {
                    if (string.IsNullOrWhiteSpace(pi.Caption))
                    {
                        continue;
                    }

                    var propertyLabel = this.AddLabel(pi, rowIndex);

                    if (string.IsNullOrWhiteSpace(pi.ListOfValueView) == false)
                    {
                        this.AddListOfValueTextBox(pi, rowIndex, propertyLabel);
                    }
                    else
                    {
                        if (pi.DataType == typeof(string))
                        {
                            if (pi.IsCheckBox)
                            {
                                this.AddCheckBox(pi, rowIndex);
                            }
                            else
                            {
                                this.AddTextBox(pi, rowIndex);
                            }
                        }
                        else if (pi.DataType == typeof(int) ||
                            pi.DataType == typeof(Int32) ||
                            pi.DataType == typeof(Int64) ||
                            pi.DataType == typeof(double) ||
                            pi.DataType == typeof(decimal))
                        {
                            this.AddNumberTextBoxForSearch(pi, rowIndex);
                        }
                        else if (pi.DataType == typeof(DateTime))
                        {
                            this.AddDateTimePickerForSearch(pi, rowIndex);
                        }
                    }

                    rowIndex++;
                }

                var fistControl = tableLayoutPanelMain.GetControlFromPosition(1, 0);
                if (fistControl != null)
                {
                    fistControl.Focus();
                }
            }
            catch
            {
                throw;
            }
        }

        private void BaseButtonClear_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control dbControl in tableLayoutPanelMain.Controls)
                {
                    var sEntity = this.EntityList.SingleOrDefault(en => en.ColumnName == dbControl.Name);
                    if (sEntity != null)
                    {
                        if (dbControl is CheckBox dbCheckControl)
                        {
                            dbCheckControl.CheckState = CheckState.Indeterminate;
                        }
                        else
                        {
                            dbControl.Text = string.Empty;
                        }
                    }
                }
            }
            catch { }
        }
    }
}
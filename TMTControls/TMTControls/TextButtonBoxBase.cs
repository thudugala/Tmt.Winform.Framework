using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMT.Controls.WinForms.DatabaseUI;
using TMT.Controls.WinForms.DataGrid;
using TMT.Controls.WinForms.Dialogs;

namespace TMT.Controls.WinForms
{
    public enum TextInputType
    {
        Text,
        Number,
        NumberWithSearch
    }

    [ToolboxBitmap(typeof(UserControl))]
    [DefaultEvent(nameof(TextChanged))]
    public partial class TextButtonBoxBase : UserControl
    {
        private ToolStripProgressBar _controlProgressBar;
        private bool _DoValidate = true;
        private ListOfValueDialog _lovDialog;
        private HashSet<char> allowedKeySet = new HashSet<char>() { '.' };
        private HashSet<char> allowedKeySetForSearch = new HashSet<char>() { '.', '<', '=', '>', '!', ';' };

        public TextButtonBoxBase()
        {
            InitializeComponent();

            this.SuspendLayout();

            InnerTextBox.GotFocus += (sender, e) => this.OnGotFocus(e);
            this.KeyInputType = TextInputType.Text;

            this.ResumeLayout(false);
        }

        public override Color BackColor
        {
            get
            {
                return InnerTextBox.BackColor;
            }
            set
            {
                InnerTextBox.BackColor = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ButtonHeight
        {
            get
            {
                return buttonOK.Height;
            }
            set
            {
                buttonOK.Height = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ButtonWidth
        {
            get
            {
                return buttonOK.Width;
            }
            set
            {
                buttonOK.Width = value;
            }
        }

        [Category("Behavior"), DefaultValue(CharacterCasing.Normal)]
        public CharacterCasing CharacterCasing
        {
            get
            {
                return this.InnerTextBox.CharacterCasing;
            }
            set
            {
                this.InnerTextBox.CharacterCasing = value;
            }
        }

        public override Color ForeColor
        {
            get
            {
                return InnerTextBox.ForeColor;
            }
            set
            {
                InnerTextBox.ForeColor = value;
            }
        }

        [Category("Design"), DefaultValue(TextInputType.Text)]
        public TextInputType KeyInputType { get; set; }

        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Localizable(true)]
        [Bindable(true)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get
            {
                return InnerTextBox.Text;
            }
            set
            {
                InnerTextBox.Text = value;
            }
        }

        public HorizontalAlignment TextAlign
        {
            get
            {
                return InnerTextBox.TextAlign;
            }
            set
            {
                InnerTextBox.TextAlign = value;
            }
        }

        public void ClearUndo()
        {
            InnerTextBox.ClearUndo();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == Keys.F8)
                {
                    buttonOK.PerformClick();
                }
            }
            catch { }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private async void Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (this is DbTextButtonBoxEditingControl columEditControl)
                {
                    await this.ColumnListOfValue(columEditControl);
                }
                else if (this is DbTextButtonBox dbTextButton)
                {
                    await this.FieldListOfValue(dbTextButton);
                }
                if (InnerTextBox.Focused == false)
                {
                    InnerTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.MSG_LOV_SetError);
            }
        }

        private async Task ColumnListOfValue(DbTextButtonBoxEditingControl sender)
        {
            try
            {
                this._DoValidate = false;

                var gridview = sender.EditingControlDataGridView as DbDataGridView;

                if (this._lovDialog == null)
                {
                    this._lovDialog = new ListOfValueDialog();
                }
                if (this._lovDialog.Visible)
                {
                    return;
                }

                this.SetProgressBarVisibile(true);

                var currentColumn = gridview.CurrentCell.OwningColumn as DbTextButtonBoxColumn;

                var lovEvent = new ListOfValueLoadingEventArgs()
                {
                    RowIndex = gridview.CurrentCell.RowIndex,
                    IsValidate = false,
                    PrimaryColumnName = currentColumn.DataPropertyName,
                    PrimaryColumnType = currentColumn.ValueType,
                    ListOfValueHeaderText = currentColumn.HeaderText,
                    ListOfValueViewName = currentColumn.ListOfValueViewName
                };
                lovEvent.PrimaryColumnValue = gridview.CurrentCell.ValueString();
                lovEvent.Row = gridview.Rows[lovEvent.RowIndex];

                await Task.Run(async () => await gridview.GetListOfValueSelectedRow(lovEvent));

                this._lovDialog.Text = lovEvent.ListOfValueHeaderText;
                this._lovDialog.SetDataSourceTable(lovEvent.ListOfValueDataTable);

                if (this._lovDialog.ShowDialog(this) == DialogResult.OK)
                {
                    var lovLoaded = new ListOfValueLoadedEventArgs(this._lovDialog.SelectedRow)
                    {
                        IsValidate = lovEvent.IsValidate,
                        RowIndex = lovEvent.RowIndex,
                        PrimaryColumnName = lovEvent.PrimaryColumnName,
                        ListOfValueViewName = lovEvent.ListOfValueViewName
                    };

                    gridview.CurrentCell.Value = lovLoaded.SelectedRow[lovEvent.PrimaryColumnName].ToString();
                    gridview.CurrentCell.Tag = gridview.CurrentCell.Value;

                    gridview.SetListOfValueSelectedRow(lovLoaded);

                    gridview.CurrentCell.Selected = true;
                    gridview.CurrentCell.Style.BackColor = Color.Empty;
                }
                gridview.EndEdit();
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.MSG_LOV_SetError);
            }
            finally
            {
                this.SetProgressBarVisibile(false);
                this._DoValidate = true;
            }
        }

        private async Task ColumnValidate(DbTextButtonBoxEditingControl sender)
        {
            try
            {
                var gridview = sender.EditingControlDataGridView as DbDataGridView;
                var currentColumn = gridview.CurrentCell.OwningColumn as DbTextButtonBoxColumn;

                string sValue = string.Empty;
                if (gridview.CurrentCell.Value != null)
                {
                    sValue = gridview.CurrentCell.Value.ToString();

                    object oldOValue = gridview.CurrentCell.Tag;
                    if (oldOValue != null && sValue == oldOValue.ToString())
                    {
                        return;
                    }
                    else
                    {
                        gridview.CurrentCell.Tag = sValue;
                    }
                }

                if (string.IsNullOrWhiteSpace(sValue) == false)
                {
                    var lovEvent = new ListOfValueLoadingEventArgs()
                    {
                        RowIndex = gridview.CurrentCell.RowIndex,
                        IsValidate = true,
                        PrimaryColumnName = currentColumn.DataPropertyName,
                        PrimaryColumnType = currentColumn.ValueType,
                        ListOfValueViewName = currentColumn.ListOfValueViewName
                    };

                    lovEvent.PrimaryColumnValue = gridview.CurrentCell.ValueString();
                    lovEvent.Row = gridview.Rows[lovEvent.RowIndex];

                    await Task.Run(async () => await gridview.GetListOfValueSelectedRow(lovEvent));

                    if (lovEvent.ListOfValueDataTable != null)
                    {
                        bool dataFound = (lovEvent.ListOfValueDataTable.Rows.Count == 1);

                        if (dataFound)
                        {
                            var lovLoaded = new ListOfValueLoadedEventArgs()
                            {
                                IsValidate = lovEvent.IsValidate,
                                RowIndex = lovEvent.RowIndex,
                                PrimaryColumnName = lovEvent.PrimaryColumnName,
                                ListOfValueViewName = lovEvent.ListOfValueViewName
                            };
                            var row = lovEvent.ListOfValueDataTable.Rows[0];
                            foreach (DataColumn col in lovEvent.ListOfValueDataTable.Columns)
                            {
                                lovLoaded.SelectedRow.Add(col.ColumnName, row[col]);
                            }
                            gridview.CurrentCell.Tag = gridview.CurrentCell.Value;

                            gridview.SetListOfValueSelectedRow(lovLoaded);
                        }

                        gridview.CurrentCell.Style.BackColor = (dataFound) ? Color.Empty : Color.Red;
                    }
                    else
                    {
                        gridview.CurrentCell.Style.BackColor = Color.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.ERROR_CellValidation);
            }
        }

        private async Task FieldListOfValue(DbTextButtonBox dbTextButton)
        {
            try
            {
                this._DoValidate = false;

                if (this._lovDialog == null)
                {
                    this._lovDialog = new ListOfValueDialog();
                }
                if (this._lovDialog.Visible)
                {
                    return;
                }
                this.SetProgressBarVisibile(true);

                var lovEvent = new ListOfValueLoadingEventArgs()
                {
                    IsValidate = false,
                    PrimaryColumnName = dbTextButton.DbColumnName,
                    PrimaryColumnType = dbTextButton.GetDbColumnSystemType(),
                    PrimaryColumnValue = dbTextButton.Text,
                    ListOfValueHeaderText = dbTextButton.ConnectedLabel.Text,
                    ListOfValueViewName = dbTextButton.ListOfValueViewName
                };

                await Task.Run(async () => await dbTextButton.GetListOfValueSelectedRow(lovEvent));

                this._lovDialog.Text = lovEvent.ListOfValueHeaderText;
                this._lovDialog.SetDataSourceTable(lovEvent.ListOfValueDataTable);

                if (this._lovDialog.ShowDialog(this) == DialogResult.OK)
                {
                    var lovLoaded = new ListOfValueLoadedEventArgs(this._lovDialog.SelectedRow)
                    {
                        IsValidate = lovEvent.IsValidate,
                        PrimaryColumnName = lovEvent.PrimaryColumnName,
                        ListOfValueViewName = lovEvent.ListOfValueViewName
                    };

                    dbTextButton.Text = lovLoaded.SelectedRow[lovEvent.PrimaryColumnName].ToString();
                    dbTextButton.ListOfValueText = dbTextButton.Text;

                    dbTextButton.CausesValidation = true;
                    dbTextButton.Focus();

                    dbTextButton.SetListOfValueSelectedRow(lovLoaded);
                }
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.MSG_LOV_SetError);
            }
            finally
            {
                this.SetProgressBarVisibile(false);
                this._DoValidate = true;
            }
        }

        private async Task FieldValidate(DbTextButtonBox dbTextButton)
        {
            try
            {
                if (dbTextButton.ListOfValueText != dbTextButton.Text)
                {
                    var lovEvent = new ListOfValueLoadingEventArgs()
                    {
                        IsValidate = true,
                        PrimaryColumnName = dbTextButton.DbColumnName,
                        PrimaryColumnType = dbTextButton.GetDbColumnSystemType(),
                        PrimaryColumnValue = dbTextButton.Text,
                        ListOfValueViewName = dbTextButton.ListOfValueViewName
                    };

                    await Task.Run(async () => await dbTextButton.GetListOfValueSelectedRow(lovEvent));

                    bool dataFound = (lovEvent.ListOfValueDataTable != null && lovEvent.ListOfValueDataTable.Rows.Count == 1);
                    if (dataFound)
                    {
                        dbTextButton.ListOfValueText = dbTextButton.Text;

                        DataRow selectedDataRow = lovEvent.ListOfValueDataTable.Rows[0];
                        var selectedRow = new Dictionary<string, object>();
                        foreach (DataColumn col in lovEvent.ListOfValueDataTable.Columns)
                        {
                            selectedRow.Add(col.ColumnName, selectedDataRow[col]);
                        }

                        var lovLoaded = new ListOfValueLoadedEventArgs(selectedRow)
                        {
                            IsValidate = lovEvent.IsValidate,
                            PrimaryColumnName = lovEvent.PrimaryColumnName,
                            ListOfValueViewName = lovEvent.ListOfValueViewName
                        };

                        dbTextButton.SetListOfValueSelectedRow(lovLoaded);
                    }

                    this.BackColor = (dataFound) ? Color.Empty : Color.Red;
                }
                else
                {
                    this.BackColor = Color.Empty;
                }
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(this, ex, Properties.Resources.MSG_LOV_SetError);
            }
        }

        private void InnerTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.KeyInputType == TextInputType.NumberWithSearch)
            {
                if (char.IsControl(e.KeyChar) == false &&
                    char.IsDigit(e.KeyChar) == false &&
                    this.allowedKeySetForSearch.Contains(e.KeyChar) == false)
                {
                    e.Handled = true;
                }
            }
            else if (this.KeyInputType == TextInputType.Number)
            {
                if (char.IsControl(e.KeyChar) == false &&
                    char.IsDigit(e.KeyChar) == false &&
                    this.allowedKeySet.Contains(e.KeyChar) == false)
                {
                    e.Handled = true;
                }
            }
        }

        private void InnerTextBox_TextChanged(object sender, EventArgs e)
        {
            this.OnTextChanged(e);
        }

        private async void InnerTextBox_Validated(object sender, EventArgs e)
        {
            if (this._DoValidate)
            {
                this.OnValidated(e);

                if (this is DbTextButtonBoxEditingControl columEditControl)
                {
                    await this.ColumnValidate(columEditControl);
                }
                else if (this is DbTextButtonBox dbTextButton)
                {
                    await this.FieldValidate(dbTextButton);
                }
            }
        }

        private void SetProgressBarVisibile(bool visibility)
        {
            if (this._controlProgressBar == null)
            {
                this._controlProgressBar = this.FindParentBaseUserControl()?.progressBarBase;
            }

            if (this._controlProgressBar != null)
            {
                this._controlProgressBar.Visible = visibility;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMTControls.TMTDatabaseUI;
using TMTControls.TMTDataGrid;
using TMTControls.TMTDialogs;

namespace TMTControls
{
    [ToolboxBitmap(typeof(UserControl))]
    [DefaultEvent("TextChanged")]
    public partial class TMTTextButtonBoxBase : UserControl
    {
        private TMTListOfValueDialog _lovDialog;
        private HashSet<char> allowedKeySetForSearch = new HashSet<char>() { '.', '<', '=', '>', '!', ';' };
        private HashSet<char> allowedKeySet = new HashSet<char>() { '.' };

        private bool _DoValidate = true;

        public TMTTextButtonBoxBase()
        {
            InitializeComponent();

            this.SuspendLayout();

            InnerTextBox.GotFocus += (sender, e) => this.OnGotFocus(e);
            this.KeyInputType = TextInputType.Text;

            this.ResumeLayout(false);
        }

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

        [Category("Design"), DefaultValue(TextInputType.Text)]
        public TextInputType KeyInputType { get; set; }

        [Category("Design")]
        public ProgressBar ControlProgressBar { get; set; }

        public void ClearUndo()
        {
            InnerTextBox.ClearUndo();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (this is TMTDataGridViewTextButtonBoxEditingControl columEditControl)
            {
                this.ColumnListOfValue(columEditControl);
            }
            else if (this is TMTTextButtonBox tmtTextButton)
            {
                this.FieldListOfValue(tmtTextButton);
            }
            if (InnerTextBox.Focused == false)
            {
                InnerTextBox.Focus();
            }
        }

        private void InnerTextBox_Validated(object sender, EventArgs e)
        {
            if (this._DoValidate)
            {
                this.OnValidated(e);

                if (this is TMTDataGridViewTextButtonBoxEditingControl columEditControl)
                {
                    this.ColumnValidate(columEditControl);
                }
                else if (this is TMTTextButtonBox tmtTextButton)
                {
                    this.FieldValidate(tmtTextButton);
                }
            }
        }

        private async void FieldListOfValue(TMTTextButtonBox tmtTextButton)
        {
            try
            {
                this._DoValidate = false;

                if (this._lovDialog == null)
                {
                    this._lovDialog = new TMTListOfValueDialog();
                }
                if (ControlProgressBar != null)
                {
                    ControlProgressBar.Visible = true;
                }

                var lovEvent = new ListOfValueLoadingEventArgs()
                {
                    IsValidate = false,
                    PrimaryColumnName = tmtTextButton.DbColumnName,
                    PrimaryColumnType = tmtTextButton.GetDbColumnSystemType().FullName,
                    PrimaryColumnValue = tmtTextButton.Text,
                    ListOfValueHeaderText = tmtTextButton.ConnectedLabel.Text,
                    SearchConditionTable = TMTExtend.GetSearchConditionTable(),
                    ListOfValueViewName = tmtTextButton.ListOfValueViewName
                };

                await Task.Run(() => tmtTextButton.GetListOfValueSelectedRow(lovEvent));

                this._lovDialog.HeaderLabel = lovEvent.ListOfValueHeaderText;
                this._lovDialog.SetDataSourceTable(lovEvent.ListOfValueDataTable);

                if (this._lovDialog.ShowDialog(this) == DialogResult.OK)
                {
                    ListOfValueLoadedEventArgs lovLoaded = new ListOfValueLoadedEventArgs(this._lovDialog.SelectedRow)
                    {
                        IsValidate = lovEvent.IsValidate,
                        PrimaryColumnName = lovEvent.PrimaryColumnName,
                        ListOfValueViewName = lovEvent.ListOfValueViewName
                    };

                    tmtTextButton.Text = lovLoaded.SelectedRow[lovEvent.PrimaryColumnName].ToString();
                    tmtTextButton.ListOfValueText = tmtTextButton.Text;

                    tmtTextButton.CausesValidation = true;
                    tmtTextButton.Focus();

                    tmtTextButton.SetListOfValueSelectedRow(lovLoaded);
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.MSG_LOV_SetError);
            }
            finally
            {
                if (ControlProgressBar != null)
                {
                    ControlProgressBar.Visible = false;
                }
                this._DoValidate = true;
            }
        }

        private async void FieldValidate(TMTTextButtonBox tmtTextButton)
        {
            try
            {
                if (tmtTextButton.ListOfValueText != tmtTextButton.Text)
                {
                    ListOfValueLoadingEventArgs lovEvent = new ListOfValueLoadingEventArgs()
                    {
                        IsValidate = true,
                        PrimaryColumnName = tmtTextButton.DbColumnName,
                        PrimaryColumnType = tmtTextButton.GetDbColumnSystemType().FullName,
                        PrimaryColumnValue = tmtTextButton.Text,
                        SearchConditionTable = TMTExtend.GetSearchConditionTable(),
                        ListOfValueViewName = tmtTextButton.ListOfValueViewName
                    };

                    await Task.Run(() => tmtTextButton.GetListOfValueSelectedRow(lovEvent));

                    bool dataFound = (lovEvent.ListOfValueDataTable != null && lovEvent.ListOfValueDataTable.Rows.Count == 1);
                    if (dataFound)
                    {
                        tmtTextButton.ListOfValueText = tmtTextButton.Text;

                        DataRow selectedDataRow = lovEvent.ListOfValueDataTable.Rows[0];
                        Dictionary<string, object> selectedRow = new Dictionary<string, object>();
                        foreach (DataColumn col in lovEvent.ListOfValueDataTable.Columns)
                        {
                            selectedRow.Add(col.ColumnName, selectedDataRow[col]);
                        }

                        ListOfValueLoadedEventArgs lovLoaded = new ListOfValueLoadedEventArgs(selectedRow)
                        {
                            IsValidate = lovEvent.IsValidate,
                            PrimaryColumnName = lovEvent.PrimaryColumnName,
                            ListOfValueViewName = lovEvent.ListOfValueViewName
                        };

                        tmtTextButton.SetListOfValueSelectedRow(lovLoaded);
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
                TMTErrorDialog.Show(this, ex, Properties.Resources.MSG_LOV_SetError);
            }
        }

        private async void ColumnListOfValue(TMTDataGridViewTextButtonBoxEditingControl sender)
        {
            try
            {
                this._DoValidate = false;

                var gridview = sender.EditingControlDataGridView as TMTDataGridView;

                if (this._lovDialog == null)
                {
                    this._lovDialog = new TMTListOfValueDialog();
                }
                if (ControlProgressBar != null)
                {
                    ControlProgressBar.Visible = true;
                }

                var currentColumn = gridview.CurrentCell.OwningColumn as TMTDataGridViewTextButtonBoxColumn;

                var lovEvent = new ListOfValueLoadingEventArgs()
                {
                    RowIndex = gridview.CurrentCell.RowIndex,
                    IsValidate = false,
                    PrimaryColumnName = currentColumn.DataPropertyName,
                    PrimaryColumnType = currentColumn.ValueType.FullName,
                    ListOfValueHeaderText = currentColumn.HeaderText,
                    SearchConditionTable = TMTExtend.GetSearchConditionTable(),
                    ListOfValueViewName = currentColumn.ListOfValueViewName
                };
                lovEvent.PrimaryColumnValue = gridview.CurrentCell.ValueString();
                lovEvent.Row = gridview.Rows[lovEvent.RowIndex];

                await Task.Run(() => gridview.GetListOfValueSelectedRow(lovEvent));

                this._lovDialog.HeaderLabel = lovEvent.ListOfValueHeaderText;
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
                TMTErrorDialog.Show(this, ex, Properties.Resources.MSG_LOV_SetError);
            }
            finally
            {
                if (ControlProgressBar != null)
                {
                    ControlProgressBar.Visible = false;
                }
                this._DoValidate = true;
            }
        }

        private async void ColumnValidate(TMTDataGridViewTextButtonBoxEditingControl sender)
        {
            try
            {
                var gridview = sender.EditingControlDataGridView as TMTDataGridView;
                var currentColumn = gridview.CurrentCell.OwningColumn as TMTDataGridViewTextButtonBoxColumn;

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
                        PrimaryColumnType = currentColumn.ValueType.FullName,
                        SearchConditionTable = TMTExtend.GetSearchConditionTable(),
                        ListOfValueViewName = currentColumn.ListOfValueViewName
                    };

                    lovEvent.PrimaryColumnValue = gridview.CurrentCell.ValueString();
                    lovEvent.Row = gridview.Rows[lovEvent.RowIndex];

                    await Task.Run(() => gridview.GetListOfValueSelectedRow(lovEvent));

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
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_CellValidation);
            }
        }

        private void InnerTextBox_TextChanged(object sender, EventArgs e)
        {
            this.OnTextChanged(e);
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
    }

    public enum TextInputType
    {
        Text,
        Number,
        NumberWithSearch
    }
}
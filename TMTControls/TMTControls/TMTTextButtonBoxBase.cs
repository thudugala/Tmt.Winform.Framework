using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TMTControls.TMTDataGrid;

namespace TMTControls
{
    [ToolboxBitmap(typeof(UserControl))]
    [DefaultEvent("TextChanged")]
    public partial class TMTTextButtonBoxBase : UserControl
    {
        private TMTLOVDialog _lovDialog;
        private HashSet<char> allowedKeySetForSearch = new HashSet<char>() { '.', '<', '=', '>', '!', ';' };
        private HashSet<char> allowedKeySet = new HashSet<char>() { '.' };

        public TMTTextButtonBoxBase()
        {
            InitializeComponent();

            this.SuspendLayout();

            InnerTextBox.GotFocus += InnerTextBox_GotFocus;
            this.KeyInputType = TextInputType.Text;

            this.ResumeLayout(false);
        }

        public void InnerTextBox_GotFocus(object sender, EventArgs e)
        {
            this.OnGotFocus(e);
        }

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

        public void ClearUndo()
        {
            InnerTextBox.ClearUndo();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (this is TMTDataGridViewTextButtonBoxEditingControl columEditControl)
            {
                this.ColumnLov(columEditControl);
            }
            else if (this is TMTTextButtonBox tmtTextButton)
            {
                this.FieldLov(tmtTextButton);
            }
            if (InnerTextBox.Focused == false)
            {
                InnerTextBox.Focus();
            }
        }

        private void InnerTextBox_Validated(object sender, EventArgs e)
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

        private void FieldLov(TMTTextButtonBox tmtTextButton)
        {
            try
            {
                if (this._lovDialog == null)
                {
                    this._lovDialog = new TMTLOVDialog();
                }

                LovLoadingEventArgs lovEvent = new LovLoadingEventArgs()
                {
                    IsValidate = false,
                    PrimaryColumnName = tmtTextButton.DbColumnName,
                    PrimaryColumnType = tmtTextButton.GetDataSourceInformation().GetDbColumnType().FullName,
                    PrimaryColumnValue = tmtTextButton.Text,
                    LovHeaderText = tmtTextButton.ConnectedLabel.Text,
                    SearchConditionTable = TMTExtendard.GetSearchConditionTable(),
                    LovViewName = tmtTextButton.LovViewName
                };

                tmtTextButton.GetLovSelectedRow(lovEvent);

                this._lovDialog.HeaderLabel = lovEvent.LovHeaderText;
                this._lovDialog.SetDataSourceTable(lovEvent.LovDataTable);

                if (this._lovDialog.ShowDialog(this) == DialogResult.OK)
                {
                    LovLoadedEventArgs lovLoaded = new LovLoadedEventArgs()
                    {
                        IsValidate = lovEvent.IsValidate,
                        SelectedRow = this._lovDialog.SelectedRow,
                        PrimaryColumnName = lovEvent.PrimaryColumnName,
                        LovViewName = lovEvent.LovViewName
                    };
                    tmtTextButton.Text = lovLoaded.SelectedRow[lovEvent.PrimaryColumnName].ToString();
                    tmtTextButton.LovText = tmtTextButton.Text;

                    tmtTextButton.CausesValidation = true;
                    tmtTextButton.Focus();

                    tmtTextButton.SetLovSelectedRow(lovLoaded);
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.MSG_LOV_SetError);
            }
        }

        private void FieldValidate(TMTTextButtonBox tmtTextButton)
        {
            try
            {
                if (tmtTextButton.LovText != tmtTextButton.Text)
                {
                    LovLoadingEventArgs lovEvent = new LovLoadingEventArgs()
                    {
                        IsValidate = true,
                        PrimaryColumnName = tmtTextButton.DbColumnName,
                        PrimaryColumnType = tmtTextButton.GetDataSourceInformation().GetDbColumnType().FullName,
                        PrimaryColumnValue = tmtTextButton.Text,
                        SearchConditionTable = TMTExtendard.GetSearchConditionTable(),
                        LovViewName = tmtTextButton.LovViewName
                    };

                    tmtTextButton.GetLovSelectedRow(lovEvent);

                    bool dataFound = (lovEvent.LovDataTable != null && lovEvent.LovDataTable.Rows.Count == 1);
                    if (dataFound)
                    {
                        tmtTextButton.LovText = tmtTextButton.Text;

                        LovLoadedEventArgs lovLoaded = new LovLoadedEventArgs()
                        {
                            IsValidate = lovEvent.IsValidate,
                            PrimaryColumnName = lovEvent.PrimaryColumnName,
                            LovViewName = lovEvent.LovViewName
                        };

                        DataRow selectedDataRow = lovEvent.LovDataTable.Rows[0];
                        Dictionary<string, Object> selectedRow = new Dictionary<string, object>();
                        foreach (DataColumn col in lovEvent.LovDataTable.Columns)
                        {
                            selectedRow.Add(col.ColumnName, selectedDataRow[col]);
                        }
                        lovLoaded.SelectedRow = selectedRow;

                        tmtTextButton.SetLovSelectedRow(lovLoaded);
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

        private void ColumnLov(TMTDataGridViewTextButtonBoxEditingControl sender)
        {
            try
            {
                var gridview = sender.EditingControlDataGridView as TMTDataGridView;

                if (this._lovDialog == null)
                {
                    this._lovDialog = new TMTLOVDialog();
                }

                var currentColumn = gridview.CurrentCell.OwningColumn as TMTDataGridViewTextButtonBoxColumn;

                LovLoadingEventArgs lovEvent = new LovLoadingEventArgs()
                {
                    RowIndex = gridview.CurrentCell.RowIndex,
                    IsValidate = false,
                    PrimaryColumnName = currentColumn.DataPropertyName,
                    PrimaryColumnType = currentColumn.GetDataSourceInformation().GetDbColumnType().FullName,
                    LovHeaderText = currentColumn.HeaderText,
                    SearchConditionTable = TMTExtendard.GetSearchConditionTable(),
                    LovViewName = currentColumn.LovViewName
                };
                if (gridview.CurrentCell.Value != null)
                {
                    lovEvent.PrimaryColumnValue = gridview.CurrentCell.Value.ToString();
                }
                lovEvent.Row = gridview.Rows[lovEvent.RowIndex];

                gridview.GetLovSelectedRow(lovEvent);

                this._lovDialog.HeaderLabel = lovEvent.LovHeaderText;
                this._lovDialog.SetDataSourceTable(lovEvent.LovDataTable);

                if (this._lovDialog.ShowDialog(this) == DialogResult.OK)
                {
                    DataGridViewRow currectRow = gridview.Rows[lovEvent.RowIndex];

                    LovLoadedEventArgs lovLoaded = new LovLoadedEventArgs()
                    {
                        IsValidate = lovEvent.IsValidate,
                        RowIndex = lovEvent.RowIndex,
                        PrimaryColumnName = lovEvent.PrimaryColumnName,
                        SelectedRow = this._lovDialog.SelectedRow,
                        LovViewName = lovEvent.LovViewName
                    };

                    gridview.CurrentCell.Value = lovLoaded.SelectedRow[lovEvent.PrimaryColumnName].ToString();
                    gridview.CurrentCell.Tag = gridview.CurrentCell.Value;

                    gridview.SetLovSelectedRow(lovLoaded);

                    gridview.CurrentCell.Selected = true;
                    gridview.CurrentCell.Style.BackColor = Color.Empty;
                }
                gridview.EndEdit();
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.MSG_LOV_SetError);
            }
        }

        private void ColumnValidate(TMTDataGridViewTextButtonBoxEditingControl sender)
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
                    LovLoadingEventArgs lovEvent = new LovLoadingEventArgs()
                    {
                        RowIndex = gridview.CurrentCell.RowIndex,
                        IsValidate = true,
                        PrimaryColumnName = currentColumn.DataPropertyName,
                        PrimaryColumnType = currentColumn.GetDataSourceInformation().GetDbColumnType().FullName,
                        SearchConditionTable = TMTExtendard.GetSearchConditionTable(),
                        LovViewName = currentColumn.LovViewName
                    };
                    if (gridview.CurrentCell.Value != null)
                    {
                        lovEvent.PrimaryColumnValue = gridview.CurrentCell.Value.ToString();
                    }
                    lovEvent.Row = gridview.Rows[lovEvent.RowIndex];

                    gridview.GetLovSelectedRow(lovEvent);

                    if (lovEvent.LovDataTable != null)
                    {
                        bool dataFound = (lovEvent.LovDataTable.Rows.Count == 1);

                        if (dataFound)
                        {
                            LovLoadedEventArgs lovLoaded = new LovLoadedEventArgs()
                            {
                                IsValidate = lovEvent.IsValidate,
                                RowIndex = lovEvent.RowIndex,
                                PrimaryColumnName = lovEvent.PrimaryColumnName,
                                SelectedRow = new Dictionary<string, object>(),
                                LovViewName = lovEvent.LovViewName
                            };
                            DataRow row = lovEvent.LovDataTable.Rows[0];
                            foreach (DataColumn col in lovEvent.LovDataTable.Columns)
                            {
                                lovLoaded.SelectedRow.Add(col.ColumnName, row[col]);
                            }
                            gridview.CurrentCell.Tag = gridview.CurrentCell.Value;

                            gridview.SetLovSelectedRow(lovLoaded);
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
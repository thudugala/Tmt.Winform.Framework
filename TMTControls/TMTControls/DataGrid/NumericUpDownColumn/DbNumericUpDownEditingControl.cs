using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.DataGrid
{
    /// <summary>
    /// Defines the editing control for the DataGridViewNumericUpDownCell custom cell type.
    /// </summary>
    [ToolboxItem(false)]
    internal class DbNumericUpDownEditingControl : NumericUpDown, IDataGridViewEditingControl
    {
        /// <summary>
        /// Constructor of the editing control class
        /// </summary>
        public DbNumericUpDownEditingControl()
        {
            // The editing control must not be part of the tabbing loop
            this.TabStop = false;
        }

        // Beginning of the IDataGridViewEditingControl interface implementation

        /// <summary>
        /// Property which caches the grid that uses this editing control
        /// </summary>
        public virtual DataGridView EditingControlDataGridView { get; set; }

        /// <summary>
        /// Property which represents the current formatted value of the editing control
        /// </summary>
        public virtual object EditingControlFormattedValue
        {
            get
            {
                return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
            }
            set
            {
                this.Text = (string)value;
            }
        }

        /// <summary>
        /// Property which represents the row in which the editing control resides
        /// </summary>
        public virtual int EditingControlRowIndex { get; set; }

        /// <summary>
        /// Property which indicates whether the value of the editing control has changed or not
        /// </summary>
        public virtual bool EditingControlValueChanged { get; set; }

        /// <summary>
        /// Property which determines which cursor must be used for the editing panel,
        /// i.e. the parent of the editing control.
        /// </summary>
        public virtual Cursor EditingPanelCursor
        {
            get
            {
                return Cursors.Default;
            }
        }

        /// <summary>
        /// Property which indicates whether the editing control needs to be repositioned
        /// when its value changes.
        /// </summary>
        public virtual bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Method called by the grid before the editing control is shown so it can adapt to the
        /// provided cell style.
        /// </summary>
        public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            if (dataGridViewCellStyle == null)
            {
                throw new ArgumentNullException(nameof(dataGridViewCellStyle));
            }

            this.Font = dataGridViewCellStyle.Font;
            if (dataGridViewCellStyle.BackColor.A < 255)
            {
                // The NumericUpDown control does not support transparent back colors
                Color opaqueBackColor = Color.FromArgb(255, dataGridViewCellStyle.BackColor);
                this.BackColor = opaqueBackColor;
                this.EditingControlDataGridView.EditingPanel.BackColor = opaqueBackColor;
            }
            else
            {
                this.BackColor = dataGridViewCellStyle.BackColor;
            }
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            this.TextAlign = DbNumericUpDownCell.TranslateAlignment(dataGridViewCellStyle.Alignment);
        }

        /// <summary>
        /// Method called by the grid on keystrokes to determine if the editing control is
        /// interested in the key or not.
        /// </summary>
        public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            if (this.Controls[1] is TextBox textBox)
            {
                switch (keyData & Keys.KeyCode)
                {
                    case Keys.Right:
                        // If the end of the selection is at the end of the string,
                        // let the DataGridView treat the key message
                        if ((this.RightToLeft == RightToLeft.No && (textBox.SelectionLength != 0 || textBox.SelectionStart != textBox.Text.Length)) ||
                            (this.RightToLeft == RightToLeft.Yes && (textBox.SelectionLength != 0 || textBox.SelectionStart != 0)))
                        {
                            return true;
                        }
                        break;

                    case Keys.Left:
                        // If the end of the selection is at the begining of the string
                        // or if the entire text is selected and we did not start editing,
                        // send this character to the dataGridView, else process the key message
                        if ((this.RightToLeft == RightToLeft.No && (textBox.SelectionLength != 0 || textBox.SelectionStart != 0)) ||
                            (this.RightToLeft == RightToLeft.Yes && (textBox.SelectionLength != 0 || textBox.SelectionStart != textBox.Text.Length)))
                        {
                            return true;
                        }
                        break;

                    case Keys.Down:
                        // If the current value hasn't reached its minimum yet, handle the key. Otherwise let
                        // the grid handle it.
                        if (this.Value > this.Minimum)
                        {
                            return true;
                        }
                        break;

                    case Keys.Up:
                        // If the current value hasn't reached its maximum yet, handle the key. Otherwise let
                        // the grid handle it.
                        if (this.Value < this.Maximum)
                        {
                            return true;
                        }
                        break;

                    case Keys.Home:
                    case Keys.End:
                        if (textBox.SelectionLength != textBox.Text.Length)
                        {
                            return true;
                        }
                        break;

                    case Keys.Delete:
                        if (textBox.SelectionLength > 0 ||
                            textBox.SelectionStart < textBox.Text.Length)
                        {
                            return true;
                        }
                        break;
                }
            }
            return !dataGridViewWantsInputKey;
        }

        /// <summary>
        /// Returns the current value of the editing control.
        /// </summary>
        public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            bool userEdit = this.UserEdit;
            try
            {
                // Prevent the Value from being set to Maximum or Minimum when the cell is being painted.
                this.UserEdit = (context & DataGridViewDataErrorContexts.Display) == 0;
                return this.Value.ToString((this.ThousandsSeparator ? "N" : "F") + this.DecimalPlaces.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            }
            finally
            {
                this.UserEdit = userEdit;
            }
        }

        /// <summary>
        /// Called by the grid to give the editing control a chance to prepare itself for
        /// the editing session.
        /// </summary>
        public virtual void PrepareEditingControlForEdit(bool selectAll)
        {
            if (this.Controls[1] is TextBox textBox)
            {
                if (selectAll)
                {
                    textBox.SelectAll();
                }
                else
                {
                    // Do not select all the text, but
                    // position the caret at the end of the text
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        // End of the IDataGridViewEditingControl interface implementation

        /// <summary>
        /// Listen to the KeyPress notification to know when the value changed, and
        /// notify the grid of the change.
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            base.OnKeyPress(e);

            // The value changes when a digit, the decimal separator, the group separator or
            // the negative sign is pressed.
            bool notifyValueChange = false;
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                notifyValueChange = true;
            }
            else
            {
                NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;

                if (string.IsNullOrEmpty(numberFormatInfo.NumberDecimalSeparator) == false)
                {
                    notifyValueChange = numberFormatInfo.NumberDecimalSeparator[0] == e.KeyChar;
                }
                if (!notifyValueChange && !string.IsNullOrEmpty(numberFormatInfo.NumberGroupSeparator))
                {
                    notifyValueChange = numberFormatInfo.NumberGroupSeparator[0] == e.KeyChar;
                }
                if (!notifyValueChange && !string.IsNullOrEmpty(numberFormatInfo.NegativeSign))
                {
                    notifyValueChange = numberFormatInfo.NegativeSign[0] == e.KeyChar;
                }
            }

            if (notifyValueChange)
            {
                // Let the DataGridView know about the value change
                NotifyDataGridViewOfValueChange();
            }
        }

        /// <summary>
        /// Listen to the ValueChanged notification to forward the change to the grid.
        /// </summary>
        protected override void OnValueChanged(EventArgs e)
        {
            base.OnValueChanged(e);
            if (this.Focused)
            {
                // Let the DataGridView know about the value change
                NotifyDataGridViewOfValueChange();
            }
        }

        /// <summary>
        /// A few keyboard messages need to be forwarded to the inner textbox of the
        /// NumericUpDown control so that the first character pressed appears in it.
        /// </summary>
        protected override bool ProcessKeyEventArgs(ref Message m)
        {
            if (this.Controls[1] is TextBox textBox)
            {
                NativeMethods.SendMessage(textBox.Handle, m.Msg, m.WParam, m.LParam);
                return true;
            }
            else
            {
                return base.ProcessKeyEventArgs(ref m);
            }
        }

        /// <summary>
        /// Small utility function that updates the local dirty state and
        /// notifies the grid of the value change.
        /// </summary>
        private void NotifyDataGridViewOfValueChange()
        {
            if (this.EditingControlValueChanged == false)
            {
                this.EditingControlValueChanged = true;
                this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            }
        }
    }
}
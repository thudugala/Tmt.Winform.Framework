using System;
using System.Drawing;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewTextButtonBoxEditingControl : TMTTextButtonBoxBase, IDataGridViewEditingControl
    {
        public TMTDataGridViewTextButtonBoxEditingControl()
        {
            InitializeComponent();
        }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            if (dataGridViewCellStyle == null)
            {
                throw new ArgumentNullException(nameof(dataGridViewCellStyle));
            }

            this.Font = dataGridViewCellStyle.Font;
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            if (dataGridViewCellStyle.BackColor.A < 255)
            {
                Color opaqueBackColor = Color.FromArgb(255, dataGridViewCellStyle.BackColor);
                BackColor = opaqueBackColor;
                EditingControlDataGridView.EditingPanel.BackColor = opaqueBackColor;
            }
            else
            {
                BackColor = dataGridViewCellStyle.BackColor;
            }
            this.TextAlign = TMTDataGridViewTextButtonBoxCell.TranslateAlignment(dataGridViewCellStyle.Alignment);
        }

        public DataGridView EditingControlDataGridView { get; set; }

        public object EditingControlFormattedValue
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = (value != null) ? value.ToString() : string.Empty;
            }
        }

        public int EditingControlRowIndex { get; set; }

        public bool EditingControlValueChanged { get; set; }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            TextBox textBox = InnerTextBox;
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:
                    {
                        if (textBox != null)
                        {
                            // If the end of the selection is at the end of the string,
                            // let the DataGridView treat the key message
                            if ((RightToLeft == RightToLeft.No && !(textBox.SelectionLength == 0 && textBox.SelectionStart == textBox.Text.Length)) ||
                                (RightToLeft == RightToLeft.Yes && !(textBox.SelectionLength == 0 && textBox.SelectionStart == 0)))
                            {
                                return true;
                            }
                        }
                        break;
                    }

                case Keys.Left:
                    {
                        if (textBox != null)
                        {
                            // If the end of the selection is at the begining of the string
                            // or if the entire text is selected and we did not start editing,
                            // send this character to the dataGridView, else process the key message
                            if ((RightToLeft == RightToLeft.No && !(textBox.SelectionLength == 0 && textBox.SelectionStart == 0)) ||
                                (RightToLeft == RightToLeft.Yes && !(textBox.SelectionLength == 0 && textBox.SelectionStart == textBox.Text.Length)))
                            {
                                return true;
                            }
                        }
                        break;
                    }

                case Keys.Home:
                case Keys.End:
                    {
                        // Let the grid handle the key if the entire text is selected.
                        if (textBox != null)
                        {
                            if (textBox.SelectionLength != textBox.Text.Length)
                            {
                                return true;
                            }
                        }
                        break;
                    }

                case Keys.Delete:
                    {
                        // Let the grid handle the key if the carret is at the end of the text.
                        if (textBox != null)
                        {
                            if (textBox.SelectionLength > 0 ||
                                textBox.SelectionStart < textBox.Text.Length)
                            {
                                return true;
                            }
                        }
                        break;
                    }
            }
            return !dataGridViewWantsInputKey;
        }

        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return this.EditingControlFormattedValue;
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
            if (selectAll)
            {
                InnerTextBox.SelectAll();
            }
            else
            {
                // Do not select all the text, but
                // position the caret at the end of the text
                InnerTextBox.SelectionStart = InnerTextBox.Text.Length;
            }
        }

        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (this.Focused)
            {
                // Let the DataGridView know about the value change
                NotifyDataGridViewOfValueChange();
            }
        }

        private void NotifyDataGridViewOfValueChange()
        {
            if (this.EditingControlValueChanged == false)
            {
                this.EditingControlValueChanged = true;
                this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // InnerTextBox
            //
            this.InnerTextBox.BackColor = System.Drawing.Color.White;
            this.InnerTextBox.TextChanged += new System.EventHandler(this.InnerTextBox_TextChanged);
            //
            // TMTDataGridViewTextButtonBoxEditingControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoSize = true;
            this.Name = "TMTDataGridViewTextButtonBoxEditingControl";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void InnerTextBox_TextChanged(object sender, EventArgs e)
        {
            NotifyDataGridViewOfValueChange();
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewCalendarCell : DataGridViewTextBoxCell
    {
        public TMTDataGridViewCalendarCell()
            : base()
        {
            // Use the short date format.
            this.Style.Format = "d";
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            TMTDataGridViewCalendarEditingControl ctl = DataGridView.EditingControl as TMTDataGridViewCalendarEditingControl;
            // Use the default row value when Value property is null.
            if (this.Value == null || string.IsNullOrWhiteSpace(this.Value.ToString()))
            {
                ctl.Value = (DateTime)this.DefaultNewRowValue;
            }
            else
            {
                ctl.Value = (DateTime)this.Value;
            }
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing control that CalendarCell uses.
                return typeof(TMTDataGridViewCalendarEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains.

                return typeof(DateTime);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                // Use the current date and time as the default value.
                return DateTime.Now;
            }
        }

        public override void PositionEditingControl(bool setLocation,
                                            bool setSize,
                                            Rectangle cellBounds,
                                            Rectangle cellClip,
                                            DataGridViewCellStyle cellStyle,
                                            bool singleVerticalBorderAdded,
                                            bool singleHorizontalBorderAdded,
                                            bool isFirstDisplayedColumn,
                                            bool isFirstDisplayedRow)
        {
            Rectangle editingControlBounds = PositionEditingPanel(cellBounds,
                                                        cellClip,
                                                        cellStyle,
                                                        singleVerticalBorderAdded,
                                                        singleHorizontalBorderAdded,
                                                        isFirstDisplayedColumn,
                                                        isFirstDisplayedRow);
            editingControlBounds = GetAdjustedEditingControlBounds(editingControlBounds, cellStyle);
            this.DataGridView.EditingControl.Location = new Point(editingControlBounds.X, editingControlBounds.Y);
            this.DataGridView.EditingControl.Size = new Size(editingControlBounds.Width, editingControlBounds.Height);
        }

        private static Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds, DataGridViewCellStyle cellStyle)
        {
            // Add a 1 pixel padding on the left and right of the editing control
            editingControlBounds.X += 1;
            editingControlBounds.Width = Math.Max(0, editingControlBounds.Width - 2);

            // Adjust the vertical location of the editing control:
            int preferredHeight = cellStyle.Font.Height + 3;
            if (preferredHeight < editingControlBounds.Height)
            {
                switch (cellStyle.Alignment)
                {
                    case DataGridViewContentAlignment.MiddleLeft:
                    case DataGridViewContentAlignment.MiddleCenter:
                    case DataGridViewContentAlignment.MiddleRight:
                        editingControlBounds.Y += (editingControlBounds.Height - preferredHeight) / 2;
                        break;

                    case DataGridViewContentAlignment.BottomLeft:
                    case DataGridViewContentAlignment.BottomCenter:
                    case DataGridViewContentAlignment.BottomRight:
                        editingControlBounds.Y += editingControlBounds.Height - preferredHeight;
                        break;
                }
            }

            return editingControlBounds;
        }
    }
}
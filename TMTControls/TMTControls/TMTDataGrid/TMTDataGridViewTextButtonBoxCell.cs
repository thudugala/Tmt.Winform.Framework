using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TMTControls.TMTDataGrid
{
    public class TMTDataGridViewTextButtonBoxCell : DataGridViewTextBoxCell
    {
        // Used in TranslateAlignment function
        private static readonly DataGridViewContentAlignment AnyRight = DataGridViewContentAlignment.TopRight |
                                                                        DataGridViewContentAlignment.MiddleRight |
                                                                        DataGridViewContentAlignment.BottomRight;

        private static readonly DataGridViewContentAlignment AnyCenter = DataGridViewContentAlignment.TopCenter |
                                                                         DataGridViewContentAlignment.MiddleCenter |
                                                                         DataGridViewContentAlignment.BottomCenter;

        // Default dimensions of the static rendering bitmap used for the painting of the non-edited cells
        private const int DATAGRIDVIEWNUMERICUPDOWNCELL_defaultRenderingBitmapWidth = 150;

        private const int DATAGRIDVIEWNUMERICUPDOWNCELL_defaultRenderingBitmapHeight = 18;

        // Type of this cell's editing control
        private static Type defaultEditType = typeof(TMTDataGridViewTextButtonBoxEditingControl);

        // Type of this cell's value. The formatted value type is string, the same as the base class DataGridViewTextBoxCell
        private static Type defaultValueType = typeof(string);

        // The bitmap used to paint the non-edited cells via a call to TMTTextButtonBoxBase.DrawToBitmap
        [ThreadStatic]
        private static Bitmap renderingBitmap;

        // The NumericUpDown control used to paint the non-edited cells via a call to TMTTextButtonBoxBase.DrawToBitmap
        [ThreadStatic]
        private static TMTTextButtonBoxBase paintingTMTTextButtonBoxBase;

        public TMTDataGridViewTextButtonBoxCell()
            : base()
        {
            // Create a thread specific bitmap used for the painting of the non-edited cells
            if (renderingBitmap == null)
            {
                renderingBitmap = new Bitmap(DATAGRIDVIEWNUMERICUPDOWNCELL_defaultRenderingBitmapWidth, DATAGRIDVIEWNUMERICUPDOWNCELL_defaultRenderingBitmapHeight);
            }

            // Create a thread specific NumericUpDown control used for the painting of the non-edited cells
            if (paintingTMTTextButtonBoxBase == null)
            {
                paintingTMTTextButtonBoxBase = new TMTTextButtonBoxBase
                {
                    // Some properties only need to be set once for the lifetime of the control:
                    BorderStyle = BorderStyle.None
                };
            }
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            if (DataGridView.EditingControl is TMTTextButtonBoxBase textButtonBox)
            {
                string initialFormattedValueStr = initialFormattedValue as string;
                textButtonBox.Text = initialFormattedValueStr;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void DetachEditingControl()
        {
            base.DetachEditingControl();
            if (DataGridView.EditingControl is TMTTextButtonBoxBase textButton)
            {
                textButton.ClearUndo();
            }
        }

        public override Type EditType
        {
            get { return defaultEditType; }
        }

        public override Type ValueType
        {
            get { return base.ValueType ?? defaultValueType; }
        }

        public override Type FormattedValueType
        {
            get { return defaultValueType; }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
                               object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle,
                               DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            if (cellStyle == null)
            {
                throw new ArgumentNullException(nameof(cellStyle));
            }

            if (this.DataGridView == null)
            {
                return;
            }

            if (paintingTMTTextButtonBoxBase.IsDisposed)
            {
                return;
            }

            // First paint the borders and background of the cell.
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle,
                       paintParts & ~(DataGridViewPaintParts.ErrorIcon | DataGridViewPaintParts.ContentForeground));

            Point ptCurrentCell = this.DataGridView.CurrentCellAddress;
            bool cellCurrent = ptCurrentCell.X == this.ColumnIndex && ptCurrentCell.Y == rowIndex;
            bool cellEdited = cellCurrent && this.DataGridView.EditingControl != null;

            // If the cell is in editing mode, there is nothing else to paint
            if (cellEdited == false)
            {
                if (PartPainted(paintParts, DataGridViewPaintParts.ContentForeground))
                {
                    // Paint a TMTTextButtonBoxBase control
                    // Take the borders into account
                    Rectangle borderWidths = BorderWidths(advancedBorderStyle);
                    Rectangle valBounds = cellBounds;
                    valBounds.Offset(borderWidths.X, borderWidths.Y);
                    valBounds.Width -= borderWidths.Right;
                    valBounds.Height -= borderWidths.Bottom;
                    // Also take the padding into account
                    if (cellStyle.Padding != Padding.Empty)
                    {
                        if (this.DataGridView.RightToLeft == RightToLeft.Yes)
                        {
                            valBounds.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
                        }
                        else
                        {
                            valBounds.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
                        }
                        valBounds.Width -= cellStyle.Padding.Horizontal;
                        valBounds.Height -= cellStyle.Padding.Vertical;
                    }
                    // Determine the TMTTextButtonBoxBase control location
                    //valBounds = GetAdjustedEditingControlBounds(valBounds, cellStyle);

                    bool cellSelected = (cellState & DataGridViewElementStates.Selected) != 0;

                    if (renderingBitmap.Width < valBounds.Width ||
                        renderingBitmap.Height < valBounds.Height)
                    {
                        // The static bitmap is too small, a bigger one needs to be allocated.
                        renderingBitmap.Dispose();
                        renderingBitmap = new Bitmap(valBounds.Width, valBounds.Height);
                    }
                    // Make sure the TMTTextButtonBoxBase control is parented to a visible control
                    if (paintingTMTTextButtonBoxBase.Parent == null || paintingTMTTextButtonBoxBase.Parent.Visible == false)
                    {
                        paintingTMTTextButtonBoxBase.Parent = this.DataGridView;
                    }
                    // Set all the relevant properties
                    paintingTMTTextButtonBoxBase.TextAlign = TMTDataGridViewNumericUpDownCell.TranslateAlignment(cellStyle.Alignment);
                    paintingTMTTextButtonBoxBase.Font = cellStyle.Font;
                    paintingTMTTextButtonBoxBase.Width = valBounds.Width;
                    paintingTMTTextButtonBoxBase.Height = valBounds.Height;
                    paintingTMTTextButtonBoxBase.RightToLeft = this.DataGridView.RightToLeft;
                    paintingTMTTextButtonBoxBase.Location = new Point(0, -paintingTMTTextButtonBoxBase.Height - 100);
                    paintingTMTTextButtonBoxBase.Text = formattedValue as string;

                    Color backColor;
                    if (PartPainted(paintParts, DataGridViewPaintParts.SelectionBackground) && cellSelected)
                    {
                        backColor = cellStyle.SelectionBackColor;
                    }
                    else
                    {
                        backColor = cellStyle.BackColor;
                    }
                    if (PartPainted(paintParts, DataGridViewPaintParts.Background))
                    {
                        if (backColor.A < 255)
                        {
                            // The TMTTextButtonBoxBase control does not support transparent back colors
                            backColor = Color.FromArgb(255, backColor);
                        }
                        paintingTMTTextButtonBoxBase.BackColor = backColor;
                    }
                    // Finally paint the TMTTextButtonBoxBase control
                    Rectangle srcRect = new Rectangle(0, 0, valBounds.Width, valBounds.Height);
                    if (srcRect.Width > 0 && srcRect.Height > 0)
                    {
                        paintingTMTTextButtonBoxBase.DrawToBitmap(renderingBitmap, srcRect);
                        graphics.DrawImage(renderingBitmap, new Rectangle(valBounds.Location, valBounds.Size),
                                           srcRect, GraphicsUnit.Pixel);
                    }
                }
                if (PartPainted(paintParts, DataGridViewPaintParts.ErrorIcon))
                {
                    // Paint the potential error icon on top of the NumericUpDown control
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText,
                               cellStyle, advancedBorderStyle, DataGridViewPaintParts.ErrorIcon);
                }
            }
        }

        /// <summary>
        /// Little utility function called by the Paint function to see if a particular part needs to be painted.
        /// </summary>
        private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart)
        {
            return (paintParts & paintPart) != 0;
        }

        /// <summary>
        /// Little utility function used by both the cell and column types to translate a DataGridViewContentAlignment value into
        /// a HorizontalAlignment value.
        /// </summary>
        internal static HorizontalAlignment TranslateAlignment(DataGridViewContentAlignment align)
        {
            if ((align & AnyRight) != 0)
            {
                return HorizontalAlignment.Right;
            }
            else if ((align & AnyCenter) != 0)
            {
                return HorizontalAlignment.Center;
            }
            else
            {
                return HorizontalAlignment.Left;
            }
        }
    }
}
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.DataGrid
{
    public class DbTextButtonBoxCell : DataGridViewTextBoxCell
    {
        private const int DATAGRIDVIEWNUMERICUPDOWNCELL_defaultRenderingBitmapHeight = 18;

        // Default dimensions of the static rendering bitmap used for the painting of the non-edited cells
        private const int DATAGRIDVIEWNUMERICUPDOWNCELL_defaultRenderingBitmapWidth = 150;

        private static readonly DataGridViewContentAlignment AnyCenter = DataGridViewContentAlignment.TopCenter |
                                                                                 DataGridViewContentAlignment.MiddleCenter |
                                                                                 DataGridViewContentAlignment.BottomCenter;

        // Used in TranslateAlignment function
        private static readonly DataGridViewContentAlignment AnyRight = DataGridViewContentAlignment.TopRight |
                                                                        DataGridViewContentAlignment.MiddleRight |
                                                                        DataGridViewContentAlignment.BottomRight;

        // Type of this cell's editing control
        private static Type defaultEditType = typeof(DbTextButtonBoxEditingControl);

        // Type of this cell's value. The formatted value type is string, the same as the base class DataGridViewTextBoxCell
        private static Type defaultValueType = typeof(string);

        // The NumericUpDown control used to paint the non-edited cells via a call to DbTextButtonBoxBase.DrawToBitmap
        [ThreadStatic]
        private static TextButtonBoxBase paintingTextButtonBoxBase;

        // The bitmap used to paint the non-edited cells via a call to DbTextButtonBoxBase.DrawToBitmap
        [ThreadStatic]
        private static Bitmap renderingBitmap;

        public DbTextButtonBoxCell()
            : base()
        {
            // Create a thread specific bitmap used for the painting of the non-edited cells
            if (renderingBitmap == null)
            {
                renderingBitmap = new Bitmap(DATAGRIDVIEWNUMERICUPDOWNCELL_defaultRenderingBitmapWidth, DATAGRIDVIEWNUMERICUPDOWNCELL_defaultRenderingBitmapHeight);
            }

            // Create a thread specific NumericUpDown control used for the painting of the non-edited cells
            if (paintingTextButtonBoxBase == null)
            {
                paintingTextButtonBoxBase = new TextButtonBoxBase
                {
                    // Some properties only need to be set once for the lifetime of the control:
                    BorderStyle = BorderStyle.None
                };
            }
        }

        public override Type EditType
        {
            get { return defaultEditType; }
        }

        public override Type FormattedValueType
        {
            get { return defaultValueType; }
        }

        public override Type ValueType
        {
            get { return base.ValueType ?? defaultValueType; }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void DetachEditingControl()
        {
            base.DetachEditingControl();
            if (DataGridView.EditingControl is TextButtonBoxBase textButton)
            {
                textButton.ClearUndo();
            }
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            if (DataGridView.EditingControl is TextButtonBoxBase textButtonBox)
            {
                string initialFormattedValueStr = initialFormattedValue as string;
                textButtonBox.Text = initialFormattedValueStr;
            }
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

            if (paintingTextButtonBoxBase.IsDisposed)
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
                    // Paint a DbTextButtonBoxBase control
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
                    // Determine the DbTextButtonBoxBase control location
                    //valBounds = GetAdjustedEditingControlBounds(valBounds, cellStyle);

                    bool cellSelected = (cellState & DataGridViewElementStates.Selected) != 0;

                    if (renderingBitmap.Width < valBounds.Width ||
                        renderingBitmap.Height < valBounds.Height)
                    {
                        // The static bitmap is too small, a bigger one needs to be allocated.
                        renderingBitmap.Dispose();
                        renderingBitmap = new Bitmap(valBounds.Width, valBounds.Height);
                    }
                    // Make sure the DbTextButtonBoxBase control is parented to a visible control
                    if (paintingTextButtonBoxBase.Parent == null || paintingTextButtonBoxBase.Parent.Visible == false)
                    {
                        paintingTextButtonBoxBase.Parent = this.DataGridView;
                    }
                    // Set all the relevant properties
                    paintingTextButtonBoxBase.TextAlign = DbNumericUpDownCell.TranslateAlignment(cellStyle.Alignment);
                    paintingTextButtonBoxBase.Font = cellStyle.Font;
                    paintingTextButtonBoxBase.Width = valBounds.Width;
                    paintingTextButtonBoxBase.Height = valBounds.Height;
                    paintingTextButtonBoxBase.RightToLeft = this.DataGridView.RightToLeft;
                    paintingTextButtonBoxBase.Location = new Point(0, -paintingTextButtonBoxBase.Height - 100);
                    paintingTextButtonBoxBase.Text = formattedValue as string;

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
                            // The DbTextButtonBoxBase control does not support transparent back colors
                            backColor = Color.FromArgb(255, backColor);
                        }
                        paintingTextButtonBoxBase.BackColor = backColor;
                    }
                    // Finally paint the DbTextButtonBoxBase control
                    Rectangle srcRect = new Rectangle(0, 0, valBounds.Width, valBounds.Height);
                    if (srcRect.Width > 0 && srcRect.Height > 0)
                    {
                        paintingTextButtonBoxBase.DrawToBitmap(renderingBitmap, srcRect);
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
    }
}
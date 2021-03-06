using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.DataGrid
{
    /// <summary>
    /// Custom column type dedicated to the DataGridViewNumericUpDownCell cell type.
    /// </summary>
    public class DbNumericUpDownColumn : DataGridViewColumn, IDbColumn
    {
        /// <summary>
        /// Constructor for the DataGridViewNumericUpDownColumn class.
        /// </summary>
        public DbNumericUpDownColumn()
            : base(new DbNumericUpDownCell())
        {
            base.ValueType = typeof(Int64);
            base.SortMode = DataGridViewColumnSortMode.Automatic;
            this.TabStop = true;
        }

        /// <summary>
        /// Represents the implicit cell that gets cloned when adding rows to the grid.
        /// </summary>
        [
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
        ]
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value is DbNumericUpDownCell)
                {
                    base.CellTemplate = value;
                }
                else
                {
                    throw new InvalidCastException($"Value provided for CellTemplate must be of type {nameof(DbNumericUpDownColumn)}.{nameof(DbNumericUpDownCell)} or derive from it.");
                }
            }
        }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyMandatory { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyPrimaryKey { get; set; }

        [Category("Data"), DefaultValue(TypeCode.Int64), RefreshProperties(RefreshProperties.All)]
        public TypeCode DataPropertyType
        {
            get
            {
                return Type.GetTypeCode(base.ValueType);
            }
            set
            {
                base.ValueType = Type.GetType("System." + value);
            }
        }

        /// <summary>
        /// Replicates the DecimalPlaces property of the DataGridViewNumericUpDownCell cell type.
        /// </summary>
        [
            Category("Data"),
            DefaultValue(DbNumericUpDownCell.DATAGRIDVIEWNUMERICUPDOWNCELL_defaultDecimalPlaces),
            Description("Indicates the number of decimal places to display.")
        ]
        public int DecimalPlaces
        {
            get
            {
                if (this.NumericUpDownCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }
                return this.NumericUpDownCellTemplate.DecimalPlaces;
            }
            set
            {
                if (this.NumericUpDownCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }
                // Update the template cell so that subsequent cloned cells use the new value.
                this.NumericUpDownCellTemplate.DecimalPlaces = value;
                if (this.DataGridView != null)
                {
                    // Update all the existing DataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily.
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[this.Index] is DbNumericUpDownCell dataGridViewCell)
                        {
                            // Call the internal SetDecimalPlaces method instead of the property to avoid invalidation
                            // of each cell. The whole column is invalidated later in a single operation for better performance.
                            dataGridViewCell.SetDecimalPlaces(rowIndex, value);
                        }
                    }
                    this.DataGridView.InvalidateColumn(this.Index);
                    // TODO: Call the grid's autosizing methods to autosize the column, rows, column headers / row headers as needed.
                }
            }
        }

        [Category("Appearance")]
        public DataGridViewCellStyle HeaderStyle
        {
            get
            {
                return this.HeaderCell.Style;
            }
            set
            {
                this.HeaderCell.Style = value;
            }
        }

        /// <summary>
        /// Replicates the Increment property of the DataGridViewNumericUpDownCell cell type.
        /// </summary>
        [
            Category("Data"),
            Description("Indicates the amount to increment or decrement on each button click.")
        ]
        public Decimal Increment
        {
            get
            {
                if (this.NumericUpDownCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }
                return this.NumericUpDownCellTemplate.Increment;
            }
            set
            {
                if (this.NumericUpDownCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }
                this.NumericUpDownCellTemplate.Increment = value;
                if (this.DataGridView != null)
                {
                    DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[this.Index] is DbNumericUpDownCell dataGridViewCell)
                        {
                            dataGridViewCell.SetIncrement(rowIndex, value);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Replicates the Maximum property of the DataGridViewNumericUpDownCell cell type.
        /// </summary>
        [
            Category("Data"),
            Description("Indicates the maximum value for the numeric up-down cells."),
            RefreshProperties(RefreshProperties.All),
            DefaultValue(100000)
        ]
        public Decimal Maximum
        {
            get
            {
                if (this.NumericUpDownCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }
                return this.NumericUpDownCellTemplate.Maximum;
            }
            set
            {
                if (this.NumericUpDownCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }
                this.NumericUpDownCellTemplate.Maximum = value;
                if (this.DataGridView != null)
                {
                    DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[this.Index] is DbNumericUpDownCell dataGridViewCell)
                        {
                            dataGridViewCell.SetMaximum(rowIndex, value);
                        }
                    }
                    this.DataGridView.InvalidateColumn(this.Index);
                    // TODO: This column and/or grid rows may need to be autosized depending on their
                    //       autosize settings. Call the autosizing methods to autosize the column, rows,
                    //       column headers / row headers as needed.
                }
            }
        }

        /// <summary>
        /// Replicates the Minimum property of the DataGridViewNumericUpDownCell cell type.
        /// </summary>
        [
            Category("Data"),
            Description("Indicates the minimum value for the numeric up-down cells."),
            RefreshProperties(RefreshProperties.All)
        ]
        public Decimal Minimum
        {
            get
            {
                if (this.NumericUpDownCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }
                return this.NumericUpDownCellTemplate.Minimum;
            }
            set
            {
                if (this.NumericUpDownCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }
                this.NumericUpDownCellTemplate.Minimum = value;
                if (this.DataGridView != null)
                {
                    DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[this.Index] is DbNumericUpDownCell dataGridViewCell)
                        {
                            dataGridViewCell.SetMinimum(rowIndex, value);
                        }
                    }
                    this.DataGridView.InvalidateColumn(this.Index);
                    // TODO: This column and/or grid rows may need to be autosized depending on their
                    //       autosize settings. Call the autosizing methods to autosize the column, rows,
                    //       column headers / row headers as needed.
                }
            }
        }

        [Category("Behavior"), DefaultValue(true)]
        public bool TabStop { get; set; }

        /// <summary>
        /// Replicates the ThousandsSeparator property of the DataGridViewNumericUpDownCell cell type.
        /// </summary>
        [
            Category("Data"),
            DefaultValue(DbNumericUpDownCell.DATAGRIDVIEWNUMERICUPDOWNCELL_defaultThousandsSeparator),
            Description("Indicates whether the thousands separator will be inserted between every three decimal digits.")
        ]
        public bool ThousandsSeparator
        {
            get
            {
                if (this.NumericUpDownCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }
                return this.NumericUpDownCellTemplate.ThousandsSeparator;
            }
            set
            {
                if (this.NumericUpDownCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }
                this.NumericUpDownCellTemplate.ThousandsSeparator = value;
                if (this.DataGridView != null)
                {
                    DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[this.Index] is DbNumericUpDownCell dataGridViewCell)
                        {
                            dataGridViewCell.SetThousandsSeparator(rowIndex, value);
                        }
                    }
                    this.DataGridView.InvalidateColumn(this.Index);
                    // TODO: This column and/or grid rows may need to be autosized depending on their
                    //       autosize settings. Call the autosizing methods to autosize the column, rows,
                    //       column headers / row headers as needed.
                }
            }
        }

        /// <summary>
        /// Small utility function that returns the template cell as a DataGridViewNumericUpDownCell
        /// </summary>
        private DbNumericUpDownCell NumericUpDownCellTemplate
        {
            get
            {
                return this.CellTemplate as DbNumericUpDownCell;
            }
        }

        public override object Clone()
        {
            var that = (DbNumericUpDownColumn)base.Clone();

            that.DataPropertyType = this.DataPropertyType;
            that.DataPropertyMandatory = this.DataPropertyMandatory;
            that.DataPropertyPrimaryKey = this.DataPropertyPrimaryKey;
            that.TabStop = this.TabStop;
            that.HeaderStyle = this.HeaderStyle;

            return that;
        }

        /// <summary>
        /// Returns a standard compact string representation of the column.
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(100);
            sb.Append("DbNumericUpDownColumn { Name=");
            sb.Append(this.Name);
            sb.Append(", Index=");
            sb.Append(this.Index.ToString(CultureInfo.CurrentCulture));
            sb.Append(" }");
            return sb.ToString();
        }

        /// Indicates whether the Increment property should be persisted.
        private bool ShouldSerializeIncrement()
        {
            return !this.Increment.Equals(DbNumericUpDownCell.DATAGRIDVIEWNUMERICUPDOWNCELL_defaultIncrement);
        }

        /// Indicates whether the Maximum property should be persisted.
        private bool ShouldSerializeMaximum()
        {
            return !this.Maximum.Equals(DbNumericUpDownCell.DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMaximum);
        }

        /// Indicates whether the Maximum property should be persisted.
        private bool ShouldSerializeMinimum()
        {
            return !this.Minimum.Equals(DbNumericUpDownCell.DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMinimum);
        }
    }
}
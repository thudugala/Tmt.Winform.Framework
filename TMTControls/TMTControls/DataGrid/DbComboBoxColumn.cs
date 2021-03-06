﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.DataGrid
{
    public class DbComboBoxColumn : DataGridViewComboBoxColumn, IDbColumn
    {
        public DbComboBoxColumn()
        {
            this.FlatStyle = FlatStyle.Flat;
            base.ValueType = typeof(string);
            this.TabStop = true;
        }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyMandatory { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool DataPropertyPrimaryKey { get; set; }

        [Category("Data"), DefaultValue(TypeCode.String), RefreshProperties(RefreshProperties.All)]
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

        [Category("Behavior"), DefaultValue(true)]
        public bool TabStop { get; set; }

        public override object Clone()
        {
            DbComboBoxColumn that = (DbComboBoxColumn)base.Clone();

            that.DataPropertyType = this.DataPropertyType;
            that.DataPropertyMandatory = this.DataPropertyMandatory;
            that.DataPropertyPrimaryKey = this.DataPropertyPrimaryKey;
            that.DisplayMember = this.DisplayMember;
            that.ValueMember = this.ValueMember;
            that.TabStop = this.TabStop;

            return that;
        }
    }
}
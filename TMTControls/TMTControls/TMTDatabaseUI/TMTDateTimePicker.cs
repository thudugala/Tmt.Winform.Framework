﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls.TMTDatabaseUI
{
    public class TMTDateTimePicker : DateTimePicker, ITMTDatabaseUIControl
    {
        public TMTDateTimePicker()
        {
            this.SuspendLayout();

            this.DbColumnType = TypeCode.DateTime;
            this.Format = DateTimePickerFormat.Short;

            this.ResumeLayout(false);
        }

        [Category("Design")]
        public Label ConnectedLabel { get; set; }

        [Category("Data"), DefaultValue("")]
        public string DbColumnName { get; set; }

        [Category("Data"), DefaultValue(TypeCode.DateTime), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public TypeCode DbColumnType { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool KeyColumn { get; set; }

        [Category("Data"), DefaultValue(false)]
        public bool MandatoryColumn { get; set; }

        public string GetLableText()
        {            
            return this.ConnectedLabel?.Text;
        }

        public Type GetDbColumnSystemType()
        {
            return Type.GetType("System." + this.DbColumnType);
        }
    }
}
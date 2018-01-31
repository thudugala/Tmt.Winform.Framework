using System;
using System.ComponentModel;

namespace TMTControls.TMTDatabaseUI
{
    public interface ITMTDatabaseUIControl
    {
        [Category("Data"), DefaultValue("")]
        string DbColumnName { get; set; }

        [Category("Data"), DefaultValue(TypeCode.String)]
        TypeCode DbColumnType { get; set; }

        [Category("Data"), DefaultValue(false)]
        bool KeyColumn { get; set; }

        [Category("Data"), DefaultValue(false)]
        bool MandatoryColumn { get; set; }

        string GetLableText();

        Type GetDbColumnSystemType();
    }
}
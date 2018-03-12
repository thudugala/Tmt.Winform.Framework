using System;
using System.ComponentModel;

namespace TMT.Controls.WinForms.DatabaseUI
{
    public interface IDbControl
    {
        [Category("Data"), DefaultValue("")]
        string DbColumnName { get; set; }

        [Category("Data"), DefaultValue(TypeCode.String)]
        TypeCode DbColumnType { get; set; }

        [Category("Data"), DefaultValue(false)]
        bool KeyColumn { get; set; }

        [Category("Data"), DefaultValue(false)]
        bool MandatoryColumn { get; set; }

        Type GetDbColumnSystemType();

        string GetLableText();
    }
}
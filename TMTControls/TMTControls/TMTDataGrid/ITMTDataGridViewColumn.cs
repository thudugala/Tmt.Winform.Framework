using System;
using System.ComponentModel;

namespace TMTControls.TMTDataGrid
{
    public interface ITMTDataGridViewColumn
    {
        [Category("Data"), DefaultValue(false)]
        bool DataPropertyMandatory { get; set; }

        [Category("Data"), DefaultValue(false)]
        bool DataPropertyPrimaryKey { get; set; }

        [Category("Data"), DefaultValue(TypeCode.String), RefreshProperties(RefreshProperties.All)]
        TypeCode DataPropertyType { get; set; }

        [Category("Behavior"), DefaultValue(true)]
        bool TabStop { get; set; }
    }
}
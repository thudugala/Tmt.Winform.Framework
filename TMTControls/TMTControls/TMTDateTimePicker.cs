﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TMTControls
{
    public class TMTDateTimePicker : DateTimePicker
    {
        private Label _connectedLabel;

        public TMTDateTimePicker()
        {
            this.SuspendLayout();

            this.GetDataSourceInformation().DbColumnType = TypeCode.DateTime;
            this.Format = DateTimePickerFormat.Short;

            this.ResumeLayout(false);
        }

        [Category("Design")]
        public Label ConnectedLabel
        {
            get
            {
                return this._connectedLabel;
            }
            set
            {
                this._connectedLabel = value;
                if (this._connectedLabel != null)
                {
                    this.GetDataSourceInformation().DbLabelText = this._connectedLabel.Text;
                }
            }
        }

        [Category("Data"), DefaultValue("")]
        public string DbColumnName
        {
            get
            {
                return this.GetDataSourceInformation().DbColumnName;
            }
            set
            {
                this.GetDataSourceInformation().DbColumnName = value;
            }
        }

        [Category("Data"), DefaultValue(false)]
        public bool KeyColum
        {
            get
            {
                return this.GetDataSourceInformation().KeyColum;
            }
            set
            {
                this.GetDataSourceInformation().KeyColum = value;
            }
        }

        [Category("Data"), DefaultValue(false)]
        public bool MandatoryColum
        {
            get
            {
                return this.GetDataSourceInformation().MandatoryColum;
            }
            set
            {
                this.GetDataSourceInformation().MandatoryColum = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new object Tag
        {
            get
            {
                return base.Tag;
            }
            set
            {
                base.Tag = value;
            }
        }
    }
}
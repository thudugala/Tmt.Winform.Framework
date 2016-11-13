using System;
using System.Drawing;
using System.Windows.Forms;

namespace TMTControls
{
    [ToolboxBitmap(typeof(DateTimePicker))]
    public partial class TMTDateTimePickerForSearch : UserControl
    {
        private TMTDateTimePickerDropDown dialog;

        public TMTDateTimePickerForSearch()
        {
            InitializeComponent();
        }

        public new string Text
        {
            get
            {
                return textBoxMain.Text;
            }
            set
            {
                textBoxMain.Text = value;
            }
        }

        private void buttonDorpDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (dialog == null ||
                    dialog.IsDisposed)
                {
                    //The point just below the button
                    Point location = this.PointToScreen(new Point(buttonDorpDown.Left + buttonDorpDown.Width, buttonDorpDown.Bottom));
                    dialog = new TMTDateTimePickerDropDown();
                    dialog.SetLocation(location);

                    dialog.DateSelected += dialog_DateSelected;

                    dialog.Show(this);
                }
                else if (dialog != null &&
                         dialog.IsDisposed == false)
                {
                    dialog.Close();
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_ShowCalendar);
            }
        }

        private void dialog_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                if (dialog != null &&
                    dialog.IsDisposed == false)
                {
                    if (string.IsNullOrEmpty(textBoxMain.Text) == false)
                    {
                        textBoxMain.Text = textBoxMain.Text.Trim();

                        if (textBoxMain.Text.EndsWith("!=") ||
                            textBoxMain.Text.EndsWith("<=") ||
                            textBoxMain.Text.EndsWith(">=") ||
                            textBoxMain.Text.EndsWith("<") ||
                            textBoxMain.Text.EndsWith(">") ||
                            textBoxMain.Text.EndsWith(";"))
                        {
                            textBoxMain.Text += " " + e.Start.ToShortDateString();
                        }
                        else
                        {
                            textBoxMain.Text += "; " + e.Start.ToShortDateString();
                        }
                    }
                    else
                    {
                        textBoxMain.Text = e.Start.ToShortDateString();
                    }
                    dialog.Hide(); //Hide the drop-down
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_SetSelectedDate);
            }
        }

        private void textBoxMain_Enter(object sender, EventArgs e)
        {
            try
            {
                if (dialog != null &&
                    dialog.IsDisposed == false)
                {
                    dialog.Close(); //close the drop-down
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_CloseCalendar);
            }
        }
    }
}
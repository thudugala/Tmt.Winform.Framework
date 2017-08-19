using System;
using System.ComponentModel;
using System.Windows.Forms;
using TMTControls.TMTDialogs;

namespace TMTControls
{
    [DefaultEvent("DataManager")]
    public partial class TMTChart : UserControl
    {
        public TMTChart()
        {
            InitializeComponent();

            chartMain.ChartAreas["ChartAreaMain"].BackColor = chartMain.BackColor;
            chartMain.Legends["LegendMain"].BackColor = chartMain.BackColor;
        }

        [Category("TMT Data")]
        public event EventHandler<TMTChartBackgroundWorkEventArgs> DataManager;

        [Category("TMT Data")]
        public string ViewName { get; set; }

        public virtual void AfterDataLoad()
        {
        }

        public virtual void ShowChart()
        {
            chartMain.ChartAreas["ChartAreaMain"].BackColor = chartMain.BackColor;
            chartMain.Legends["LegendMain"].BackColor = chartMain.BackColor;
        }

        public virtual void OnDataManager(TMTChartBackgroundWorkEventArgs data)
        {
            DataManager?.Invoke(this, data);
        }

        private void backgroundWorkerMain_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Do not access the form's BackgroundWorker reference directly.
                // Instead, use the reference provided by the sender parameter.
                var worker = sender as BackgroundWorker;

                if (worker.CancellationPending == false)
                {
                    var arg = e.Argument as TMTChartBackgroundWorkEventArgs;

                    this.OnDataManager(arg);

                    e.Result = arg;
                }

                // If the operation was canceled by the user,
                // set the DoWorkEventArgs.Cancel property to true.
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                }
            }
            catch
            {
                //throw the exception so that RunWorkerCompleted can catch it.
                throw;
            }
        }

        private void backgroundWorkerMain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    TMTErrorDialog.Show(this, e.Error, Properties.Resources.ERROR_BW_Issue);
                }
                else
                {
                    if (e.Result is TMTChartBackgroundWorkEventArgs arg)
                    {
                        chartMain.DataSource = arg.ViewData;
                        chartMain.DataBind();
                        this.AfterDataLoad();
                    }
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_BW_Issue);
            }
            finally
            {
                progressBarMain.Visible = false;
                this.UseWaitCursor = false;
            }
        }
    }
}
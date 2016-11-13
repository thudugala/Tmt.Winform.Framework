using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

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

        public delegate void DataManagerEventHandler(object sender, TMTChartBackgroundWorkeArg e);

        [Category("TMT Data")]
        public event DataManagerEventHandler DataManager;

        [Category("TMT Data")]
        public string ViewName { get; set; }

        public virtual void AfterLoad()
        {
        }

        public virtual void ShowChart()
        {
            chartMain.ChartAreas["ChartAreaMain"].BackColor = chartMain.BackColor;
            chartMain.Legends["LegendMain"].BackColor = chartMain.BackColor;
        }

        public virtual void OnDataManager(TMTChartBackgroundWorkeArg data)
        {
            if (DataManager != null)
            {
                DataManager(this, data);
            }
        }

        public class TMTChartBackgroundWorkeArg : EventArgs
        {
            public TMTChartBackgroundWorkeArg()
            {
                this.SearchConditionTable = TMTExtendard.GetSearchConditionTable();
            }

            public DataTable SearchConditionTable { get; set; }
            public DataTable ViewData { get; set; }
            public string ViewName { get; set; }
        }

        private void backgroundWorkerMain_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Do not access the form's BackgroundWorker reference directly.
                // Instead, use the reference provided by the sender parameter.
                BackgroundWorker worker = sender as BackgroundWorker;

                if (worker.CancellationPending == false)
                {
                    TMTChartBackgroundWorkeArg arg = e.Argument as TMTChartBackgroundWorkeArg;

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
                    if (e.Result is TMTChartBackgroundWorkeArg)
                    {
                        chartMain.DataSource = (e.Result as TMTChartBackgroundWorkeArg).ViewData;
                        chartMain.DataBind();
                        this.AfterLoad();
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
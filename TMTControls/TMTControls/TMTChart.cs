using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using TinyIoC;
using TMTControls.TMTDialogs;

namespace TMTControls
{
    [DefaultEvent("DataManager")]
    public partial class TMTChart : UserControl
    {
        private IDataManager DataManager;

        public TMTChart()
        {
            InitializeComponent();
        }

        [Category("Data")]
        public string ViewName { get; set; }

        private void TMTChart_Load(object sender, EventArgs e)
        {
            try
            {
                this.SuspendLayout();

                TinyIoCContainer.Current.TryResolve(out this.DataManager);

                chartMain.ChartAreas["ChartAreaMain"].BackColor = chartMain.BackColor;
                chartMain.Legends["LegendMain"].BackColor = chartMain.BackColor;

                this.ResumeLayout(false);
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
            }
        }

        public async virtual void ShowChart()
        {
            try
            {
                progressBarMain.Visible = true;
                this.UseWaitCursor = true;

                //chartMain.ChartAreas["ChartAreaMain"].BackColor = chartMain.BackColor;
                //chartMain.Legends["LegendMain"].BackColor = chartMain.BackColor;

                var charArg = new ListOfValueLoadingEventArgs
                {
                    ListOfValueViewName = this.ViewName
                };

                await Task.Run(() => this.DataPopulateAllListOfValueRecords(charArg));

                chartMain.DataSource = charArg.ListOfValueDataTable;
                chartMain.DataBind();
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
            }
            finally
            {
                progressBarMain.Visible = false;
                this.UseWaitCursor = false;
            }
        }

        internal void DataPopulateAllListOfValueRecords(ListOfValueLoadingEventArgs arg)
        {
            DataTable dataTable = null;
            if (arg.LimitLoad)
            {
                dataTable = DataManager?.LoadListOfValuesDataFromDatabase(arg,
                    this.GetViewColumnDbNameList(), null);
            }
            else
            {
                int limitOffset = 0;
                bool keepOnloading = true;
                while (keepOnloading)
                {
                    var table = DataManager?.LoadListOfValuesDataFromDatabase(arg,
                        this.GetViewColumnDbNameList(), limitOffset);

                    limitOffset += 100;
                    if (table != null)
                    {
                        if (dataTable == null)
                        {
                            dataTable = table;
                        }
                        else
                        {
                            dataTable.Merge(table);
                        }

                        if (table.Rows.Count < 100)
                        {
                            keepOnloading = false;
                        }
                    }
                    else
                    {
                        keepOnloading = false;
                    }
                }
            }
            arg.ListOfValueDataTable = dataTable;
        }

        protected virtual IList<string> GetViewColumnDbNameList()
        {
            return new List<string>();
        }
    }
}
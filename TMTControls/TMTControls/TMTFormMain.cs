using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TinyIoC;
using TMTControls.TMTDialogs;
using TMTControls.TMTPanels;

namespace TMTControls
{
    public partial class TMTFormMain : Form
    {
        private List<Type> navigationOrder = new List<Type>();

        public TMTFormMain()
        {
            InitializeComponent();
        }

        public virtual UserControl LoadPanel(Type panelType)
        {
            if (panelType == null)
            {
                throw new ArgumentNullException(nameof(panelType));
            }

            UserControl panel = null;
            try
            {
                string panleName = panelType.Name;

                if (panelMain.Controls.ContainsKey(panleName) == false)
                {
                    panel = Activator.CreateInstance(panelType) as UserControl;

                    panel.Name = panleName;
                    panel.Dock = DockStyle.Fill;
                    panelMain.Controls.Add(panel);

                    if (panel is BaseHomeWindow homePanel)
                    {
                        homePanel.TileButtonClicked += FormMain_TileButtonClicked;
                    }
                    else if (panel is BaseUserControl basePanel)
                    {
                        basePanel.NavigateBack += FormMain_BackButtonClicked;
                    }
                }
                else
                {
                    panel = panelMain.Controls[panleName] as UserControl;
                }

                if (panel != null)
                {
                    panel.Visible = true;
                    panel.BringToFront();
                    panel.Focus();
                    if (panel is BaseWindow baseWindow)
                    {
                        baseWindow.LoadIfActive();
                    }
                    if (this.navigationOrder.Contains(panel.GetType()))
                    {
                        int itemIndex = this.navigationOrder.IndexOf(panel.GetType());
                        while (itemIndex < this.navigationOrder.Count)
                        {
                            this.navigationOrder.RemoveAt(itemIndex);
                        }
                    }

                    this.navigationOrder.Add(panel.GetType());
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
            }
            return panel;
        }

        public virtual UserControl LoadTopWindow(object sender)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            var topWindow = this.navigationOrder.Last();
            if (topWindow != null)
            {
                this.navigationOrder.Remove(topWindow);

                if (topWindow == sender.GetType())
                {
                    topWindow = this.navigationOrder.Last();
                    if (topWindow != null)
                    {
                        this.navigationOrder.Remove(topWindow);
                    }
                }
                if (topWindow != null)
                {
                    return this.LoadPanel(topWindow);
                }
            }
            return null;
        }

        private void FormMain_BackButtonClicked(object sender, EventArgs e)
        {
            try
            {
                this.LoadTopWindow(sender);
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
            }
        }

        private void FormMain_TileButtonClicked(object sender, TileButtonClickedEventArgs e)
        {
            if (e.NavigatePanel != null)
            {
                this.LoadPanel(e.NavigatePanel);
            }
        }

        private void TMTFormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Properties.Settings.Default.Save();
            }
            catch { }
        }

        private void TMTFormMain_Load(object sender, EventArgs e)
        {
            try
            {
                FontAwesome5.SetFontFileBytes(Properties.Resources.fontawesome_webfont);
                FontAwesome5.DefaultProperties.Size = 48;
                FontAwesome5.DefaultProperties.ForeColor = Color.White;

                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    this.Text += $" - {ApplicationDeployment.CurrentDeployment.CurrentVersion}";
                }
                TinyIoCContainer.Current.AutoRegister();

                var theHomeWindow = TinyIoCContainer.Current.Resolve<IRootHomeWinodw>();
                if (theHomeWindow != null)
                {
                    this.LoadPanel(theHomeWindow.GetType());
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
            }
        }
    }
}
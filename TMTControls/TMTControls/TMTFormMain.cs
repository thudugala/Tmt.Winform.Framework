using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using TMTControls.TMTPanels;

namespace TMTControls
{
    public partial class TMTFormMain : Form
    {
        private Stack<Type> navigationOrder = new Stack<Type>();

        public TMTFormMain()
        {
            InitializeComponent();
        }

        public UserControl LoadPanel(Type panelType)
        {
            return this.LoadPanel(panelType.Assembly, string.Format("{0}.{1}", panelType.Namespace, panelType.Name));
        }

        public virtual UserControl LoadPanel(Assembly assemblyOfType, string panelFullName)
        {
            UserControl panel = null;
            try
            {
                if (string.IsNullOrWhiteSpace(panelFullName))
                {
                    return null;
                }

                string panleName = panelFullName.Substring(panelFullName.LastIndexOf('.') + 1);

                if (panelMain.Controls.ContainsKey(panleName) == false)
                {
                    panel = assemblyOfType.CreateInstance(panelFullName) as UserControl;

                    panel.Name = panleName;
                    panel.Dock = DockStyle.Fill;
                    panelMain.Controls.Add(panel);

                    if (panel is TMTPanelHome homePanel)
                    {
                        homePanel.TileButtonClicked += FormMain_TileButtonClicked;
                    }
                    else if (panel is TMTUserControl tmtPanel)
                    {
                        tmtPanel.BackButtonClicked += FormMain_BackButtonClicked;
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
                    if (panel is TMTPanelTable tabelPanel)
                    {
                        tabelPanel.LoadWindowWithDataWhenActive();
                    }
                    else if (panel is TMTPanelForm formPanel)
                    {
                        formPanel.LoadWindowWithDataWhenActive();
                    }
                    this.navigationOrder.Push(panel.GetType());
                }
            }
            catch (Exception ex)
            {
                TMTErrorDialog.Show(this, ex, Properties.Resources.ERROR_PanelLoadIssue);
            }
            return panel;
        }

        private void FormMain_TileButtonClicked(object sender, TMTPanelHome.TileButtonClickedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.PanelFullName) == false)
            {
                this.LoadPanel(e.AssemblyOfType, e.PanelFullName);
            }
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

        public virtual UserControl LoadTopWindow(object sender)
        {
            var topWindow = this.navigationOrder.Pop();
            if (topWindow == sender.GetType())
            {
                topWindow = this.navigationOrder.Pop();
            }
            if (topWindow != null)
            {
                return this.LoadPanel(topWindow);
            }
            return null;
        }

        private void TMTFormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Properties.Settings.Default.Save();
            }
            catch { }
        }
    }
}
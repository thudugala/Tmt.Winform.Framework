using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
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

        public UserControl LoadPanel(Type panelType)
        {
            if (panelType == null)
            {
                throw new ArgumentNullException(nameof(panelType));
            }

            return this.LoadPanel(panelType.Assembly, string.Format(CultureInfo.InvariantCulture, "{0}.{1}", panelType.Namespace, panelType.Name));
        }

        public virtual UserControl LoadPanel(Assembly assemblyOfType, string panelFullName)
        {
            if (assemblyOfType == null)
            {
                throw new ArgumentNullException(nameof(assemblyOfType));
            }

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
                    if (this.navigationOrder.Contains(panel.GetType()))
                    {
                        int itemIndex = this.navigationOrder.IndexOf(panel.GetType());
                        while(itemIndex < this.navigationOrder.Count)
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

        private void FormMain_TileButtonClicked(object sender, TileButtonClickedEventArgs e)
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
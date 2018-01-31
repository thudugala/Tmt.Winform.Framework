using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMTControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMTControls.Tests
{
    [TestClass()]
    public class TMTFormMainTests
    {
        [TestMethod()]
        public void LoadPanelTest()
        {
            var main = new TMTFormMain();
            var panel = main.LoadPanel(typeof(TMTPanels.TMTPanelForm));

            Assert.IsNotNull(panel);
            Assert.IsInstanceOfType(panel, typeof(TMTPanels.TMTPanelForm));
        }

        [TestMethod()]
        public void LoadTopWindowTest()
        {
            var main = new TMTFormMain();                        
            var homePanel = main.LoadPanel(typeof(TMTPanels.TMTPanelHome));
            var tablePanel = main.LoadPanel(typeof(TMTPanels.TMTPanelTable));
            var formPanel = main.LoadPanel(typeof(TMTPanels.TMTPanelForm));

            var topWindow = main.LoadTopWindow(formPanel);

            Assert.IsNotNull(topWindow);
            Assert.IsInstanceOfType(topWindow, typeof(TMTPanels.TMTPanelTable));
        }
    }
}
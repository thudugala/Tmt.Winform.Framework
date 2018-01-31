using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMTControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMTControls.TMTDataGrid;

namespace TMTControls.Tests
{
    [TestClass()]
    public class TMTExtendTests
    {
        [TestMethod()]
        public void GetChildDataGridViewListTest()
        {
            var tabPageBranchVerifier = new TabPage();
            tabPageBranchVerifier.Controls.Add(new TMTDataGridView());

            var tabPageTemplateDetails = new TabPage();
            tabPageTemplateDetails.Controls.Add(new TMTDataGridView());

            var tabPageInvoices = new TabPage();
            tabPageInvoices.Controls.Add(new TMTDataGridView());

            var tabControlMain = new TabControl();
            tabControlMain.Controls.Add(tabPageBranchVerifier);
            tabControlMain.Controls.Add(tabPageTemplateDetails);
            tabControlMain.Controls.Add(tabPageInvoices);

            var panelControlContainer = new Panel();
            panelControlContainer.Controls.Add(tabControlMain);

            var gridList = panelControlContainer.GetChildDataGridViewList();
            
            Assert.IsNotNull(gridList);
            Assert.IsTrue(gridList.Count == 3);
        }
    }
}
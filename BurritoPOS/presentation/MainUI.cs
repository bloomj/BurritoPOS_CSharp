using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BurritoPOS.business;
using BurritoPOS.domain;
using log4net;
using log4net.Config;
using Spring;
using Spring.Context;
using Spring.Context.Support;

namespace BurritoPOS.presentation
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainUI : Form
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Inventory i;
        private InventoryManager iManager;
        private Random rand;

        /// <summary>
        /// 
        /// </summary>
        public MainUI()
        {
            try
            {
                //Spring.NET
                XmlApplicationContext ctx = new XmlApplicationContext("config/spring.cfg.xml");
                iManager = (InventoryManager)ctx.GetObject("InventoryManager");

                rand = new Random();
                i = new Inventory(rand.Next(), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250), rand.Next(10, 250));
                iManager.createInventory(i);
            }
            catch (Exception e)
            {
                dLog.Debug("Exception | Unable to create Inventory: " + e.Message + "\n" + e.StackTrace);
            }

            InitializeComponent();

            this.IsMdiContainer = true;
        }

        private void MainUI_Load(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void createOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create a new instance of the MDI child template form
            OrderUI orderForm = new OrderUI(i);

            //Set parent form for the child window 
            orderForm.MdiParent = this;

            //Display the child window
            orderForm.Show();
        }

        private void viewInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create a new instance of the MDI child template form
            InventoryUI inventoryForm = new InventoryUI(i);

            //Set parent form for the child window 
            inventoryForm.MdiParent = this;

            //Display the child window
            inventoryForm.Show();
        }

        private void viewOrderHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create a new instance of the MDI child template form
            OrderViewUI orderViewForm = new OrderViewUI();

            //Set parent form for the child window 
            orderViewForm.MdiParent = this;

            //Display the child window
            orderViewForm.Show();
        }
    }
}

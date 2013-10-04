using System;
using System.Collections.Generic;
using System.Collections;
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

namespace BurritoPOS.presentation.WinForm
{
    /// <summary>
    /// 
    /// </summary>
    public partial class OrderViewUI : Form
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private OrderManager oManager;
        private ArrayList tOrders;

        /// <summary>
        /// 
        /// </summary>
        public OrderViewUI()
        {
            try
            {
                //Spring.NET
                XmlApplicationContext ctx = new XmlApplicationContext("config/spring.cfg.xml");
                oManager = (OrderManager)ctx.GetObject("OrderManager");

                tOrders = oManager.getOrderHistories();
            }
            catch (Exception e)
            {
                dLog.Debug("Exception | Unable to initialize business layer components: " + e.Message + "\n" + e.StackTrace);
            }

            InitializeComponent();

            // Initialize the DataGridView.
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSize = true;

            // Initialize columns
            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "ID";
            column.Name = "#";
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Date";
            column.Name = "Date";
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "BurritoName";
            column.Name = "# of Burritos";
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Submitted";
            column.Name = "Submitted";
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Complete";
            column.Name = "Complete";
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "TotalCost";
            column.Name = "Total Cost";
            dataGridView1.Columns.Add(column);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;

            this.AutoSize = true;

            try
            {
                //add all orders
                foreach (Order o in tOrders)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = n;
                    dataGridView1.Rows[n].Cells[1].Value = o.orderDate;
                    dataGridView1.Rows[n].Cells[2].Value = o.burritos.Count;
                    dataGridView1.Rows[n].Cells[3].Value = o.isSubmitted;
                    dataGridView1.Rows[n].Cells[4].Value = o.isComplete;
                    dataGridView1.Rows[n].Cells[5].Value = o.totalCost;
                }

                updateTotalSales();
            }
            catch (Exception e)
            {
                dLog.Debug("Exception | Unable to initialize business layer components: " + e.Message + "\n" + e.StackTrace);
            }
        }

        private void deleteOrderBtn_Click(object sender, EventArgs e)
        {
            try {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedIndex = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    Order o = (Order)tOrders[selectedIndex];

                    if (oManager.cancelOrder(o))
                    {
                        dataGridView1.Rows.RemoveAt(selectedIndex);

                        //update all burrito cell numbers
                        for (int n = 0; n < dataGridView1.Rows.Count; n++)
                            dataGridView1.Rows[n].Cells[0].Value = n;

                        tOrders = oManager.getOrderHistories();
                        updateTotalSales();
                    }
                }
            }
            catch (Exception err)
            {
                dLog.Debug("Exception in deleteOrderBtn_Click: " + err.Message + "\n" + err.StackTrace);
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateTotalSales()
        {
            try
            {
                decimal totSales = 0;

                //add all orders
                foreach (Order o in tOrders)
                    totSales += o.totalCost;

                salesLbl.Text = "Total Sales: $" + totSales;
            }
            catch (Exception e)
            {
                dLog.Debug("Exception in updateTotalSales: " + e.Message + "\n" + e.StackTrace);
            }
        }
    }
}

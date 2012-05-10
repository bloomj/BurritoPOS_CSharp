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
    public partial class OrderUI : Form
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private OrderManager oManager;
        private Order newOrder;
        private InventoryManager iManager;
        private Inventory curInventory;
        private BurritoDialog bDialog;

        /// <summary>
        /// 
        /// </summary>
        public OrderUI(Inventory _i)
        {
            try
            {
                //Spring.NET
                XmlApplicationContext ctx = new XmlApplicationContext("config/spring.cfg.xml");
                iManager = (InventoryManager)ctx.GetObject("InventoryManager");
                oManager = (OrderManager)ctx.GetObject("OrderManager");

                newOrder = new Order();
                curInventory = _i;
                bDialog = new BurritoDialog(curInventory);
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
            column.DataPropertyName = "#";
            column.Name = "#";
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Type";
            column.Name = "Type";
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Price";
            column.Name = "Price";
            dataGridView1.Columns.Add(column);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;

            this.AutoSize = true;
            
        }

        private void addBurritoBtn_Click(object sender, EventArgs e)
        {
            try
            {
                bDialog.clearState(curInventory);
                if (bDialog.ShowDialog() == DialogResult.OK && oManager.addBurritoToOrder(newOrder, bDialog.getNewBurrito()))
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = n;
                    dataGridView1.Rows[n].Cells[1].Value = bDialog.getBurritoType();
                    dataGridView1.Rows[n].Cells[2].Value = bDialog.getNewBurrito().Price;

                    //remove ingredients from Inventory
                    iManager.removeFromInventory(curInventory, bDialog.getNewBurrito());
                    iManager.updateInventory(curInventory);

                    //update cost
                    updateTotalCost();
                }
            }
            catch (Exception err)
            {
                dLog.Error("Exception in addBurritoBtn_Click: " + err.Message);
            }
        }

        private void editBurritoBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    bDialog.clearState(curInventory);

                    //keep track of the burrito we are modifying
                    int selectedIndex = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    Burrito tBurrito = newOrder.burritos[selectedIndex];
                    bDialog.setBurrito(tBurrito);

                    if (bDialog.ShowDialog() == DialogResult.OK && oManager.updateBurritoInOrder(newOrder, bDialog.getNewBurrito()))
                    {
                        dataGridView1.Rows[selectedIndex].Cells[1].Value = bDialog.getBurritoType();
                        dataGridView1.Rows[selectedIndex].Cells[2].Value = bDialog.getNewBurrito().Price;

                        //remove ingredients from Inventory
                        iManager.removeFromInventory(curInventory, tBurrito);
                        iManager.removeFromInventory(curInventory, bDialog.getNewBurrito());
                        iManager.updateInventory(curInventory);

                        //update cost
                        updateTotalCost();
                    }
                }
            }
            catch (Exception err)
            {
                dLog.Error("Exception in editBurritoBtn_Click: " + err.Message);
            }
        }

        private void removeBurritoBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedIndex = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    Burrito tBurrito = newOrder.burritos[selectedIndex];

                    if (oManager.removeBurritoFromOrder(newOrder, tBurrito))
                    {
                        dataGridView1.Rows.RemoveAt(selectedIndex);

                        //update all burrito cell numbers
                        for (int n = 0; n < dataGridView1.Rows.Count; n++)
                            dataGridView1.Rows[n].Cells[0].Value = n;

                        //remove ingredients from Inventory
                        iManager.removeFromInventory(curInventory, tBurrito);
                        iManager.updateInventory(curInventory);

                        //update cost
                        updateTotalCost();
                    }
                }
            }
            catch (Exception err)
            {
                dLog.Error("Exception in removeBurritoBtn_Click: " + err.Message);
            }
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (newOrder.totalCost.CompareTo(new decimal(0)) != 1)
                {
                    MessageBox.Show("Please add a burrito to submit an order", "Warning");
                }
                else
                {
                    newOrder.orderDate = DateTime.Now;
                    newOrder.isSubmitted = true;

                    if (oManager.updateOrder(newOrder))
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception err)
            {
                dLog.Error("Exception in submitBtn_Click: " + err.Message);
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel this order?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    //return ingredients back to inventory
                    foreach (Burrito b in newOrder.burritos)
                        iManager.returnToInventory(curInventory, b);
                    iManager.updateInventory(curInventory);

                    //cancel any order that was created up to this point
                    if (oManager.cancelOrder(newOrder))
                    {
                        dLog.Debug("Successfully cancelled order #: " + newOrder.id);
                    }
                    else
                    {
                        dLog.Debug("Failed to cancel order #: " + newOrder.id);
                    }

                    this.Close();
                }
                catch (Exception err)
                {
                    dLog.Error("Exception in cancelBtn_Click: " + err.Message);
                }
            }
        }

        private void updateTotalCost()
        {
            try
            {
                newOrder.totalCost = oManager.calculateTotal(newOrder);
                priceLbl.Text = "Total Price: $" + newOrder.totalCost;
            }
            catch (Exception err)
            {
                dLog.Error("Exception in updateTotalCost: " + err.Message);
            }
        }
    }
}

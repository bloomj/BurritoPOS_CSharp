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
    public partial class InventoryUI : Form
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Inventory curInventory;
        private InventoryManager iManager;
        private Random rand;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_i"></param>
        public InventoryUI(Inventory _i)
        {
            try
            {
                //Spring.NET
                XmlApplicationContext ctx = new XmlApplicationContext("config/spring.cfg.xml");
                iManager = (InventoryManager)ctx.GetObject("InventoryManager");

                rand = new Random();
                curInventory = _i;
            }
            catch (Exception e)
            {
                dLog.Debug("Exception | Unable to initialize Inventory UI: " + e.Message + "\n" + e.StackTrace);
            }

            InitializeComponent();

            setDefaultValues();
        }

        private void setDefaultValues()
        {
            try
            {
                beefTxt.Text = curInventory.BeefQty.ToString();
                chickenTxt.Text = curInventory.ChickenQty.ToString();
                hummusTxt.Text = curInventory.HummusQty.ToString();
                flourTortText.Text = curInventory.FlourTortillaQty.ToString();
                wheatTortText.Text = curInventory.WheatTortillaQty.ToString();
                chiliTortTxt.Text = curInventory.ChiliTortillaQty.ToString();
                herbGarlicTortTxt.Text = curInventory.HerbGarlicTortillaQty.ToString();
                jalapenoTortTxt.Text = curInventory.JalapenoCheddarTortillaQty.ToString();
                tomatoTortTxt.Text = curInventory.TomatoBasilTortillaQty.ToString();
                whiteRiceTxt.Text = curInventory.WhiteRiceQty.ToString();
                brownRiceTxt.Text = curInventory.BrownRiceQty.ToString();
                blackBeansTxt.Text = curInventory.BlackBeansQty.ToString();
                pintoBeansTxt.Text = curInventory.PintoBeansQty.ToString();
                salsaPicoTxt.Text = curInventory.SalsaPicoQty.ToString();
                salsaVerdeTxt.Text = curInventory.SalsaVerdeQty.ToString();
                salsaSpecialTxt.Text = curInventory.SalsaSpecialQty.ToString();
                guacTxt.Text = curInventory.GuacamoleQty.ToString();
                lettuceTxt.Text = curInventory.LettuceQty.ToString();
                cucumberTxt.Text = curInventory.CucumberQty.ToString();
                jalapenoTxt.Text = curInventory.JalapenosQty.ToString();
                onionTxt.Text = curInventory.OnionQty.ToString();
                tomatoTxt.Text = curInventory.TomatoesQty.ToString();
            }
            catch (Exception e)
            {
                dLog.Error("Exception in setDefaultValues: " + e.Message);
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //set updated values
                if (Int32.Parse(beefTxt.Text) >= 0)
                    curInventory.setBeefQty(Int32.Parse(beefTxt.Text));
                if (Int32.Parse(chickenTxt.Text) >= 0)
                    curInventory.setChickenQty(Int32.Parse(chickenTxt.Text));
                if (Int32.Parse(hummusTxt.Text) >= 0)
                    curInventory.setHummusQty(Int32.Parse(hummusTxt.Text));
                if (Int32.Parse(flourTortText.Text) >= 0)
                    curInventory.setFlourTortillaQty(Int32.Parse(flourTortText.Text));
                if (Int32.Parse(wheatTortText.Text) >= 0)
                    curInventory.setWheatTortillaQty(Int32.Parse(wheatTortText.Text));
                if (Int32.Parse(chiliTortTxt.Text) >= 0)
                    curInventory.setChiliTortillaQty(Int32.Parse(chiliTortTxt.Text));
                if (Int32.Parse(herbGarlicTortTxt.Text) >= 0)
                    curInventory.setHerbGarlicTortillaQty(Int32.Parse(herbGarlicTortTxt.Text));
                if (Int32.Parse(jalapenoTortTxt.Text) >= 0)
                    curInventory.setJalapenoCheddarTortillaQty(Int32.Parse(jalapenoTortTxt.Text));
                if (Int32.Parse(tomatoTortTxt.Text) >= 0)
                    curInventory.setTomatoBasilTortillaQty(Int32.Parse(tomatoTortTxt.Text));
                if (Int32.Parse(whiteRiceTxt.Text) >= 0)
                    curInventory.setWhiteRiceQty(Int32.Parse(whiteRiceTxt.Text));
                if (Int32.Parse(brownRiceTxt.Text) >= 0)
                    curInventory.setBrownRiceQty(Int32.Parse(brownRiceTxt.Text));
                if (Int32.Parse(blackBeansTxt.Text) >= 0)
                    curInventory.setBlackBeansQty(Int32.Parse(blackBeansTxt.Text));
                if (Int32.Parse(pintoBeansTxt.Text) >= 0)
                    curInventory.setPintoBeansQty(Int32.Parse(pintoBeansTxt.Text));
                if (Int32.Parse(salsaPicoTxt.Text) >= 0)
                    curInventory.setSalsaPicoQty(Int32.Parse(salsaPicoTxt.Text));
                if (Int32.Parse(salsaSpecialTxt.Text) >= 0)
                    curInventory.setSalsaSpecialQty(Int32.Parse(salsaSpecialTxt.Text));
                if (Int32.Parse(salsaVerdeTxt.Text) >= 0)
                    curInventory.setSalsaVerdeQty(Int32.Parse(salsaVerdeTxt.Text));
                if (Int32.Parse(lettuceTxt.Text) >= 0)
                    curInventory.setLettuceQty(Int32.Parse(lettuceTxt.Text));
                if (Int32.Parse(cucumberTxt.Text) >= 0)
                    curInventory.setCucumberQty(Int32.Parse(cucumberTxt.Text));
                if (Int32.Parse(jalapenoTxt.Text) >= 0)
                    curInventory.setJalapenosQty(Int32.Parse(jalapenoTxt.Text));
                if (Int32.Parse(onionTxt.Text) >= 0)
                    curInventory.setOnionQty(Int32.Parse(onionTxt.Text));
                if (Int32.Parse(tomatoTxt.Text) >= 0)
                    curInventory.setTomatoesQty(Int32.Parse(tomatoTxt.Text));

                if (curInventory.validate() && iManager.updateInventory(curInventory))
                    MessageBox.Show("Inventory Successfully updated","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                else
                    MessageBox.Show("Failed to update Inventory", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception err)
            {
                dLog.Error("Exception in updateBtn_Click: " + err.Message);
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (curInventory.validate())
                    iManager.updateInventory(curInventory);

                this.Close();
            }
            catch (Exception err)
            {
                dLog.Error("Exception in exitBtn_Click: " + err.Message);
            }
        }
    }
}
